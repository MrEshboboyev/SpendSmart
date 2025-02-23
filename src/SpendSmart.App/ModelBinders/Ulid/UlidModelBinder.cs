using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Threading.Tasks;

namespace SpendSmart.App.ModelBinders.Ulid;

/// <summary>
/// Represents the <see cref="Ulid"/> model binder.
/// </summary>
public sealed class UlidModelBinder : IModelBinder
{
    /// <inheritdoc />
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        ArgumentNullException.ThrowIfNull(bindingContext);

        ValueProviderResult valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

        if (valueProviderResult == ValueProviderResult.None)
        {
            return Task.CompletedTask;
        }

        string value = valueProviderResult.FirstValue;

        System.Ulid.TryParse(value, out System.Ulid ulid);

        bindingContext.Result = ModelBindingResult.Success(ulid);

        return Task.CompletedTask;
    }
}
