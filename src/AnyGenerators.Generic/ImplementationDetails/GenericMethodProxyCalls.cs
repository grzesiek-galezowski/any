using System;
using System.Linq;
using System.Reflection;

namespace TddXt.AnyGenerators.Generic.ImplementationDetails
{
  public class GenericMethodProxyCalls
  {
    public object ResultOfGenericVersionOfMethod<T>(T instance, Type genericArgumentType, string name)
    {
      return ResultOfGenericVersionOfMethod(instance, genericArgumentType, name, new object[]{});
    }

    public object ResultOfGenericVersionOfMethod<T>(T instance, Type genericArgumentType, string name, params object[] parameters)
    {
      var method = FindEmptyGenericsInstanceMethod<T>(name, parameters.Length);

      var genericMethod = method.MakeGenericMethod(genericArgumentType);

      return genericMethod.Invoke(instance, parameters);

    }

    public object ResultOfGenericVersionOfStaticMethod<T>(Type genericArgumentType, string name)
    {
      return ResultOfGenericVersionOfStaticMethod<T>(genericArgumentType, name, new object[] { });
    }

    private object ResultOfGenericVersionOfStaticMethod<T>(Type genericArgumentType, string name, params object[] parameters)
    {
      var method = FindEmptyGenericsStaticMethod<T>(name, parameters.Length);

      var genericMethod = method.MakeGenericMethod(genericArgumentType);

      return genericMethod.Invoke(null, parameters);

    }

    public object ResultOfGenericVersionOfMethod<T>(
      T instance, Type type1, Type type2, string name)
    {
      return ResultOfGenericVersionOfMethod(instance, type1, type2, name, new object[]{});
    }

    public object ResultOfGenericVersionOfMethod<T>(
      T instance, Type type1, Type type2, string name, params object[] parameters)
    {
      var method = FindEmptyGenericsInstanceMethod<T>(name, parameters.Length);

      var genericMethod = method.MakeGenericMethod(type1, type2);

      return genericMethod.Invoke(instance, parameters);
    }

    public object ResultOfGenericVersionOfStaticMethod<T>(
      Type type1, Type type2, string name)
    {
      return ResultOfGenericVersionOfStaticMethod<T>(type1, type2, name, new object[] { });
    }

    public object ResultOfGenericVersionOfStaticMethod<T>(
      Type type1, Type type2, string name, params object[] parameters)
    {
      var method = FindEmptyGenericsStaticMethod<T>(name, parameters.Length);

      var genericMethod = method.MakeGenericMethod(type1, type2);

      return genericMethod.Invoke(null, parameters);
    }


    public MethodInfo FindEmptyGenericsInstanceMethod<T>(
      string name, int parametersLength)
    {
      return FindEmptyGenericsMethod<T>(name, parametersLength, 
        BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
    }


    public MethodInfo FindEmptyGenericsStaticMethod<T>(
      string name, int parametersLength)
    {
      return FindEmptyGenericsMethod<T>(name, parametersLength,
        BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
    }

    private static MethodInfo FindEmptyGenericsMethod<T>(string name, int parametersLength,
      BindingFlags bindingFlags)
    {
      var methods = typeof(T).GetMethods(
          bindingFlags)
        .Where(m => m.IsGenericMethodDefinition)
        .Where(m => m.GetParameters().Length == parametersLength);
      var method = methods.First(m => m.Name == name);
      return method;
    }
  }
}