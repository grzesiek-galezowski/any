using System;
using TddEbook.TddToolkit.TypeResolution.FakeChainElements;

namespace TddEbook.TddToolkit.Generators
{
  public interface ISpecialCasesOfResolutions<T>
  {
    IResolution<T> CreateResolutionOfKeyValuePair();
    IResolution<T> CreateResolutionOf2GenericType(string className, params Type[] matchingTypes);
    IResolution<T> CreateResolutionOf1GenericType(string resolvedTypeName, params Type[] genericTypes);
    IResolution<T> CreateResolutionOfArray();
  }
}