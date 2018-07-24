using NSubstitute;
using TddXt.AnyExtensibility;
using TypeReflection;

namespace TddXt.AnyGenerators.NSubstitute
{
  public class SubstituteGenerator<T> : InlineGenerator<T> where T : class
  {
    //todo move substitute generator to a separate nuget project
    public T GenerateInstance(InstanceGenerator instanceGenerator) 
    {
      var type = typeof(T);
      var sub = Substitute.For<T>();

      var methods = SmartType.For(type).GetAllPublicInstanceMethodsWithReturnValue();

      foreach (var method in methods)
      {
        method.InvokeWithAnyArgsOn(sub, instanceGenerator.Instance)
          .ReturnsForAnyArgs(method.GenerateAnyReturnValue(instanceGenerator.Instance));
      }

      return sub;
    }
  }
}