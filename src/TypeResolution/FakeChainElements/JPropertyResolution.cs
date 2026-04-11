using System;
using TddXt.AnyExtensibility;
using TddXt.TypeReflection;

namespace TddXt.TypeResolution.FakeChainElements;

public class JPropertyResolution : IResolution
{
  public bool AppliesTo(Type type)
  {
    return NewtonsoftJsonTypePredicates.IsJProperty(type);
  }

  public object Apply(InstanceGenerator gen, GenerationRequest request, Type type)
  {
    var objectType = SmartType.QueryExportedTypes(type.Assembly, NewtonsoftJsonTypePredicates.IsJProperty);
    var key = gen.Instance(typeof(string), request);
    var value = gen.Instance(typeof(string), request);
    return objectType.CreateInstance(
      [typeof(string), typeof(object)],
      [key, value]);
  }
}
