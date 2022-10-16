using System;
using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution.NestingLimiting;

public class GlobalNestingLimit : NestingLimit
{
  private readonly int _limit;
  private int _nesting;

  private GlobalNestingLimit(int limit)
  {
    _limit = limit;
  }

  public void AddNestingFor<T>(GenerationTrace generationTrace)
  {
    _nesting++;
    generationTrace.AddNestingAndCheckWith(_nesting, typeof(T));
  }

  public bool IsReachedFor<T>()
  {
    if (_nesting == _limit + 1)
    {
      return true;
    }
    if (_nesting > _limit + 1)
    {
      throw new InvalidOperationException("nesting limit bug. Actual: " + _nesting + ", limit: " + _limit);
    }
    return false;

  }

  public void RemoveNestingFor<T>(GenerationTrace generationTrace)
  {
    _nesting--;
    generationTrace.RemoveNestingAndCheckWith(_nesting, typeof(T));
  }

  public static GlobalNestingLimit Of(int limit)
  {
    return new GlobalNestingLimit(limit);
  }
}
