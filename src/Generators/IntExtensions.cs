using System;

namespace TddEbook.TddToolkit.Generators
{
  static class IntExtensions
  {
    public static void Times(this int times, Action action)
    {
      for (int i = 0; i < times; i++)
      {
        action();
      }
    }
  }
}