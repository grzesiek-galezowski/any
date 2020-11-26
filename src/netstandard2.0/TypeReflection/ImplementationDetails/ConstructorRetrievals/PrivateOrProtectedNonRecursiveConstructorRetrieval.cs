using System.Collections.Generic;
using System.Linq;
using TddXt.TypeReflection.Interfaces;

namespace TddXt.TypeReflection.ImplementationDetails.ConstructorRetrievals
{
  public class PrivateOrProtectedNonRecursiveConstructorRetrieval : ConstructorRetrieval
  {
    private readonly ConstructorRetrieval _next;

    public PrivateOrProtectedNonRecursiveConstructorRetrieval(ConstructorRetrieval next)
    {
      _next = next;
    }

    public IEnumerable<IConstructorWrapper> RetrieveFrom(IConstructorQueries constructors)
    {
      var methods = constructors.TryToObtainPrivateAndProtectedConstructorsWithoutRecursiveArguments();
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