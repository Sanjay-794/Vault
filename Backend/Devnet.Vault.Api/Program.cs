
using Devnet.Vault.Api.Extensions;

namespace Devnet.Vault.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddPresentation(builder.Configuration);

        var app = builder.Build();

        app.AddMiddlewares();

        app.Run();
    }
}
