using System;
using System.Diagnostics;
using System.Reflection;
using TddXt.AnyExtensibility;
using TddXt.CommonTypes;
using TddXt.TypeResolution.Interfaces;

namespace TddXt.TypeResolution.FakeChainElements
{
  public class FakeConcreteClass<T> : IResolution<T>
  {
    private readonly FallbackTypeGenerator<T> _fallbackTypeGenerator;
    private readonly IValueGenerator _valueGenerator;

    public FakeConcreteClass(
      FallbackTypeGenerator<T> fallbackTypeGenerator, 
      IValueGenerator valueGenerator)
    {
      _fallbackTypeGenerator = fallbackTypeGenerator;
      _valueGenerator = valueGenerator;
    }

    public bool Applies()
    {
      return true;
    }

    public T Apply(InstanceGenerator instanceGenerator, GenerationTrace trace)
    {
      try
      {
        return _valueGenerator.Value<T>();
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