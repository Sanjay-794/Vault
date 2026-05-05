using NetArchTest.Rules;
using System.Reflection;

namespace Devnet.Vault.Tests.Architecture;

public class DependencyDirectionTests
{
    [Fact]
    public void Layers_Should_Follow_Clean_Architecture()
    {
        var forbiddenDependencies = new Dictionary<Assembly, Assembly[]>
        {
            [AssemblyExtensions.Domain] = [AssemblyExtensions.Application, AssemblyExtensions.Infrastructure, AssemblyExtensions.Api],
            [AssemblyExtensions.Application] = [AssemblyExtensions.Infrastructure, AssemblyExtensions.Api],
            [AssemblyExtensions.Infrastructure] = [AssemblyExtensions.Api]
        };

        foreach (var rule in forbiddenDependencies)
        {
            var assembly = rule.Key;

            var forbidden = rule.Value
                .Select(a => a.Name())
                .ToArray();

            var result = Types.InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOnAny(forbidden)
                .GetResult();

            Assert.True(result.IsSuccessful, AssemblyExtensions.GetFailureMessage(result));
        }
    }
}