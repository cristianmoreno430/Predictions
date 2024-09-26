using ArchUnitNET.Domain;
using ArchUnitNET.Loader;
using ArchUnitNET.xUnit;
using static ArchUnitNET.Fluent.ArchRuleDefinition;

namespace Predictions.IApredictions.Tests;

public class IApredictionsTypesShould
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
      .ResideInNamespace("Predictions.IApredictions.*", true)
      .And()
      .AreNot([typeof(AssemblyInfo), typeof(IApredictionsModuleServiceRegistrar)])
      .As("Module types");

    var rule = domainTypes.Should().BeInternal();

    rule.Check(Architecture);
  }
}
