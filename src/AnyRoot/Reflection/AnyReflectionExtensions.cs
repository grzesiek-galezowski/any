using System;
using System.Reflection;
using TddXt.AnyExtensibility;
using TddXt.AnyGenerators.Root;

namespace TddXt.AnyRoot.Reflection;

public static class AnyReflectionExtensions
{
  extension(BasicGenerator gen)
  {
    public MethodInfo Method()
    {
      return gen.InstanceOf(InlineGenerators.MethodInfo());
    }

    public Type Type()
    {
      return gen.InstanceOf(InlineGenerators.Type());
    }
  }
}