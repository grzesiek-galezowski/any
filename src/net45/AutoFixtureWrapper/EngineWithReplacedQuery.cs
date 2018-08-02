using System.Collections.Generic;
using System.Linq;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;

namespace TddXt.AutoFixtureWrapper
{
  public class EngineWithReplacedQuery : DefaultEngineParts
  {
    public override IEnumerator<ISpecimenBuilder> GetEnumerator()
    {
      using (var enumerator = base.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          var value = enumerator.Current;

          // Replace target method query
          var mi = value as MethodInvoker;
          CompositeMethodQuery cmq = mi?.Query as CompositeMethodQuery;
          if (mi != null && cmq?.Queries.Skip(1).FirstOrDefault() is FactoryMethodQuery)
          {
            yield return new MethodInvoker(
                new CompositeMethodQuery(
                    new ModestConstructorQuery(),
                    new PatchedFactoryMethodQuery()
                )
            );
          }
          else
          {
            yield return value;
          }
        }
      }
    }
  }
}