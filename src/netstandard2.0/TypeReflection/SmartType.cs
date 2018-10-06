using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using TddXt.CommonTypes;
using TddXt.TypeReflection.ImplementationDetails;
using TddXt.TypeReflection.ImplementationDetails.ConstructorRetrievals;
using TddXt.TypeReflection.Interfaces;

namespace TddXt.TypeReflection
{
  public interface ISmartType : IType, IConstructorQueries
  {
    IEnumerable<IConstructorWrapper> FactoryMethods();
  }

  public class SmartType : ISmartType
  {
    private readonly ConstructorRetrieval _constructorRetrieval;
    private readonly Type _type;
    private readonly TypeInfo _typeInfo;

    public SmartType(Type type, ConstructorRetrieval constructorRetrieval)
    {
      _type = type;
      _constructorRetrieval = constructorRetrieval;
      _typeInfo = _type.GetTypeInfo();
    }

    public bool HasPublicParameterlessConstructor()
    {
      return GetPublicParameterlessConstructor().HasValue
             || _typeInfo.IsPrimitive 
             || _typeInfo.IsAbstract;
    }

    public IEnumerable<IConstructorWrapper> FactoryMethods()
    {
      var factoryMethods = TryToObtainPublicStaticFactoryMethodWithoutRecursion()
        .Where(m => m.HasNonPointerArgumentsOnly() && !m.IsParameterless())
        .OrderBy(m => m.GetParametersCount());
      return factoryMethods;
    }


    public Maybe<IConstructorWrapper> GetNonPublicParameterlessConstructorInfo()
    {
      var constructorInfo = _type.GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, Type.EmptyTypes, null);
      if (constructorInfo != null)
      {
        return Maybe.Wrap(DefaultParameterlessConstructor.ForOrdinaryType(constructorInfo));
      }
      else
      { 
        return Maybe<IConstructorWrapper>.Not;
      }
    }

    public Maybe<IConstructorWrapper> GetPublicParameterlessConstructor()
    {

      var constructorInfo = _type.GetConstructor(
        BindingFlags.Public | BindingFlags.Instance, null, Type.EmptyTypes, null);
      if (constructorInfo == null)
      {
        return Maybe<IConstructorWrapper>.Not;
      }
      else
      {
        return Maybe.Wrap(DefaultParameterlessConstructor.ForOrdinaryType(constructorInfo));
      }
    }

    public bool IsImplementationOfOpenGeneric(Type openGenericType)
    {
      return _typeInfo.GetInterfaces().Any(
        ifaceType => IsOpenGeneric(ifaceType, openGenericType));
    }

    public bool IsOpenGeneric(Type openGenericType)
    {
      return IsOpenGeneric(_typeInfo, openGenericType);
    }

    public bool IsConcrete()
    {
      return !_typeInfo.IsAbstract && !_typeInfo.IsInterface;
    }

    public Maybe<IConstructorWrapper> PickConstructorWithLeastNonPointersParameters()
    {
      IConstructorWrapper leastParamsConstructor = null;

      var constructors = For(_type).GetAllPublicConstructors();
      var numberOfParams = int.MaxValue;

      foreach (var typeConstructor in constructors)
      {
        if (
          typeConstructor.HasNonPointerArgumentsOnly()
          && typeConstructor.HasLessParametersThan(numberOfParams))
        {
          leastParamsConstructor = typeConstructor;
          numberOfParams = typeConstructor.GetParametersCount();
        }
      }

      return Maybe.Wrap(leastParamsConstructor);
    }

    public IEnumerable<IConstructorWrapper> GetAllPublicConstructors()
    {
      return _constructorRetrieval.RetrieveFrom(this);
    }

    public List<IConstructorWrapper> TryToObtainInternalConstructorsWithoutRecursiveArguments()
    {
      return TryToObtainNonPublicConstructors(ConstructorWrapper.IsInternal).Where(c => c.IsNotRecursive()).ToList();
    }

    public IEnumerable<IConstructorWrapper> TryToObtainPublicConstructorsWithoutRecursiveArguments()
    {
      return TryToObtainPublicConstructors().Where(c => c.IsNotRecursive());
    }

