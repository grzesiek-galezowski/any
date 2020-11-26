using System;
using TddXt.TypeResolution.FakeChainElements;

namespace TddXt.AnyGenerators.Generic.ExtensionPoints
{
  public interface ISpecialCasesOfResolutions<T>
  {
    IResolution<T> CreateResolutionOfKeyValuePair();
    IResolution<T> CreateResolutionOf2GenericType(string className, params Type[] matchingTypes);
    IResolution<T> CreateResolutionOf1GenericType(string resolvedTypeName, params Type[] genericTypes);
    IResolution<T> CreateResolutionOfArray();
  }
}