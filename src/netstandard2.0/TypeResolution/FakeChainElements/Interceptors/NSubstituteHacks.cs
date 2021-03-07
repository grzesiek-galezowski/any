using System;
using System.Linq;
using System.Reflection;
using Castle.DynamicProxy;
using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution.FakeChainElements.Interceptors
{
  internal static class NSubstituteHacks
  {
    public static void AssertIsNotInvokedDuringNSubstituteQuery(IInvocation invocation,
      Func<Type, GenerationRequest, object> instanceSource)
    {
      var interceptedInvocation = new InterceptedInvocation(invocation, instanceSource);

      if (interceptedInvocation.IsPropertyGetter())
      {
        return;
      }

      try
      {
        var assembly = AppDomain.CurrentDomain.GetAssemblies().SingleOrDefault(a =>
          a.FullName.StartsWith("NSubstitute,") && a.CodeBase.EndsWith("NSubstitute.dll"));
        if (assembly != null)
        {
          if (assembly.SubstitutionContextClass().Current().ThreadContext().IsQuerying())
          {
            throw new AnyInstanceUsedInsteadOfNSubstituteDuringAQueryException();
          }
        }
      }
      catch (AnyInstanceUsedInsteadOfNSubstituteDuringAQueryException)
      {
        throw;
      }
      catch (Exception e)
      {
        Console.WriteLine($"NSubstitute hack could not be processed because {e}");
      }
    }

    private static bool IsQuerying(this object threadContext)
    {
      return (bool)threadContext.GetType().GetProperty("IsQuerying").GetValue(threadContext);
    }

    private static object ThreadContext(this object currentSubstitutionContext)
    {
      return currentSubstitutionContext.GetType().GetProperty("ThreadContext")
        .GetValue(currentSubstitutionContext);
    }

    private static object Current(this Type substitutionContextType)
    {
      return substitutionContextType.GetProperty("Current").GetValue(null);
    }

    private static Type SubstitutionContextClass(this Assembly assembly)
    {
      return assembly.GetType("NSubstitute.Core.SubstitutionContext", true);
    }
  }
}