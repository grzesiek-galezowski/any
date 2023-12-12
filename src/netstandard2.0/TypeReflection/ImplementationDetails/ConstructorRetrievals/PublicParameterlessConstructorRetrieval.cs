using System.Collections.Generic;
using TddXt.TypeReflection.Interfaces;

namespace TddXt.TypeReflection.ImplementationDetails.ConstructorRetrievals;

public class PublicParameterlessConstructorRetrieval(ConstructorRetrieval next) : ConstructorRetrieval
{
  public IEnumerable<IConstructorWrapper> RetrieveFrom(IConstructorQueries constructors)
  {
    var constructor = constructors.GetPublicParameterlessConstructor();
    if (constructor.HasValue)
    {
      return new List<IConstructorWrapper> { constructor.Value() };
    }
    else
    {
      return next.RetrieveFrom(constructors);
    }
  }
}
