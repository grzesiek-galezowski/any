using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using TddXt.AutoFixtureWrapper;
using TddXt.CommonTypes;

namespace TddXt.AnyGenerators.Root.ImplementationDetails
{
  public class AutoFixtureConfiguration
  {
    private readonly CircularList<MethodInfo> _methodList =
      CircularList.CreateStartingFromRandom(typeof(List<int>).GetMethods(BindingFlags.Public | BindingFlags.Instance));

    private readonly CircularList<Type> _types = CircularList.CreateStartingFromRandom(
      typeof(Type1),
      typeof(Type2),
      typeof(Type3),
      typeof(Type4),
      typeof(Type5),
      typeof(Type6),
      typeof(Type7),
      typeof(Type8),
      typeof(Type9),
      typeof(Type10),
      typeof(Type11),
      typeof(Type12),
      typeof(Type13));

    public void ApplyTo(FixtureWrapper fixtureWrapper)
    {
      var wrapper = fixtureWrapper;
      wrapper.Register(() => _types.Next());
      wrapper.Register(() => _methodList.Next());
      wrapper.Register(() => new Exception(wrapper.Create<string>(), new Exception(wrapper.Create<string>())));
      wrapper.Register(
        () =>
          new IPAddress(new[]
            {wrapper.Create<byte>(), wrapper.Create<byte>(), wrapper.Create<byte>(), wrapper.Create<byte>()}));
    }
  }

  public class Type1 {}
  public class Type2 {}
  public class Type3 {}
  public class Type4 {}
  public class Type5 {}
  public class Type6 {}
  public class Type7 {}
  public class Type8 {}
  public class Type9 {}
  public class Type10 {}
  public class Type11 {}
  public class Type12 {}
  public class Type13 {}
}