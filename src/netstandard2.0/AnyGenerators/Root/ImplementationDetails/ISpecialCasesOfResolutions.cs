using System;
using TddXt.TypeResolution.FakeChainElements;

namespace TddXt.AnyGenerators.Root.ImplementationDetails;

public interface ISpecialCasesOfResolutions
{
  IResolution CreateResolutionOfKeyValuePair();
  IResolution CreateResolutionOf2GenericType(string className, params Type[] matchingTypes);
  IResolution CreateResolutionOf1GenericType(string resolvedTypeName, params Type[] genericTypes);
  IResolution CreateResolutionOfArray();
}
