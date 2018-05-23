using System;
using System.Collections.Generic;
using TddXt.AnyRoot.Collections;
using TddXt.AnyRoot.Exploding;
using TddXt.AnyRoot.Invokable;
using TddXt.AnyRoot.Math;
using TddXt.AnyRoot.Network;
using TddXt.AnyRoot.NSubstitute;
using TddXt.AnyRoot.Numbers;
using TddXt.AnyRoot.Reflection;
using TddXt.AnyRoot.Strings;
using TddXt.AnyRoot.Time;
using static TddXt.AnyRoot.Root;

namespace TestAppNetFramework
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine(Any.Instance<IEnumerable<int>>());
      Console.WriteLine(Any.Integer());
      Console.WriteLine(Any.Substitute<IEnumerable<int>>());
      Console.WriteLine(Any.StringContaining("lolek"));
    }
  }
}
