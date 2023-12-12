using System.Collections.Generic;
using System.Linq;
using TddXt.TypeReflection.Interfaces;

namespace TddXt.TypeReflection.ImplementationDetails.ConstructorRetrievals;

public class PublicNonRecursiveConstructorRetrieval(ConstructorRetrieval next) : ConstructorRetrieval
{
  public IEnumerable<IConstructorWrapper> RetrieveFrom(IConstructorQueries constructors)
  {
    var publicConstructors = constructors.TryToObtainPublicConstructorsWithoutRecursiveArguments();

    if (publicConstructors.Any())
    {
      return publicConstructors;
    }
    else
    {
      return next.RetrieveFrom(constructors);
    }
  }
}
