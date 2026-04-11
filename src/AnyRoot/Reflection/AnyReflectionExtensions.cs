using System;
using System.Reflection;
using TddXt.AnyExtensibility;
using TddXt.AnyGenerators.Root;

namespace TddXt.AnyRoot.Reflection;

public static class AnyReflectionExtensions
{
  extension(BasicGenerator gen)
  {
    public MethodInfo Method() => gen.InstanceOf(InlineGenerators.MethodInfo());

    public Type Type() => gen.InstanceOf(InlineGenerators.Type());
  }
}
