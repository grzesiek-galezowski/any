using System;
using TddXt.AnyExtensibility;
using TddXt.TypeReflection;

namespace TddXt.TypeResolution.FakeChainElements;

public class JTokenResolution : IResolution
{
  public bool AppliesTo(Type type)
  {
    return 
      NewtonsoftJsonTypePredicates.IsJToken(type) || 
      NewtonsoftJsonTypePredicates.IsJValue(type);
  }

  public object Apply(InstanceGenerator gen, GenerationRequest request, Type type)
  {
    var constructorArg = gen.Instance(typeof(string), request);
    var factoryClass = SmartType.QueryExportedTypes(type.Assembly, NewtonsoftJsonTypePredicates.IsJValue);
    return factoryClass.CreateInstance([typeof(object)], [constructorArg]);
  }
}
