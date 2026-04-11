using System.Collections.Generic;
using System.Linq;
using TddXt.TypeReflection.Interfaces;

namespace TddXt.TypeReflection.ImplementationDetails.ConstructorRetrievals;

public class PrivateOrProtectedNonRecursiveConstructorRetrieval(ConstructorRetrieval next) : ConstructorRetrieval
{
  public IEnumerable<IConstructorWrapper> RetrieveFrom(IConstructorQueries constructors)
  {
    var methods = constructors.TryToObtainPrivateAndProtectedConstructorsWithoutRecursiveArguments();
    if (methods.Any())
    {
      return methods;
    }
    else
    {
      return next.RetrieveFrom(constructors);
    }

  }
}
