using System;
using System.Collections.Generic;
using System.Reflection;
using TddXt.AnyExtensibility;

namespace AnySpecification;

public class GenericListCustomization : GenerationCustomization
{
  public bool AppliesTo(Type type)
  {
    return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>);
  }

  public object Generate(Type type, InstanceGenerator gen, GenerationRequest request)
  {
    var list = Activator.CreateInstance(type);
    var addMethod = type.GetMethod("Add", BindingFlags.Instance | BindingFlags.Public);
    var elementInstance = gen.Instance(type.GetGenericArguments()[0], request);
    addMethod!.Invoke(list, new[] {elementInstance});
    addMethod!.Invoke(list, new[] {elementInstance});
    addMethod!.Invoke(list, new[] {elementInstance});
    addMethod!.Invoke(list, new[] {elementInstance});
    return list!;
  }
}