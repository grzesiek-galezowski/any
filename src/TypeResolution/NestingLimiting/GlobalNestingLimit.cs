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

  public void AddNestingFor(Type type, GenerationTrace generationTrace)
  {
    _nesting++;
    generationTrace.AddNestingAndCheckWith(_nesting, type);
  }

  public bool IsReachedFor(Type type)
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

  public void RemoveNestingFor(Type type, GenerationTrace generationTrace)
  {
    _nesting--;
    generationTrace.RemoveNestingAndCheckWith(_nesting, type);
  }

  public static GlobalNestingLimit Of(int limit)
  {
    return new GlobalNestingLimit(limit);
  }
}
