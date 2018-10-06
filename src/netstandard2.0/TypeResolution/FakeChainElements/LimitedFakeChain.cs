using System;
using System.Reflection;
using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution.FakeChainElements
{
  public class LimitedFakeChain<T> : IFakeChain<T>
  {
    private readonly IFakeChain<T> _fakeChain;
    private readonly NestingLimit _nestingLimit;

    public LimitedFakeChain(NestingLimit nestingLimit, IFakeChain<T> fakeChain)
    {
      _nestingLimit = nestingLimit;
      _fakeChain = fakeChain;
    }

    public T Resolve(InstanceGenerator instanceGenerator, GenerationTrace trace)
    {
      try
      {
        _nestingLimit.AddNestingFor<T>(trace);
        if (!_nestingLimit.IsReachedFor<T>())
        {
          return _fakeChain.Resolve(instanceGenerator, trace);
        }
        else 
        {
          try
          {
            trace.NestingLimitReachedTryingDummy();
            return instanceGenerator.Dummy<T>(trace); //TODO
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
        _nestingLimit.RemoveNestingFor<T>(trace);
      }

    }
  }
}