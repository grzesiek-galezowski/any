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
      //lock (_syncRoot)
      {
        return _allGenerator.Instance<T>();
      }
    }

    public T Instance<T>(params GenerationCustomization[] customizations)
    {
      //lock (_syncRoot)
      {
        return _allGenerator.Instance<T>(customizations);
      }
    }

    public T InstanceOf<T>(InlineGenerator<T> gen)
    {
      //lock (_syncRoot)
      {
        return _allGenerator.InstanceOf(gen);
      }
    }

    public T ValueOtherThan<T>(params T[] omittedValues)
    {
      //lock (_syncRoot)
      {
        return _allGenerator.ValueOtherThan(omittedValues);
      }
    }

    public T Value<T>(GenerationRequest request)
    {
      //lock (_syncRoot)
      {
        return _allGenerator.Value<T>(request);
      }

    }

    public T Value<T>(T seed, GenerationRequest request)
    {
      //lock (_syncRoot)
      {
        return _allGenerator.Value<T>(seed, request);
      }

    }

    public T OtherThan<T>(params T[] omittedValues)
    {
      //lock (_syncRoot)
      {
        return _allGenerator.OtherThan(omittedValues);
      }

    }

    public object OtherThan(Type type, object[] omittedValues, GenerationRequest request)
    {
      //lock (_syncRoot)
      {
        return _allGenerator.OtherThan(type, omittedValues, request);
      }
    }

    public object Instance(Type type, GenerationRequest request)
    {
      //lock (_syncRoot)
      {
        return _allGenerator.Instance(type, request);
      }

    }

    public T Dummy<T>(GenerationRequest request)
    {
      //lock (_syncRoot)
      {
        return _allGenerator.Dummy<T>(request);
      }

    }

    public T Instance<T>(GenerationRequest request)
    {
      //lock (_syncRoot)
      {
        return _allGenerator.Instance<T>(request);
      }
    }

    //bug there is a mess still with the customizations. The param should be removed,
    //bug but then the overload will be ambiguous. Maybe name this method differently for starters?
    public object Instance(Type type, GenerationRequest request, GenerationCustomization[] customizations)
    {
      //lock (_syncRoot)
      {
        return _allGenerator.Instance(type, request, customizations);
      }
    }

    public T Instance<T>(GenerationRequest request, GenerationCustomization[] customizations)
    {
      //lock (_syncRoot)
      {
        return _allGenerator.Instance<T>(request, customizations);
      }
    }

    public T Value<T>(GenerationRequest request, GenerationCustomization[] customizations)
    {
      //lock (_syncRoot)
      {
        return _allGenerator.Value<T>(request, customizations);
      }
    }
  }
}