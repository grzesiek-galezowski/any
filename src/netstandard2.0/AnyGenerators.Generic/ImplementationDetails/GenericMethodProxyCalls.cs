using System;
using System.Linq;
using System.Reflection;

namespace TddXt.AnyGenerators.Generic.ImplementationDetails
{
  public class GenericMethodProxyCalls
  {
    public object ResultOfGenericVersionOfMethod<T>(T instance, Type genericArgumentType, string name, params object[] parameters)
    {
      var method = FindEmptyGenericsInstanceMethod<T>(name, parameters);

      var genericMethod = method.MakeGenericMethod(genericArgumentType);

      return genericMethod.Invoke(instance, parameters);
    }

    public object ResultOfGenericVersionOfStaticMethod<T>(Type genericArgumentType, string name)
    {
      return ResultOfGenericVersionOfStaticMethod<T>(genericArgumentType, name, new object[] { });
    }

    private object ResultOfGenericVersionOfStaticMethod<T>(Type genericArgumentType, string name, params object[] parameters)
    {
      var method = FindEmptyGenericsStaticMethod<T>(name, parameters);

      var genericMethod = method.MakeGenericMethod(genericArgumentType);

      return genericMethod.Invoke(null, parameters);

    }

    public object ResultOfGenericVersionOfStaticMethod<T>(
      Type type1, Type type2, string name)
    {
      return ResultOfGenericVersionOfStaticMethod<T>(type1, type2, name, new object[] { });
    }

    public object ResultOfGenericVersionOfStaticMethod<T>(
      Type type1, Type type2, string name, params object[] parameters)
    {
      var method = FindEmptyGenericsStaticMethod<T>(name, parameters);

      var genericMethod = method.MakeGenericMethod(type1, type2);

      return genericMethod.Invoke(null, parameters);
    }


    private MethodInfo FindEmptyGenericsInstanceMethod<T>(
      string name, object[] parameters)
    {
      return FindEmptyGenericsMethod<T>(name, parameters, 
        BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
    }


    private MethodInfo FindEmptyGenericsStaticMethod<T>(
      string name, object[] parametersLength)
    {
      return FindEmptyGenericsMethod<T>(name, parametersLength,
        BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
    }

    private static MethodInfo FindEmptyGenericsMethod<T>(string name, object[] parameters,
      BindingFlags bindingFlags)
    {
      var methods = typeof(T).GetMethods(bindingFlags)
        .Where(m => m.IsGenericMethodDefinition)
        .Where(m => SameParameterTypes(parameters, m));
      var methodInfos = methods.Where(m => m.Name == name).ToList();
      var method = methodInfos.First();
      return method;
    }

    private static bool SameParameterTypes(object[] parameters, MethodInfo m)
    {
      var expectedParameters = m.GetParameters().Select(p => p.ParameterType).ToList();
      var actualParameters = parameters.Select(p => p.GetType()).ToList();
      if (expectedParameters.Count != actualParameters.Count)
      {
        return false;
      }

      for (var i = 0; i < expectedParameters.Count; i++)
      {
        if (expectedParameters[i] != actualParameters[i])
        {
          if (!actualParameters[i].IsSubclassOf(expectedParameters[i]))
          {
            if (actualParameters[i].GetInterfaces().All(iface => iface != expectedParameters[i]))
            {
              return false;
            }
          }
        }
      }

      return true;
    }
  }
}