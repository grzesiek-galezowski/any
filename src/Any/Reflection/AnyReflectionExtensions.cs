using System;
using System.Reflection;
using TddEbook.TddToolkit.Generators;
using TddXt.AnyExtensibility;

namespace TddXt.AnyCore.Reflection
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