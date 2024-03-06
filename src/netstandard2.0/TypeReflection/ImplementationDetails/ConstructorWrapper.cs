using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Core.NullableReferenceTypesExtensions;
using TddXt.AnyExtensibility;
using TddXt.TypeReflection.Interfaces;

namespace TddXt.TypeReflection.ImplementationDetails;

public class ConstructorWrapper : IConstructorWrapper
{
  private readonly MethodBase _constructor;
  private readonly bool _hasAbstractOrInterfaceArguments;
  private readonly Func<object[], object> _invocation;
  private readonly ParameterInfo[] _parameters;
  private readonly IEnumerable<TypeInfo> _parameterTypes;
  private readonly Type _returnType;

  public ConstructorWrapper(
    MethodBase constructor, 
    Func<object[], object> invocation, 
    ParameterInfo[] parameters, 
    Type returnType)
  {
    _constructor = constructor;
    _parameters = parameters;
    _returnType = returnType;
    _parameterTypes = _parameters.Select(p => p.ParameterType.GetTypeInfo());
    _hasAbstractOrInterfaceArguments =
      _parameterTypes.Any(type => type.IsAbstract || type.IsInterface);
    _invocation = invocation;
  }

  public bool HasNonPointerArgumentsOnly()
  {
    if(!_parameterTypes.Any(type => type.IsPointer))
    {
      return true;
    }
    else
    {
      return false;
    }
  }

  public bool HasLessParametersThan(int numberOfParams)
  {
    if (_parameters.Count() < numberOfParams)
    {
      return true;
    }
    else
    {
      return false;
    }
  }

  public int GetParametersCount()
  {
    return _parameters.Count();
  }

  public bool HasAbstractOrInterfaceArguments()
  {
    return _hasAbstractOrInterfaceArguments;
  }

  public List<object> GenerateAnyParameterValues(Func<Type, GenerationRequest, object> instanceGenerator,
    GenerationRequest request)
  {
    var constructorValues = new List<object>();

    foreach (var constructorParam in _parameterTypes)
    {
      if (IsPassedByReference(constructorParam))
      {
        constructorValues.Add(instanceGenerator(GetNonRefFromRefType(constructorParam), request));
      }
      else
      {
        constructorValues.Add(instanceGenerator(constructorParam, request));
      }
    }
    return constructorValues;
  }

  private static Type GetNonRefFromRefType(TypeInfo constructorParam)
  {
    var constructorParamFullName = constructorParam.FullName.OrThrow();
    var typeName = constructorParamFullName.Replace("&", "");
    return Type.GetType(typeName).OrThrow();
  }

  //e.g. an "in" struct
  private static bool IsPassedByReference(TypeInfo constructorParam)
  {
    return constructorParam.IsByRef;
  }

  public bool IsParameterless()
  {
    return GetParametersCount() == 0;
  }

  public object InvokeWithParametersCreatedBy(Func<Type, GenerationRequest, object> instanceGenerator,
    GenerationRequest request)
  {
    return _invocation(GenerateAnyParameterValues(instanceGenerator, request).ToArray());
  }

  public bool IsInternal()
  {
    return IsInternal(_constructor);
  }

  public bool IsNotRecursive()
  {
    return !HasAnyArgumentOfType(_returnType);
  }

  public bool IsRecursive()
  {
    return !IsNotRecursive();
  }

  public object Invoke(IEnumerable<object> parameters)
  {
    return InvokeWith(parameters);
  }

  public IEnumerable<ParameterInfo> Parameters => _parameters;

  public void LogInScopeOf(GenerationRequest request)
  {
    request.Trace.ChosenConstructor(_constructor.Name, _parameterTypes);
  }

  public static ConstructorWrapper FromConstructorInfo(ConstructorInfo constructor)
  {
    return new ConstructorWrapper(constructor, constructor.Invoke, constructor.GetParameters(), constructor.DeclaringType.OrThrow());
  }

  public static ConstructorWrapper FromStaticMethodInfo(MethodInfo m)
  {
    return new ConstructorWrapper(m, args => m.Invoke(null, args).OrThrow(), m.GetParameters(), m.ReturnType);
  }

  public object InvokeWith(IEnumerable<object> constructorParameters)
  {
    return _invocation(constructorParameters.ToArray());
  }

  public override string ToString()
  {
    var description = _constructor.DeclaringType.OrThrow().Name + "(";

    int parametersCount = GetParametersCount();
    for (int i = 0; i < parametersCount; ++i)
    {
      description += GetDescriptionFor(_parameters[i]) + Separator(i, parametersCount);
    }

    description += ")";

    return description;
  }

  private static string Separator(int i, int parametersCount)
  {
    return ((i == parametersCount - 1) ? "" : ", ");
  }

  private static string GetDescriptionFor(ParameterInfo parameter)
  {
    return parameter.ParameterType.Name + " " + parameter.Name;
  }

  public bool HasAnyArgumentOfType(Type type)
  {
    return _parameters.Any(p => p.ParameterType == type);
  }

  public static bool IsInternal(MethodBase c)
  {
    return c.IsAssembly && !c.IsPublic && !c.IsStatic;
  }

  public bool IsFactoryMethod()
  {
    return _constructor.DeclaringType == _returnType;
  }

  public static bool IsPrivateOrProtected(ConstructorInfo arg)
  {
    return arg.IsPrivate || arg.IsFamily;
  }
}
