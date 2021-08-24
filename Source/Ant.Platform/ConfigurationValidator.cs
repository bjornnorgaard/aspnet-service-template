using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Serilog;
using Ant.Platform.Exceptions;
using Ant.Platform.Options;

namespace Ant.Platform
{
    internal static class ConfigurationValidator
    {
        internal static void ValidatePlatformConfiguration(this IConfiguration configuration)
        {
            var optionTypes = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.IsAssignableTo(typeof(AbstractOptions)))
                .Where(t => t.IsAbstract == false)
                .ToList();

            var invalidConfigurations = new List<string>();

            foreach (var optionType in optionTypes)
            {
                var concreteOptionType = Activator.CreateInstance(optionType, configuration);
                configuration.GetSection(optionType.Name).Bind(concreteOptionType);

                var nulls = optionType
                    .GetProperties()
                    .Where(p => optionType.GetProperty(p.Name)?.GetValue(concreteOptionType, null) == null)
                    .Select(p => p.Name)
                    .ToList();

                foreach (var prop in nulls)
                {
                    invalidConfigurations.Add($"{optionType.Name}:{prop}");
                }
            }
            
            if (invalidConfigurations.Any())
            {
                var errorMessage = string.Join("\n\t", invalidConfigurations);
                Log.Error($"Application has missing values for these configurations:\n\t{errorMessage}");
                
                throw new ConfigurationException(invalidConfigurations);
            }
        }
    }
}