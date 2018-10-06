using System;

namespace TddXt.AnyExtensibility
{
  public class GenerationFailedException : Exception
  {
    public GenerationFailedException(GenerationTrace trace, Exception exception)
      : base(Environment.NewLine + trace.ToString(), exception)
    {
      
    }
  }
}