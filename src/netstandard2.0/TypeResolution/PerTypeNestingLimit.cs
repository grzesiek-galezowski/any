using System;

namespace TddXt.TypeResolution
{
  public interface NestingLimit
  {
    void AddNestingFor<T>();
    bool IsReachedFor<T>();
    void RemoveNestingFor<T>();
  }

  public class GlobalNestingLimit : NestingLimit
  {
    private readonly int _limit;
    private int _nesting;

    private GlobalNestingLimit(int limit)
    {
      _limit = limit;
    }

    public static GlobalNestingLimit Of(int limit)
    {
      return new GlobalNestingLimit(limit);
    }

    public void AddNestingFor<T>()
    {
      _nesting++;
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

    public void RemoveNestingFor<T>()
    {
      _nesting--;
    }
  }
}