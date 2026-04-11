using System;

namespace TddXt.TypeResolution.FakeChainElements.InlineGeneratorTypes;

public interface ISpecialCasesOfResolutions
{
  IResolution CreateResolutionOfKeyValuePair();
  IResolution CreateResolutionOf2GenericType(string className, params Type[] matchingTypes);
  IResolution CreateResolutionOf1GenericType(string resolvedTypeName, params Type[] genericTypes);
  IResolution CreateResolutionOfArray();
}
