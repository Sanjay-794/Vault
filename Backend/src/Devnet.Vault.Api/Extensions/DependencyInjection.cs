using System.Text.Json;
using System.Text.Json.Serialization;

namespace Devnet.Vault.Api.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers()
           .AddJsonOptions(options =>
           {
               options.JsonSerializerOptions.Converters.Add(
                   new JsonStringEnumConverter(JsonNamingPolicy.CamelCase, allowIntegerValues: false));
           });

        services.BindAppsettings(configuration);

        // Register MediatR for CQRS pattern
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblies(
                typeof(Devnet.Vault.Application.Features.Auth.Commands.RequestOtpCommand).Assembly);
        });

        services.AddAuthPolicy(configuration);
        services.AddCORSPolicy(configuration);
        services.AddOpenApi();

        return services;
    }
}
