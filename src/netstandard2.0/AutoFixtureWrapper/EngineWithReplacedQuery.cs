using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using AutoFixture.Kernel;

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
                    if (value is MethodInvoker mi &&
                        mi.Query is CompositeMethodQuery cmq &&
                        cmq.Queries.Skip(1).FirstOrDefault() is FactoryMethodQuery)
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