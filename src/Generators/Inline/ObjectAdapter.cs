using System;
using System.Reflection;
using TddEbook.TypeReflection;

namespace TddEbook.TddToolkit.Generators
{
  public class ObjectAdapter : InlineGenerator<object>
  {
    private readonly object _inlineGenerator;
    private readonly MethodInfo _methodInfo;
    private static readonly GenericMethodProxyCalls GenericMethodProxyCalls = new GenericMethodProxyCalls();

    public ObjectAdapter(object inlineGenerator, MethodInfo methodInfo)
    {
      _inlineGenerator = inlineGenerator;
      _methodInfo = methodInfo;
    }

    public object GenerateInstance(InstanceGenerator instanceGenerator)
    {
      return _methodInfo.Invoke(_inlineGenerator, new object[] {instanceGenerator});
    }

    public static InlineGenerator<object> For(string methodName, Type type)
    {
      var inlineGenerator = GenericMethodProxyCalls
        .ResultOfGenericVersionOfStaticMethod<InlineGenerators>(type, methodName);

      return new ObjectAdapter(inlineGenerator, GenerateInstanceMethodInfo(inlineGenerator));
    }

    public static InlineGenerator<object> For(string methodName, Type type1, Type type2)
    {
      var genericMethodProxyCalls = new GenericMethodProxyCalls();
      var inlineGenerator = genericMethodProxyCalls
        .ResultOfGenericVersionOfStaticMethod<InlineGenerators>(type1, type2, methodName);
      return new ObjectAdapter(inlineGenerator, GenerateInstanceMethodInfo(inlineGenerator));
    }

    private static MethodInfo GenerateInstanceMethodInfo(object inlineGenerator)
    {
      return inlineGenerator.GetType().GetMethod(nameof(InlineGenerator<object>.GenerateInstance));
    }

  }
}