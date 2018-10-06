using System;
using System.Collections.Generic;
using TddXt.AnyRoot.NSubstitute;
using TddXt.AnyRoot.Numbers;
using static TddXt.AnyRoot.Root;

namespace TestApp
{
    class Program
    {
      static void Main(string[] args)
        {
            Console.WriteLine(Any.Instance<IEnumerable<int>>());
            Console.WriteLine(Any.Integer());
            Console.WriteLine(Any.Substitute<IEnumerable<int>>());
        }
    }
}
