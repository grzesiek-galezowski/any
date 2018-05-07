using System;
using System.Diagnostics;
using System.Reflection;
using TddEbook.TddToolkit.TypeResolution.Interfaces;
using TddEbook.TypeReflection;

namespace TddEbook.TddToolkit.TypeResolution.FakeChainElements
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
      return true; //TODO consider catching exception here instead of in Apply() and returning false, then have a fallback chain element
    }

    public T Apply(InstanceGenerator instanceGenerator)
    {
      try
      {
        return _valueGenerator.Value<T>();
      }
      catch (AutoFixture.ObjectCreationException e)
      {
        if (Debugger.IsAttached)
        {
          Console.WriteLine(e);
        }
        return _fallbackTypeGenerator.GenerateInstance(instanceGenerator);
      }
      catch (TargetInvocationException e)
      {
        if (Debugger.IsAttached)
        {
          Console.WriteLine(e);
        }
        return _fallbackTypeGenerator.GenerateInstance(instanceGenerator);
      }
    }
  }
}