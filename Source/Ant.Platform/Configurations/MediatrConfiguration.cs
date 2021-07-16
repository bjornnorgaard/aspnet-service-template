using System.Reflection;
using Ant.Platform.PipelineBehaviors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Ant.Platform.Configurations
{
    public static class MediatrConfiguration
    {
        public static void AddPlatformMediatr(this IServiceCollection services, Assembly assembly)
        {
            services.AddMediatR(assembly);

            // Order of pipeline-behaviors is important
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingPipeline<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipeline<,>));

            // Add validators
            var validators = AssemblyScanner.FindValidatorsInAssemblies(new[] {assembly});
            validators.ForEach(validator => services.AddTransient(validator.InterfaceType, validator.ValidatorType));
        }
    }
}
