using NetArchTest.Rules;

namespace Devnet.Vault.Tests.Architecture;

public class DomainTests
{
    [Fact]
    public void Domain_Should_Not_Depend_On_Other_Layers()
    {
        var forbidden = new[]
        {
            AssemblyExtensions.Application.Name(),
            AssemblyExtensions.Infrastructure.Name(),
            AssemblyExtensions.Api.Name()
        };

        var result = Types.InAssembly(AssemblyExtensions.Domain)
            .ShouldNot()
            .HaveDependencyOnAny(forbidden)
            .GetResult();

        Assert.True(result.IsSuccessful, AssemblyExtensions.GetFailureMessage(result));
    }
}