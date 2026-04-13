using System;
using System.Reflection;

namespace TddXt.AnyRoot.Reflection;

public static class AnyReflectionExtensions
{
  extension(Any)
  {
    public static MethodInfo Method() => Any.InstanceOf(InlineGenerators.InlineGenerators.MethodInfo());

    public static Type Type() => Any.InstanceOf(InlineGenerators.InlineGenerators.Type());
  }
}
