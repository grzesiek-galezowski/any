using System.Collections.Generic;
using System.Linq;
using TddXt.TypeReflection.Interfaces;

namespace TddXt.TypeReflection.ImplementationDetails.ConstructorRetrievals
{
  public class PublicStaticFactoryMethodRetrieval : ConstructorRetrieval
  {
    private readonly ConstructorRetrieval _next;

    public PublicStaticFactoryMethodRetrieval(ConstructorRetrieval next)
    {
      _next = next;
    }

    public IEnumerable<IConstructorWrapper> RetrieveFrom(IConstructorQueries constructors)
    {
      var methods = constructors.TryToObtainPublicStaticFactoryMethodWithoutRecursion();
      if (methods.Any())
      {
        return methods;
      }
      else
      {
        return _next.RetrieveFrom(constructors);
      }
    }
  }
}