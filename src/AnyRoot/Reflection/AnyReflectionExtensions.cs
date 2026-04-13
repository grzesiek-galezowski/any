using System;
using System.Reflection;
using TddXt.AnyExtensibility;

namespace TddXt.AnyRoot.Reflection;

public static class AnyReflectionExtensions
{
  extension(BasicGenerator gen)
  {
    public MethodInfo Method() => gen.InstanceOf(InlineGenerators.InlineGenerators.MethodInfo());

    public Type Type() => gen.InstanceOf(InlineGenerators.InlineGenerators.Type());
  }
}
