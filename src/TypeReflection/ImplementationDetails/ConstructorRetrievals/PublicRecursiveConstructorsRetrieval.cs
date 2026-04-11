using System.Collections.Generic;
using System.Linq;
using TddXt.TypeReflection.Interfaces;

namespace TddXt.TypeReflection.ImplementationDetails.ConstructorRetrievals;

public class PublicRecursiveConstructorsRetrieval(ConstructorRetrieval next) : ConstructorRetrieval
{
  public IEnumerable<IConstructorWrapper> RetrieveFrom(IConstructorQueries constructors)
  {
    var constructorList = constructors.TryToObtainPublicConstructorsWithRecursiveArguments();
    if (constructorList.Any())
    {
      return constructorList.ToList();
    }
    else
    {
      return next.RetrieveFrom(constructors);
    }
  }
}
