using System.Text.Json.Serialization;

namespace Devnet.Vault.Api.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers()
           .AddJsonOptions(options =>
           {
               options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
           });

        services.BindAppsettings(configuration);

        services.AddAuthPolicy(configuration);
        services.AddCORSPolicy(configuration);
        services.AddOpenApi();

        return services;
    }
}
