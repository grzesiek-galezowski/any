using System;
using System.Linq;
using System.Reflection;
using TddXt.AnyExtensibility;
using TddXt.TypeResolution.FakeChainElements;

namespace TddXt.TypeResolution.HackedSpecialTypes
{
  public class OptionalOptionResolution<T> : IResolution<T>
  {
    public bool AppliesTo(Type type)
    {
      return typeof(T).Namespace == "Optional"
        && typeof(T).Name == "Option`1";
    }

    public T Apply(InstanceGenerator gen, GenerationRequest request, Type type1)
    {
      var type = typeof(T);
      var genericArgument = type.GetGenericArguments()[0];
      var elementInstance = gen.Instance(genericArgument, request);
      var factoryClass = type.Assembly.ExportedTypes.Single(t => t.Namespace == "Optional" && t.Name == "Option");
      var genericCreationMethod = factoryClass
        .GetMethods(BindingFlags.Static | BindingFlags.Public)
        .Where(info => info.Name == "Some")
        .Where(info => info.IsGenericMethod)
        .Single(info => info.GetGenericArguments().Length == 1);
      var someMethod = genericCreationMethod.MakeGenericMethod(genericArgument);
      var result = someMethod.Invoke(null, new[] { elementInstance });
      return (T)result;

    }
  }
}
