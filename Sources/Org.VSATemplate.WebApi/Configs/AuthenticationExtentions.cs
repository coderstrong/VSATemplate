using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;

namespace Org.VSATemplate.WebApi.Configs
{
    public static class AuthenticationExtentions
    {
        public static IServiceCollection AddAuthenticationExtension(this IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
            });
            return services;
        }
    }
}