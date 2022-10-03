using System;
using System.Collections.Generic;
using System.Reflection;
using TddXt.AnyExtensibility;
using TddXt.TypeResolution.CustomCollections;

namespace TddXt.TypeResolution.FakeChainElements;

public class MethodInfoGenerator : IResolution
{
  private readonly CircularList<MethodInfo> _methodList =
    CircularList.CreateStartingFromRandom(typeof(List<int>).GetMethods(BindingFlags.Public | BindingFlags.Instance));

  public bool AppliesTo(Type type)
  {
    return type == typeof(MethodInfo);
  }

  public object Apply(InstanceGenerator instanceGenerator, GenerationRequest request, Type type)
  {
    return _methodList.Next();
  }
}
