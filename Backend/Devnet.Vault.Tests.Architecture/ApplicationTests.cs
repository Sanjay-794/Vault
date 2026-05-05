using NetArchTest.Rules;

namespace Devnet.Vault.Tests.Architecture;

public class ApplicationTests
{
    [Fact]
    public void Application_Should_Not_Depend_On_Infrastructure_Or_Api()
    {
        var forbidden = new[]
        {
            AssemblyExtensions.Infrastructure.Name(),
            AssemblyExtensions.Api.Name()
        };

        var result = Types.InAssembly(AssemblyExtensions.Application)
            .ShouldNot()
            .HaveDependencyOnAny(forbidden)
            .GetResult();

        Assert.True(result.IsSuccessful, AssemblyExtensions.GetFailureMessage(result));
    }
}
