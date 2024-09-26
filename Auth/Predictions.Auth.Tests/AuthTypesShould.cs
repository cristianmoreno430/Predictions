using ArchUnitNET.Domain;
using ArchUnitNET.Loader;
using ArchUnitNET.xUnit;
using static ArchUnitNET.Fluent.ArchRuleDefinition;

namespace Predictions.Auth.Tests;

public class AuthTypesShould
{
    private static readonly Architecture Architecture =
      new ArchLoader()
        .LoadAssemblies(typeof(AssemblyInfo).Assembly)
        .Build();

    [Fact]
    public void BeInternal()
    {
        var domainTypes = Types()
          .That()
          .ResideInNamespace("Predictions.Auth.*", true)
          .And()
          .AreNot([typeof(AssemblyInfo), typeof(AuthModuleServiceRegistrar)])
          .As("Module types");

        var rule = domainTypes.Should().BeInternal();

        rule.Check(Architecture);
    }

    [Fact]
    public void notDependOnWebApi()
    {
        var coreTypes = Types()
          .That()
          .ResideInNamespace("Predictions.Auth.Core", true)
          .And()
          .AreNot([typeof(AssemblyInfo), typeof(AuthModuleServiceRegistrar)])
          .As("Core types");

        var webApiTypes = Types()
       .That()
       .ResideInNamespace("Predictions.Auth.WebApi", true)
       .As("WebApi types");

        var rule = coreTypes.Should().NotDependOnAny(webApiTypes);

        rule.Check(Architecture);
    }

}
