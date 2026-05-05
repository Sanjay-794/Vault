using NetArchTest.Rules;
using System.Reflection;

namespace Devnet.Vault.Tests.Architecture;

public static class AssemblyExtensions
{
    public const string DomainName = "Devnet.Vault.Domain";
    public const string ApplicationName = "Devnet.Vault.Application";
    public const string InfrastructureName = "Devnet.Vault.Infrastructure";
    public const string ApiName = "Devnet.Vault.Api";

    public static readonly Assembly Domain = Load(DomainName);
    public static readonly Assembly Application = Load(ApplicationName);
    public static readonly Assembly Infrastructure = Load(InfrastructureName);
    public static readonly Assembly Api = Load(ApiName);

    public static string Name(this Assembly assembly) =>
        assembly.GetName().Name!;

    public static Assembly Load(string name)
    {
        var assembly = AppDomain.CurrentDomain
            .GetAssemblies()
            .FirstOrDefault(a => a.GetName().Name == name);

        if (assembly != null)
            return assembly;

        try
        {
            return Assembly.Load(name);
        }
        catch (Exception ex)
        {
            var loaded = string.Join(", ",
                AppDomain.CurrentDomain
                    .GetAssemblies()
                    .Select(a => a.GetName().Name));

            throw new InvalidOperationException(
                $"Could not load assembly: {name}. Loaded assemblies: {loaded}",
                ex);
        }
    }

    public static string GetFailureMessage(TestResult result)
    {
        if (result.IsSuccessful || result.FailingTypes is null)
            return string.Empty;

        return $"Failing Types: {string.Join(", ", result.FailingTypes.Select(t => t.FullName))}";
    }
}