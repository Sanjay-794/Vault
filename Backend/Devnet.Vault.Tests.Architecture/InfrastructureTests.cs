using NetArchTest.Rules;

namespace Devnet.Vault.Tests.Architecture;

public class InfrastructureTests
{
    [Fact]
    public void Infrastructure_Should_Not_Depend_On_Api()
    {
        var forbidden = new[] { AssemblyExtensions.Api.Name() };

        var result = Types.InAssembly(AssemblyExtensions.Infrastructure)
            .ShouldNot()
            .HaveDependencyOnAny(forbidden)
            .GetResult();

        Assert.True(result.IsSuccessful, AssemblyExtensions.GetFailureMessage(result));
    }


    [Fact]
    public void Only_Infrastructure_Should_Use_EntityFrameworkCore()
    {
        const string efCore = "Microsoft.EntityFrameworkCore";

        var domainResult = Types.InAssembly(AssemblyExtensions.Domain)
            .ShouldNot()
            .HaveDependencyOn(efCore)
            .GetResult();

        var applicationResult = Types.InAssembly(AssemblyExtensions.Application)
            .ShouldNot()
            .HaveDependencyOn(efCore)
            .GetResult();

        Assert.True(domainResult.IsSuccessful, AssemblyExtensions.GetFailureMessage(domainResult));
        Assert.True(applicationResult.IsSuccessful, AssemblyExtensions.GetFailureMessage(applicationResult));
    }
}
