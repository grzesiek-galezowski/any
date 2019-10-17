using System;
using TddXt.AnyExtensibility;

namespace TddXt.AnyGenerators.Generic.ImplementationDetails
{
  [Serializable]
  public class SynchronizedInstanceGenerator : CustomizableInstanceGenerator, BasicGenerator
  {
    private readonly AllGenerator _allGenerator;
    private readonly object _syncRoot;

    public SynchronizedInstanceGenerator(AllGenerator allGenerator, object syncRoot)
    {
      _allGenerator = allGenerator;
      _syncRoot = syncRoot;
    }

    public T Instance<T>()
    {
      lock (_syncRoot)
      {
        return _allGenerator.Instance<T>();
      }
    }

    public T Instance<T>(params GenerationCustomization[] customizations)
    {
      lock (_syncRoot)
      {
        return _allGenerator.Instance<T>(customizations);
      }
    }

    public T InstanceOf<T>(InlineGenerator<T> gen)
    {
      lock (_syncRoot)
      {
        return _allGenerator.InstanceOf(gen);
      }
    }

    public T ValueOtherThan<T>(params T[] omittedValues)
    {
      lock (_syncRoot)
      {
        return _allGenerator.ValueOtherThan(omittedValues);
      }
    }

    public T Value<T>(GenerationTrace trace)
    {
      lock (_syncRoot)
      {
        return _allGenerator.Value<T>(trace);
      }

    }

    public T Value<T>(T seed, GenerationTrace trace)
    {
      lock (_syncRoot)
      {
        return _allGenerator.Value<T>(seed, trace);
      }

    }

    public T OtherThan<T>(params T[] omittedValues)
    {
      lock (_syncRoot)
      {
        return _allGenerator.OtherThan(omittedValues);
      }

    }

    public object Instance(Type type, GenerationTrace trace)
    {
      lock (_syncRoot)
      {
        return _allGenerator.Instance(type, trace);
      }

    }

    public T Dummy<T>(GenerationTrace trace)
    {
      lock (_syncRoot)
      {
        return _allGenerator.Dummy<T>(trace);
      }

    }

    public T Instance<T>(GenerationTrace trace)
    {
      lock (_syncRoot)
      {
        return _allGenerator.Instance<T>(trace);
      }
    }

    public object Instance(Type type, GenerationTrace trace, GenerationCustomization[] customizations)
    {
      lock (_syncRoot)
      {
        return _allGenerator.Instance(type, trace, customizations);
      }
    }

    public T Instance<T>(GenerationTrace trace, GenerationCustomization[] customizations)
    {
      lock (_syncRoot)
      {
        return _allGenerator.Instance<T>(trace, customizations);
      }
    }

    public T Value<T>(GenerationTrace trace, GenerationCustomization[] customizations)
    {
      lock (_syncRoot)
      {
        return _allGenerator.Value<T>(trace, customizations);
      }
    }
  }
}