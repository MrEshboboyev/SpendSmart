﻿using Microsoft.Extensions.Options;
using Raven.Client.Documents;
using Raven.Client.Documents.Indexes;
using Raven.Client.Documents.Operations.Expiration;
using Raven.Client.Documents.Session;
using Raven.Client.Json.Serialization.NewtonsoftJson;
using Raven.Client.ServerWide;
using Raven.Client.ServerWide.Operations;
using SpendSmart.Common.Abstractions.Clock;
using SpendSmart.Domain.Primitives;
using SpendSmart.Persistence.Options;
using SpendSmart.Persistence.Serialization;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace SpendSmart.Persistence.Providers;

/// <summary>
/// Represents the document store provider.
/// </summary>
public sealed class DocumentStoreProvider : IDisposable
{
    private static readonly Dictionary<Type, MethodInfo> SetCreatedOnMethodDictionary = [];
    private static readonly Dictionary<Type, MethodInfo> SetModifiedOnMethodDictionary = [];
    private readonly ISystemTime _systemTime;

    /// <summary>
    /// Initializes a new instance of the <see cref="DocumentStoreProvider"/> class.
    /// </summary>
    /// <param name="options">The RavenDb options.</param>
    /// <param name="systemTime">The system time.</param>
    public DocumentStoreProvider(IOptions<RavenDbOptions> options,
                                 ISystemTime systemTime)
    {
        _systemTime = systemTime;

        DocumentStore = new DocumentStore
        {
            Certificate = X509Certificate2.CreateFromPem(options.Value.Certificate),
            Database = options.Value.Database,
            Urls = options.Value.Urls
        };

        DocumentStore.Conventions.Serialization = new NewtonsoftJsonSerializationConventions
        {
            JsonContractResolver = new CustomContractResolver(DocumentStore.Conventions.Serialization)
        };

        DocumentStore.OnBeforeStore += SetAuditableEntityValues_OnBeforeStore;

        DocumentStore.Initialize();

        CreateDatabaseIfItDoesNotExist(DocumentStore);

        ConfigureExpirationOptions(DocumentStore);

        IndexCreation.CreateIndexes(Assembly.GetExecutingAssembly(), DocumentStore);
    }

    /// <summary>
    /// Gets the document store.
    /// </summary>
    public IDocumentStore DocumentStore { get; }

    /// <inheritdoc />
    public void Dispose() => DocumentStore?.Dispose();

    private static void CreateDatabaseIfItDoesNotExist(IDocumentStore documentStore)
    {
        var getDatabaseRecordOperation = new GetDatabaseRecordOperation(documentStore.Database);

        DatabaseRecordWithEtag databaseRecord = documentStore.Maintenance.Server.Send(getDatabaseRecordOperation);

        if (databaseRecord is not null)
        {
            return;
        }

        var createDatabaseOperation = new CreateDatabaseOperation(new DatabaseRecord(documentStore.Database));

        documentStore.Maintenance.Server.Send(createDatabaseOperation);
    }

    private static void ConfigureExpirationOptions(IDocumentStore documentStore)
    {
        var configureExpirationOperation = new ConfigureExpirationOperation(new ExpirationConfiguration
        {
            Disabled = false,
            DeleteFrequencyInSec = 60
        });

        documentStore.Maintenance.Send(configureExpirationOperation);
    }

    private void SetAuditableEntityValues_OnBeforeStore(object sender, BeforeStoreEventArgs eventArgs)
    {
        if (eventArgs.Entity is not IAuditableEntity auditableEntity)
        {
            return;
        }

        if (auditableEntity.CreatedOnUtc == default)
        {
            ApplySetPropertyMethod(auditableEntity, SetCreatedOnMethodDictionary, nameof(IAuditableEntity.CreatedOnUtc));
        }
        else
        {
            ApplySetPropertyMethod(auditableEntity, SetModifiedOnMethodDictionary, nameof(IAuditableEntity.ModifiedOnUtc));
        }
    }

    private void ApplySetPropertyMethod(
        IAuditableEntity auditableEntity,
        IDictionary<Type, MethodInfo> propertySetterMethodsDictionary,
        string propertyName)
    {
        Type underlyingType = auditableEntity.GetType();

        if (!propertySetterMethodsDictionary.TryGetValue(underlyingType, out MethodInfo value))
        {
            value = underlyingType.GetProperty(propertyName)!.GetSetMethod(true)!;
            propertySetterMethodsDictionary.Add(underlyingType, value);
        }

        value.Invoke(auditableEntity, [_systemTime.UtcNow]);
    }
}
