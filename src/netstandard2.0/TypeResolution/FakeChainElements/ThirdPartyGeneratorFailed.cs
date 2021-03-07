using System;
using static System.Environment;

namespace TddXt.TypeResolution.FakeChainElements
{
  public class ThirdPartyGeneratorFailed : Exception
  {
    public ThirdPartyGeneratorFailed(Exception creationException)
          : base($"Third-party generation library failed: {NewLine}{NewLine}<<<<<<<<<<<<{NewLine}" + creationException.Message + $"<<<<<<<<<<<<{NewLine}", creationException)
    {

    }
  }
}