    public IEnumerable<IConstructorWrapper> TryToObtainPublicConstructorsWithRecursiveArguments()
    {
      return TryToObtainPublicConstructors().Where(c => c.IsRecursive());
    }

    public IEnumerable<IConstructorWrapper> TryToObtainInternalConstructorsWithRecursiveArguments()
    {
      return TryToObtainNonPublicConstructors(ConstructorWrapper.IsInternal).Where(c => c.IsRecursive()).ToList();
    }

    public IEnumerable<IConstructorWrapper> TryToObtainPrimitiveTypeConstructor()
    {
      return DefaultParameterlessConstructor.ForValue(_type);
    }

    public IEnumerable<IConstructorWrapper> TryToObtainPublicStaticFactoryMethodWithoutRecursion()
    {
      return _typeInfo.GetMethods(BindingFlags.Static | BindingFlags.Public)
        .Where(m => !m.IsSpecialName)
        .Where(IsNotImplicitCast)
        .Where(IsNotExplicitCast)
        .Where(IsNotParseMethod)
        .Select(ConstructorWrapper.FromStaticMethodInfo)
        .Where(c => c.IsFactoryMethod());
    }

    public IEnumerable<IConstructorWrapper> TryToObtainPrivateAndProtectedConstructorsWithoutRecursiveArguments()
    {
      return TryToObtainNonPublicConstructors(ConstructorWrapper.IsPrivateOrProtected).Where(c => c.IsNotRecursive()).ToList();
    }

    public IEnumerable<IFieldWrapper> GetAllPublicInstanceFields()
    {
      return _typeInfo.GetFields(
        BindingFlags.Public | BindingFlags.Instance).Select(f => new FieldWrapper(f));
    }

    public IEnumerable<IPropertyWrapper> GetPublicInstanceWritableProperties()
    {
      return _typeInfo.GetProperties(BindingFlags.Public | BindingFlags.Instance)
        .Where(p => p.CanWrite)
        .Select(p => new PropertyWrapper(p));
    }

    public IEnumerable<IMethod> GetAllPublicInstanceMethodsWithReturnValue()
    {
      return _typeInfo.GetMethods(BindingFlags.Public | BindingFlags.Instance)
        .Where(p => p.ReturnType != typeof(void)).
        Select(p => new SmartMethod(p));
    }
    //TODO even strict mocks can be done this way...

    public bool IsException()
    {
      return _type == typeof(Exception) ||
        _typeInfo.IsSubclassOf(typeof(Exception));
    }

    public bool HasPublicConstructorCountOfAtMost(int i)
    {
      return GetAllPublicConstructors().Count() <= i;
    }

    public void AssertMatchesTypeOf(object instance)
    {
      instance.GetType().Should().Be(_type);
    }

    private static bool IsOpenGeneric(Type checkedType, Type openGenericType)
    {
      return checkedType.GetTypeInfo().IsGenericType && 
             checkedType.GetGenericTypeDefinition() == openGenericType;
    }

    public static ISmartType For(Type type)
    {
      return new SmartType(type, new ConstructorRetrievalFactory().Create());
    }

    private List<IConstructorWrapper> TryToObtainNonPublicConstructors(Func<ConstructorInfo, bool> accessCriteria)
    {
      var constructorInfos = _typeInfo.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic);
      var enumerable = constructorInfos.Where(accessCriteria);

      var wrappers = enumerable.Select(c => (IConstructorWrapper) (ConstructorWrapper.FromConstructorInfo(c))).ToList();
      return wrappers;
    }


    public List<ConstructorWrapper> TryToObtainPublicConstructors()
    {
      return _typeInfo.GetConstructors(BindingFlags.Public | BindingFlags.Instance)
        .Select(ConstructorWrapper.FromConstructorInfo).ToList();
    }

    private static bool IsNotExplicitCast(MethodInfo mi)
    {
      return !string.Equals(mi.Name, "op_Explicit", StringComparison.Ordinal);
    }

    private static bool IsNotParseMethod(MethodInfo mi)
    {
      return !mi.Name.Contains("Parse");
    }

    private static bool IsNotImplicitCast(MethodInfo mi)
    {
      return !string.Equals(mi.Name, "op_Implicit", StringComparison.Ordinal);
    }
  }

}