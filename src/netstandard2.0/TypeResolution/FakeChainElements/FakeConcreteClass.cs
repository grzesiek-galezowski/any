using System;
using System.Diagnostics;
using System.Reflection;
using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution.FakeChainElements
{
  public class FakeConcreteClass<T> : IResolution<T>
  {
    private readonly FallbackTypeGenerator<T> _fallbackTypeGenerator;

    public FakeConcreteClass(FallbackTypeGenerator<T> fallbackTypeGenerator)
    {
      _fallbackTypeGenerator = fallbackTypeGenerator;
    }

    public bool Applies()
    {
      return true;
    }

    public T Apply(InstanceGenerator instanceGenerator, GenerationRequest request)
    {
      try
      {
        return instanceGenerator.Value<T>(request);
      }
      catch (ThirdPartyGeneratorFailed e)
      {
        request.Trace.ThirdPartyGeneratorFailedTryingFallback(e);
        return _fallbackTypeGenerator.GenerateInstance(instanceGenerator, request);
      }
      catch (TargetInvocationException e)
      {
        if (Debugger.IsAttached)
        {
          Console.WriteLine(e);
        }
        return _fallbackTypeGenerator.GenerateInstance(instanceGenerator, request);
      }
    }
  }
}