using System;
using System.Collections.Generic;
using TddXt.AnyRoot;
using TddXt.AnyRoot.NSubstitute;
using TddXt.AnyRoot.Numbers;
using TddXt.AnyRoot.Strings;

namespace TestAppNetFramework
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine(Root.Any.Instance<IEnumerable<int>>());
      Console.WriteLine(Root.Any.Integer());
      Console.WriteLine(Root.Any.Substitute<IEnumerable<int>>());
      Console.WriteLine(Root.Any.StringContaining("lolek"));
    }
  }
}
