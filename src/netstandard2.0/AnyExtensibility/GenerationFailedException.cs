using System;

namespace TddXt.AnyExtensibility;

public class GenerationFailedException : Exception
{
  public GenerationFailedException(GenerationRequest request, Exception exception)
    : base(Environment.NewLine + request.Trace.ToString(), exception)
  {

  }
}