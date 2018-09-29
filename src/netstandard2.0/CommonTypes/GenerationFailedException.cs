using System;

namespace TddXt.CommonTypes
{
  public class GenerationFailedException : Exception
  {
    public GenerationFailedException(GenerationTrace trace, Exception exception)
      : base(Environment.NewLine + trace.ToString(), exception)
    {
      
    }
  }
}