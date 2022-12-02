using System;
using System.Linq;
using Core.NullableReferenceTypesExtensions;
using TddXt.AnyExtensibility;
using BindingFlags = System.Reflection.BindingFlags;

namespace TddXt.TypeResolution.FakeChainElements;

public class JPropertyResolution : IResolution
{
  public bool AppliesTo(Type type)
  {
    return type is { Namespace: "Newtonsoft.Json.Linq", Name: "JProperty" };
  }

  public object Apply(InstanceGenerator gen, GenerationRequest request, Type type)
  {
    var objectType = type.Assembly.ExportedTypes.Single(t => t is { Namespace: "Newtonsoft.Json.Linq", Name: "JProperty" });
    var key = gen.Instance(typeof(string), request);
    var value = gen.Instance(typeof(string), request);
    var constructorInfo = objectType.GetConstructor(BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(string), typeof(object)}, null).OrThrow();
    return constructorInfo.Invoke(new[] { key, value });
  }
}
