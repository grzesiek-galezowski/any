using System;

namespace TddXt.AnyExtensibility;

public class GenerationFailedException(GenerationRequest request, Exception exception)
  : Exception(Environment.NewLine + request.Trace.ToString(), exception);
