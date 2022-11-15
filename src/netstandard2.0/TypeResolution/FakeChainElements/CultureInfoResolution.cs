using System;
using System.Globalization;
using TddXt.AnyExtensibility;
using TddXt.TypeResolution.CustomCollections;

namespace TddXt.TypeResolution.FakeChainElements;

public class CultureInfoResolution : IResolution
{
  private readonly CircularList<CultureInfo> _cultures = CircularList.CreateStartingFromRandom(CultureInfo.GetCultures(CultureTypes.AllCultures));

  public bool AppliesTo(Type type)
  {
    return type == typeof(CultureInfo);
  }

  public object Apply(InstanceGenerator instanceGenerator, GenerationRequest request, Type type)
  {
    return _cultures.Next();
  }
}
