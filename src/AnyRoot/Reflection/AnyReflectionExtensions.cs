using System;
using System.Reflection;
using TddXt.AnyExtensibility;
using TddXt.AnyGenerators.Root;

namespace TddXt.AnyRoot.Reflection
{
  public static class AnyReflectionExtensions
  {
    public static MethodInfo Method(this BasicGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.MethodInfo());
    }

    public static Type Type(this BasicGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.Type());
    }
  }
}