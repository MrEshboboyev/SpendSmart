using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;

namespace SpendSmart.App.ModelBinders.Ulid;

/// <summary>
/// Represents the <see cref="UlidModelBinder"/> provider.
/// </summary>
public sealed class UlidModelBinderProvider : IModelBinderProvider
{
    /// <inheritdoc />
    public IModelBinder GetBinder(ModelBinderProviderContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        if (context.Metadata.ModelType == typeof(System.Ulid))
        {
            return new UlidModelBinder();
        }

        return null;
    }
}
