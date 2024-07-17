using System;
using TddXt.AnyExtensibility;
using TddXt.TypeReflection;

namespace TddXt.TypeResolution.FakeChainElements;

public class JContainerResolution : IResolution
{
  public bool AppliesTo(Type type)
  {
    return 
      NewtonsoftJsonTypePredicates.IsJObject(type) || 
      NewtonsoftJsonTypePredicates.IsJContainer(type);
  }

  public object Apply(InstanceGenerator gen, GenerationRequest request, Type type)
  {
    var newtonsoftJsonAssembly = type.Assembly;
    var objectType = SmartType.QueryExportedTypes(
      newtonsoftJsonAssembly, 
      NewtonsoftJsonTypePredicates.IsJObject);
    var jPropertyType = SmartType.QueryExportedTypes(
      newtonsoftJsonAssembly, 
      NewtonsoftJsonTypePredicates.IsJProperty);
    var argument = jPropertyType.GenerateInstanceWith(gen, request);
    return objectType.CreateInstance([typeof(object)], [argument]);
  }
}
