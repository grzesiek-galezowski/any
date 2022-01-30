using System;
using System.Collections.Generic;
using TddXt.AnyExtensibility;
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
      Console.WriteLine(Any.Instance<int>(new SingleTypeCustomization<int>((generator, request) => 34)));
      Console.WriteLine(Any.InstanceOf(new MyGenerator()));
    }
  }

  internal class MyGenerator : InlineGenerator<bool>
  {
    public bool GenerateInstance(InstanceGenerator instanceGenerator, GenerationRequest request)
    {
      return instanceGenerator.Instance<bool>(request);
    }
  }
}
