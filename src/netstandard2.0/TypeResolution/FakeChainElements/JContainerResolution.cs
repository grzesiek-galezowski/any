using System;
using System.Linq;
using Core.NullableReferenceTypesExtensions;
using TddXt.AnyExtensibility;
using BindingFlags = System.Reflection.BindingFlags;

namespace TddXt.TypeResolution.FakeChainElements;

public class JContainerResolution : IResolution
{
  public bool AppliesTo(Type type)
  {
    return type is { Namespace: "Newtonsoft.Json.Linq", Name: "JObject" or "JContainer" };
  }

  public object Apply(InstanceGenerator gen, GenerationRequest request, Type type)
  {
    //bug move some of this to Smart Type
    var objectType = type.Assembly.ExportedTypes.Single(t => t is { Namespace: "Newtonsoft.Json.Linq", Name: "JObject" });
    var jPropertyType = type.Assembly.ExportedTypes.Single(t => t is { Namespace: "Newtonsoft.Json.Linq", Name: "JProperty" });
    var constructor = objectType.GetConstructor(BindingFlags.Public | BindingFlags.Instance, null, new[] { typeof(object) }, null).OrThrow();
    var argument = gen.Instance(jPropertyType, request);
    return constructor.Invoke(new[] { argument });
  }
}
