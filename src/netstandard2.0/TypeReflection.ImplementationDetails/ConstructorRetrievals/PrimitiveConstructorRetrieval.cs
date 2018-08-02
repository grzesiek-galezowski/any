using System.Collections.Generic;
using TddXt.TypeReflection.Interfaces;

namespace TddXt.TypeReflection.ImplementationDetails.ConstructorRetrievals
{
  public class PrimitiveConstructorRetrieval : ConstructorRetrieval
  {
    public IEnumerable<IConstructorWrapper> RetrieveFrom(IConstructorQueries constructors)
    {
      return constructors.TryToObtainPrimitiveTypeConstructor();
    }
  }
}