using System;
using TddEbook.TypeReflection;

namespace TddEbook.TddToolkit.Generators
{
  public class AllGenerator : InstanceGenerator
  {
    public AllGenerator(
      ValueGenerator valueGenerator, 
      ProxyBasedGenerator genericGenerator)
    {
      _valueGenerator = valueGenerator;
      _genericGenerator = genericGenerator;
    }

    public const int Many = 3;

    private readonly ValueGenerator _valueGenerator;
    private readonly ProxyBasedGenerator _genericGenerator;

    public T ValueOtherThan<T>(params T[] omittedValues)
    {
      return _valueGenerator.ValueOtherThan(omittedValues);
    }

    public T Value<T>()
    {
      return _valueGenerator.Value<T>();
    }

    public T Value<T>(T seed)
    {
      return _valueGenerator.Value(seed);
    }

    public object Instance(Type type)
    {
      return _genericGenerator.Instance(type);
    }

    public T Instance<T>()
    {
      return _genericGenerator.Instance<T>();
    }

    public T Dummy<T>()
    {
      return _genericGenerator.Dummy<T>();
    }

    public T OtherThan<T>(params T[] omittedValues)
    {
      return _genericGenerator.OtherThan(omittedValues);
    }

    public T Exploding<T>() where T : class
    {
      return _genericGenerator.Exploding<T>();
    }
  }
}