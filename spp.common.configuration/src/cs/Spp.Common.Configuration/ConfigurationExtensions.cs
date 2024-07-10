using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace Spp.Common.Configuration;

public static class ConfigurationExtensions
{
    public static T? FindSettings<T>(this IConfiguration configuration, string key)
    {
        var result = configuration.GetSection(key).Get<T>();

        if (result == null)
        {
            return result;
        }

        var validationContext = new ValidationContext(result);
        var validationResult = new List<ValidationResult>();

        if (!Validator.TryValidateObject(result, validationContext, validationResult)
            || !SettingsValidator.Validate(result, validationResult))
        {
            throw new ApplicationException(
                $"Validation error on section: '{key}' of type: '{typeof(T).FullName}'.",
                new ValidationException(
                    string.Join("\n", validationResult.Select(it => it.ErrorMessage))));
        }

        return result;
    }

    public static T GetSettings<T>(this IConfiguration configuration, string key)
    {
        return FindSettings<T>(configuration, key)
               ?? throw new ApplicationException(
                   $"Could not find section '{key}' of type '{typeof(T).FullName}'.");
    }
}
