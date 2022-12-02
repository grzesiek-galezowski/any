using System;
using System.Linq;
using System.Reflection;
using Core.NullableReferenceTypesExtensions;
using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution.FakeChainElements;

public class JTokenResolution : IResolution
{
  public bool AppliesTo(Type type)
  {
    return type is { Namespace: "Newtonsoft.Json.Linq", Name: "JToken" or "JValue" };
  }

  public object Apply(InstanceGenerator gen, GenerationRequest request, Type type)
  {
    var constructorArg = gen.Instance(typeof(string), request);
    var factoryClass = type.Assembly.ExportedTypes.Single(t => t is { Namespace: "Newtonsoft.Json.Linq", Name: "JValue" });
    var constructor = factoryClass.GetConstructor(BindingFlags.Public | BindingFlags.Instance, null, new[] { typeof(string) }, null).OrThrow();
    return constructor.Invoke(new[] {constructorArg});
  }
}
