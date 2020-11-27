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

    public T Apply(InstanceGenerator instanceGenerator, GenerationTrace trace)
    {
      try
      {
        return instanceGenerator.Value<T>(trace);
      }
      catch (ThirdPartyGeneratorFailed e)
      {
        trace.ThirdPartyGeneratorFailedTryingFallback(e);
        return _fallbackTypeGenerator.GenerateInstance(instanceGenerator, trace);
      }
      catch (TargetInvocationException e)
      {
        if (Debugger.IsAttached)
        {
          Console.WriteLine(e);
        }
        return _fallbackTypeGenerator.GenerateInstance(instanceGenerator, trace);
      }
    }
  }
}