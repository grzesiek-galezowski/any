using System.Collections.Generic;
using System.Linq;
using TddXt.TypeReflection.Interfaces;

namespace TddXt.TypeReflection.ImplementationDetails.ConstructorRetrievals;

public class InternalRecursiveConstructorRetrieval(ConstructorRetrieval next) : ConstructorRetrieval
{
  public IEnumerable<IConstructorWrapper> RetrieveFrom(IConstructorQueries constructors)
  {
    var foundConstructors = constructors.TryToObtainInternalConstructorsWithRecursiveArguments();
    if (foundConstructors.Any())
    {
      return foundConstructors.ToArray();
    }
    else
    {
      return next.RetrieveFrom(constructors);
    }
  }
}
