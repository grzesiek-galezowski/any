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

  extension(object threadContext)
  {
    private bool IsQuerying()
    {
      var queryingProperty = threadContext.GetType().GetProperty("IsQuerying").OrThrow();
      return (bool)(queryingProperty.GetValue(threadContext).OrThrow());
    }
  }

  extension(object currentSubstitutionContext)
  {
    private object ThreadContext()
    {
      var ThreadContextProperty = currentSubstitutionContext
        .GetType()
        .GetProperty("ThreadContext")
        .OrThrow();
      return ThreadContextProperty
        .GetValue(currentSubstitutionContext).OrThrow();
    }
  }

  extension(Type substitutionContextType)
  {
    private object Current()
    {
      var currentProperty = substitutionContextType.GetProperty("Current").OrThrow();
      return currentProperty.GetValue(null).OrThrow();
    }
  }

  extension(Assembly assembly)
  {
    private Type SubstitutionContextClass()
    {
      return assembly
        .GetType("NSubstitute.Core.SubstitutionContext", true)
        .OrThrow();
    }
  }
}
