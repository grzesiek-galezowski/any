using System;
using System.Reflection;
using Core.NullableReferenceTypesExtensions;
using TddXt.AnyExtensibility;
using TddXt.TypeReflection;

namespace TddXt.TypeResolution.FakeChainElements.InlineGeneratorTypes.Generic;

public class ObjectAdapter(object inlineGenerator, MethodInfo methodInfo) : InlineGenerator<object>
{
  private static readonly GenericMethodProxyCalls GenericMethodProxyCalls = new();

  public object GenerateInstance(InstanceGenerator instanceGenerator, GenerationRequest request)
  {
    return methodInfo.Invoke(
        inlineGenerator,
        new object[] { instanceGenerator, request })
      .OrThrow();
  }


  public static InlineGenerator<object> For<T>(string methodName, Type type1, Type type2)
  {
    var genericMethodProxyCalls = new GenericMethodProxyCalls();
    var inlineGenerator = genericMethodProxyCalls
      .ResultOfGenericVersionOfStaticMethod<T>(type1, type2, methodName);
    return new ObjectAdapter(inlineGenerator, GenerateInstanceMethodInfo(inlineGenerator));
  }

  public static InlineGenerator<object> For<T>(string methodName, Type type)
  {
    var inlineGenerator = GenericMethodProxyCalls
      .ResultOfGenericVersionOfStaticMethod<T>(type, methodName);

    return new ObjectAdapter(inlineGenerator, GenerateInstanceMethodInfo(inlineGenerator));
  }

  private static MethodInfo GenerateInstanceMethodInfo(object inlineGenerator)
  {
    return inlineGenerator
      .GetType()
      .GetMethod(nameof(InlineGenerator<object>.GenerateInstance))
      .OrThrow();
  }
}
