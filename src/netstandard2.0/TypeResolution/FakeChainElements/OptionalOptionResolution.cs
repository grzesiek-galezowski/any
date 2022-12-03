using System;
using System.Linq;
using System.Reflection;
using Core.NullableReferenceTypesExtensions;
using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution.FakeChainElements;

public class OptionalOptionResolution : IResolution
{
  public bool AppliesTo(Type type)
  {
    return type is { Namespace: "Optional", Name: "Option`1" };
  }

  public object Apply(InstanceGenerator gen, GenerationRequest request, Type type)
  {
    //bug use SmartType here similarly to Newtonsoft ones
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
    return result.OrThrow();

  }
}
