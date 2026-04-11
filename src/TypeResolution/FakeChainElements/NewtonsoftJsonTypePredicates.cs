using System;

namespace TddXt.TypeResolution.FakeChainElements;

internal static class NewtonsoftJsonTypePredicates
{
  public static bool IsJObject(Type t)
  {
    return t is { Namespace: "Newtonsoft.Json.Linq", Name: "JObject" };
  }

  public static bool IsJProperty(Type t)
  {
    return t is { Namespace: "Newtonsoft.Json.Linq", Name: "JProperty" };
  }

  public static bool IsJContainer(Type t)
  {
    return t is { Namespace: "Newtonsoft.Json.Linq", Name: "JContainer" };
  }

  public static bool IsJToken(Type type)
  {
    return type is { Namespace: "Newtonsoft.Json.Linq", Name: "JToken" };
  }

  public static bool IsJValue(Type type)
  {
    return type is { Namespace: "Newtonsoft.Json.Linq", Name: "JValue" };
  }
}
