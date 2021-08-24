using System;
using System.Collections.Generic;

namespace Svc.Platform.Exceptions
{
    internal class ConfigurationException : Exception
    {
        public List<string> MissingConfigurations { get; }
        
        public ConfigurationException(List<string> invalidConfigurations) 
            : base($"Found missing configurations, check {nameof(MissingConfigurations)} property for details.")
        {
            MissingConfigurations = invalidConfigurations;

            Console.WriteLine("These sections and properties are missing values:");
            foreach (var prop in MissingConfigurations)
            {
                Console.WriteLine($"{prop}");
            }
        }
    }
}