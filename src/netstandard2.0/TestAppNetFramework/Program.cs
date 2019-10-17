using System;
using System.Collections.Generic;
using TddXt.AnyRoot.Numbers;
using TddXt.AnyRoot.Strings;
using static TddXt.AnyRoot.Root;

namespace TestAppNetFramework
{
  class Program
  {
    public static void Main(string[] args)
    {
      Console.WriteLine(Any.Instance<IEnumerable<int>>());
      Console.WriteLine(Any.Integer());
      Console.WriteLine(Any.StringContaining("lolek"));
    }
  }
}
