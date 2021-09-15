using System;
using System.Linq;
using Ant.Platform.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Ant.Platform.Configurations
{
    internal static class SwaggerConfiguration
    {
        internal static void AddPlatformSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            var options = new SwaggerOptions(configuration);

            services.AddSwaggerGen(c =>
            {
                c.CustomSchemaIds(t =>
                {
                    if (t.FullName.Contains("+"))
                    {
                        return t.FullName.Split(".").Last().Replace("+", "");
                    }

                    return t.Name;
                });
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = options.ApplicationTitle, Version = "v1",
                });

                var auth = new AuthenticationOptions(configuration);
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        Implicit = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri($"{auth.Authority}/authorize")
                        }
                    }
                });
            });
        }

        internal static void UsePlatformSwagger(this IApplicationBuilder app, IConfiguration configuration)
        {
            var options = new SwaggerOptions(configuration);
            var auth = new AuthenticationOptions(configuration);

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{options.ApplicationTitle} v1");
                c.RoutePrefix = string.Empty;

                c.OAuthRealm(auth.Authority);
                c.OAuthClientId(auth.TestClientId);
                c.OAuthClientSecret(auth.TestClientSecret);
                c.OAuthAppName("SwaggerUI");
                c.OAuthUseBasicAuthenticationWithAccessCodeGrant();
            });
        }
    }
}
