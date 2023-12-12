using System;
using System.Collections.Generic;
using System.Reflection;
using Core.NullableReferenceTypesExtensions;
using TddXt.AnyExtensibility;
using TddXt.TypeReflection.Interfaces;

namespace TddXt.TypeReflection.ImplementationDetails;

public class DefaultParameterlessConstructor(Func<object> creation) : IConstructorWrapper
{
  public bool HasNonPointerArgumentsOnly()
  {
    return true;
  }

  public bool HasLessParametersThan(int numberOfParams)
  {
    return true;
  }

  public int GetParametersCount()
  {
    return 0;
  }

  public bool HasAbstractOrInterfaceArguments()
  {
    return false;
  }

  public List<object> GenerateAnyParameterValues(Func<Type, GenerationRequest, object> instanceGenerator,
    GenerationRequest request)
  {
    return new List<object>();
  }

  public bool IsParameterless()
  {
    return true;
  }

  public object InvokeWithParametersCreatedBy(Func<Type, GenerationRequest, object> instanceGenerator,
    GenerationRequest request)
  {
    return creation.Invoke();
  }

  public bool IsInternal()
  {
    return false; //?? actually, this is not right...
  }

  public bool IsNotRecursive()
  {
    return true;
  }

  public bool IsRecursive()
  {
    return false;
  }

  public object Invoke(IEnumerable<object> parameters)
  {
    return creation.Invoke();
  }

  public IEnumerable<ParameterInfo> Parameters { get; } = new List<ParameterInfo>();

  public void LogInScopeOf(GenerationRequest request)
  {
    request.Trace.ChosenParameterlessConstructor();
  }

  public static IConstructorWrapper ForOrdinaryType(ConstructorInfo constructorInfo)
  {
    return new DefaultParameterlessConstructor(() => constructorInfo.Invoke(new object[]{}));
  }

  public static IEnumerable<IConstructorWrapper> ForValue(Type type)
  {
    return new [] { new DefaultParameterlessConstructor(() => Activator.CreateInstance(type).OrThrow())};
  }
}
