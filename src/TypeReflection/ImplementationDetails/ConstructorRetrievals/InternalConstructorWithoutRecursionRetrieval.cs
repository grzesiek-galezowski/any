using System.Collections.Generic;
using System.Linq;
using TddXt.TypeReflection.Interfaces;

namespace TddXt.TypeReflection.ImplementationDetails.ConstructorRetrievals;

public class InternalConstructorWithoutRecursionRetrieval(ConstructorRetrieval next) : ConstructorRetrieval
{
  public IEnumerable<IConstructorWrapper> RetrieveFrom(IConstructorQueries constructors)
  {
    var internalConstructors = constructors.TryToObtainInternalConstructorsWithoutRecursiveArguments();

    if (internalConstructors.Any())
    {
      return internalConstructors;
    }
    else
    {
      return next.RetrieveFrom(constructors);
    }
  }
}
