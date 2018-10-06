using NSubstitute;
using TddXt.AnyExtensibility;
using TddXt.TypeReflection;

namespace TddXt.AnyGenerators.NSubstitute
{
  public class SubstituteGenerator<T> : InlineGenerator<T> where T : class
  {
    //todo move substitute generator to a separate nuget project
    public T GenerateInstance(InstanceGenerator instanceGenerator, GenerationTrace trace) 
    {
      var type = typeof(T);
      var sub = Substitute.For<T>();

      var methods = SmartType.For(type).GetAllPublicInstanceMethodsWithReturnValue();

      foreach (var method in methods)
      {
        method.InvokeWithAnyArgsOn(sub, argType => instanceGenerator.Instance(argType, trace))
          .ReturnsForAnyArgs(method.GenerateAnyReturnValue(returnType => instanceGenerator.Instance(returnType, trace)));
      }

      return sub;
    }
  }
}