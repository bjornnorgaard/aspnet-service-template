using Ant.Platform.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ant.Platform.Configurations
{
    public static class AuthenticationConfiguration
    {
        public static void AddPlatformAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var options = new AuthenticationOptions(configuration);

            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.Authority = options.Authority;
                o.Audience = options.Audience;
            });
        }

        public static void UsePlatformAuthentication(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
        }
    }
}