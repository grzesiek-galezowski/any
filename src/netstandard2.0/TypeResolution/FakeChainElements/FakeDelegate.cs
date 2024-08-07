﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Core.NullableReferenceTypesExtensions;
using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution.FakeChainElements;

public class FakeDelegate : IResolution
{
  public bool AppliesTo(Type type)
  {
    return type.IsSubclassOf(typeof(Delegate));
  }

  public object Apply(InstanceGenerator instanceGenerator, GenerationRequest request, Type type)
  {
    var methodInfo = type.GetMethods().First(m => m.Name.Equals("Invoke"));
    var parameters = methodInfo.GetParameters();
    object? result;
    if (methodInfo.ReturnType != typeof(void))
    {
      var instance = CreateGenericDelegatesForFunction(instanceGenerator, methodInfo, request);
      result = Delegate.CreateDelegate(type, instance, instance.GetType().GetMethod("Get" + parameters.Length).OrThrow());
    }
    else
    {
      var instance = CreateGenericDelegatesForAction(methodInfo);
      result = Delegate.CreateDelegate(type, instance, instance.GetType().GetMethod("Do" + parameters.Length).OrThrow());
    }

    return result;
  }

  private static object CreateGenericDelegatesForFunction(InstanceGenerator instanceGenerator, MethodInfo methodInfo,
    GenerationRequest request)
  {
    var fullSignatureTypes = ReturnTypeOf(methodInfo).Concat(ParameterTypes(methodInfo));
    return CreateGenericDelegatesObjectForConcreteTypes(fullSignatureTypes, WithArgumentGeneratedBy(instanceGenerator, methodInfo, request));
  }

  private static object CreateGenericDelegatesForAction(MethodBase methodInfo)
  {
    var dummyReturnType = typeof(object);
    var dummyReturnValueForConstructorArgument = new object();

    var fullSignatureTypes = new List<Type> { dummyReturnType }.Concat(ParameterTypes(methodInfo));
    return CreateGenericDelegatesObjectForConcreteTypes(fullSignatureTypes, [dummyReturnValueForConstructorArgument]);
  }

  private static object CreateGenericDelegatesObjectForConcreteTypes(IEnumerable<Type> fullSignatureTypes, object[] withArgumentGeneratedBy)
  {
    var genericFunctionsType = typeof(GenericDelegates<,,,,,,,,,,>);
    var fullTypeArguments = fullSignatureTypes.Concat(
      RepeatDummyTypeArgToFill(fullSignatureTypes, genericFunctionsType.GetGenericArguments().Length));
    return genericFunctionsType.MakeGenericType(fullTypeArguments.ToArray())
      .GetConstructors().First().Invoke(withArgumentGeneratedBy);
  }

  private static object[] WithArgumentGeneratedBy(InstanceGenerator instanceGenerator, MethodInfo methodInfo,
    GenerationRequest request)
  {
    return [instanceGenerator.Instance(methodInfo.ReturnType, request)];
  }

  private static IEnumerable<Type> RepeatDummyTypeArgToFill(IEnumerable<Type> fullSignatureTypes, int length)
  {
    return Enumerable.Repeat(typeof(object), length - fullSignatureTypes.Count());
  }

  private static IEnumerable<Type> ParameterTypes(MethodBase methodInfo)
  {
    return methodInfo.GetParameters().Select(pi => pi.ParameterType);
  }

  private static IEnumerable<Type> ReturnTypeOf(MethodInfo methodInfo)
  {
    return [methodInfo.ReturnType];
  }
}

public class GenericDelegates<TReturn, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(TReturn value)
{
  public TReturn Get0() => value;
  public TReturn Get1(T1 a) => value;
  public TReturn Get2(T1 a, T2 b) => value;
  public TReturn Get3(T1 a, T2 b, T3 c) => value;
  public TReturn Get4(T1 a, T2 b, T3 c, T4 d) => value;
  public TReturn Get5(T1 a, T2 b, T3 c, T4 d, T5 e) => value;
  public TReturn Get6(T1 a, T2 b, T3 c, T4 d, T5 e, T6 f) => value;
  public TReturn Get7(T1 a, T2 b, T3 c, T4 d, T5 e, T6 f, T7 g) => value;
  public TReturn Get8(T1 a, T2 b, T3 c, T4 d, T5 e, T6 f, T7 g, T8 h) => value;
  public TReturn Get9(T1 a, T2 b, T3 c, T4 d, T5 e, T6 f, T7 g, T8 h, T9 i) => value;
  public TReturn Get10(T1 a, T2 b, T3 c, T4 d, T5 e, T6 f, T7 g, T8 h, T9 i, T10 j) => value;

  public void Do0()
  {
  }

  public void Do1(T1 a)
  {
  }

  public void Do2(T1 a, T2 b)
  {
  }

  public void Do3(T1 a, T2 b, T3 c)
  {
  }

  public void Do4(T1 a, T2 b, T3 c, T4 d)
  {

  }

  public void Do5(T1 a, T2 b, T3 c, T4 d, T5 e)
  {
  }

  public void Do6(T1 a, T2 b, T3 c, T4 d, T5 e, T6 f)
  {
  }

  public void Do7(T1 a, T2 b, T3 c, T4 d, T5 e, T6 f, T7 g)
  {
  }

  public void Do8(T1 a, T2 b, T3 c, T4 d, T5 e, T6 f, T7 g, T8 h)
  {
  }

  public void Do9(T1 a, T2 b, T3 c, T4 d, T5 e, T6 f, T7 g, T8 h, T9 i)
  {
  }

  public void Do10(T1 a, T2 b, T3 c, T4 d, T5 e, T6 f, T7 g, T8 h, T9 i, T10 j)
  {
  }
}
