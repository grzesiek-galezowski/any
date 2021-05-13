using System;
using System.Diagnostics;
using System.Reflection;
using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution.FakeChainElements
{
  public class FakeConcreteClass<T> : IResolution<T>
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

    public T Apply(InstanceGenerator instanceGenerator, GenerationRequest request, Type type)
    {
      try
      {
        //bug make non-generic version
        return instanceGenerator.Value<T>(request);
      }
      catch (ThirdPartyGeneratorFailed e)
      {
        request.Trace.ThirdPartyGeneratorFailedTryingFallback(e);
        return (T)_fallbackTypeGenerator.GenerateCustomizedInstance(instanceGenerator, request);
      }
      catch (TargetInvocationException e)
      {
        if (Debugger.IsAttached)
        {
          Console.WriteLine(e);
        }
        return (T)_fallbackTypeGenerator.GenerateCustomizedInstance(instanceGenerator, request);
      }
    }
  }
}
