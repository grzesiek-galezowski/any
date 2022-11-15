using System;
using System.Linq;
using System.Reflection;
using Castle.DynamicProxy;
using Core.NullableReferenceTypesExtensions;
using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution.FakeChainElements.Interceptors;

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
        HasNSubstituteFullName(a) && IsNSubstituteDll(a));
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

  private static bool IsNSubstituteDll(Assembly a)
  {
    return a.Location?.EndsWith("NSubstitute.dll") ?? false;
  }

  private static bool HasNSubstituteFullName(Assembly a)
  {
    return a.FullName?.StartsWith("NSubstitute,") ?? false;
  }

  private static bool IsQuerying(this object threadContext)
  {
    var queryingProperty = threadContext.GetType().GetProperty("IsQuerying").OrThrow();
    return (bool)(queryingProperty.GetValue(threadContext).OrThrow());
  }

  private static object ThreadContext(this object currentSubstitutionContext)
  {
    var ThreadContextProperty = currentSubstitutionContext
      .GetType()
      .GetProperty("ThreadContext")
      .OrThrow();
    return ThreadContextProperty
      .GetValue(currentSubstitutionContext).OrThrow();
  }

  private static object Current(this Type substitutionContextType)
  {
    var currentProperty = substitutionContextType.GetProperty("Current").OrThrow();
    return currentProperty.GetValue(null).OrThrow();
  }

  private static Type SubstitutionContextClass(this Assembly assembly)
  {
    return assembly
      .GetType("NSubstitute.Core.SubstitutionContext", true)
      .OrThrow();
  }
}
