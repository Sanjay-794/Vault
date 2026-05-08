using Devnet.Vault.Application.Behaviour;
using Devnet.Vault.Application.Features.Auth.Commands;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
namespace Devnet.Vault.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(RegisterOrLoginCommand).Assembly);

        services.AddTransient(
            typeof(IPipelineBehavior<,>),
            typeof(ValidationBehavior<,>)
        );

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(RegisterOrLoginCommand).Assembly);
        });

        return services;
    }
}