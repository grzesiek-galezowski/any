using System.Collections.Generic;
using TddXt.TypeReflection.Interfaces;

namespace TddXt.TypeReflection.ImplementationDetails.ConstructorRetrievals
{
  public interface ConstructorRetrieval
  {
    IEnumerable<IConstructorWrapper> RetrieveFrom(IConstructorQueries constructors);
  }
}