using System;
using System.Reflection;
using TddXt.AnyExtensibility;

namespace TypeResolution.FakeChainElements
{
  public class LimitedFakeChain<T> : IFakeChain<T>
  {
    private readonly NestingLimit _perTypeNestingLimit;
    private readonly IFakeChain<T> _fakeChain;

    public LimitedFakeChain(NestingLimit perTypeNestingLimit, IFakeChain<T> fakeChain)
    {
      _perTypeNestingLimit = perTypeNestingLimit;
      _fakeChain = fakeChain;
    }

    public T Resolve(InstanceGenerator instanceGenerator)
    {
      try
      {
        _perTypeNestingLimit.AddNestingFor<T>();
        if (!_perTypeNestingLimit.IsReachedFor<T>())
        {
          return _fakeChain.Resolve(instanceGenerator);
        }
        else 
        {
          try
          {
            return instanceGenerator.Dummy<T>();
          }
          catch (TargetInvocationException e)
          {
            return default(T);
          }
          catch (MemberAccessException e)
          {
            return default(T);
          }
          catch (ArgumentException e)
          {
            return default(T);
          }
        }
        
      }
      finally
      {
        _perTypeNestingLimit.RemoveNestingFor<T>();
      }

    }
  }
}