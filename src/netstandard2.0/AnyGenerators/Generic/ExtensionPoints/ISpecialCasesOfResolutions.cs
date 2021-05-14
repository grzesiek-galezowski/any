using System;
using TddXt.TypeResolution.FakeChainElements;

namespace TddXt.AnyGenerators.Generic.ExtensionPoints
{
  public interface ISpecialCasesOfResolutions<T>
  {
    IResolution CreateResolutionOfKeyValuePair();
    IResolution CreateResolutionOf2GenericType(string className, params Type[] matchingTypes);
    IResolution CreateResolutionOf1GenericType(string resolvedTypeName, params Type[] genericTypes);
    IResolution CreateResolutionOfArray();
  }
}
