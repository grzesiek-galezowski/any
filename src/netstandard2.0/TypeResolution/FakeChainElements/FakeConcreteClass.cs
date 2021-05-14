using System;
using System.Diagnostics;
using System.Reflection;
using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution.FakeChainElements
{
  public class FakeConcreteClass : IResolution
  {
    private readonly FallbackTypeGenerator _fallbackTypeGenerator;

    public FakeConcreteClass(FallbackTypeGenerator fallbackTypeGenerator)
    {
      _fallbackTypeGenerator = fallbackTypeGenerator;
    }

    public bool AppliesTo(Type type)
    {
      return true;
    }

    public object Apply(InstanceGenerator instanceGenerator, GenerationRequest request, Type type)
    {

      object? result;
      try
      {
        result = instanceGenerator.Value(type, request);
      }
      catch (ThirdPartyGeneratorFailed e)
      {
        request.Trace.ThirdPartyGeneratorFailedTryingFallback(e);
        result = _fallbackTypeGenerator.GenerateCustomizedInstance(instanceGenerator, request);
      }
      catch (TargetInvocationException e)
      {
        if (Debugger.IsAttached)
        {
          Console.WriteLine(e);
        }
        result = _fallbackTypeGenerator.GenerateCustomizedInstance(instanceGenerator, request);
      }
      return result;
    }
  }
}
