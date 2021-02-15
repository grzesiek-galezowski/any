using System;
using System.Reflection;
using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution.FakeChainElements
{
  public class LimitedFakeChain<T> : IFakeChain<T>
  {
    private readonly IFakeChain<T> _fakeChain;

    public LimitedFakeChain(IFakeChain<T> fakeChain)
    {
      _fakeChain = fakeChain;
    }

    public T Resolve(InstanceGenerator instanceGenerator, GenerationRequest request)
    {
      return request.WithNextNestingLevel(
        () =>
        {
          return _fakeChain.Resolve(instanceGenerator, request);
        }, 
        () =>
        {
          try
          {
            request.Trace.NestingLimitReachedTryingDummy();
            return instanceGenerator.Dummy<T>(request); //TODO
          }
          catch (TargetInvocationException e)
          {
            return default!;
          }
          catch (MemberAccessException e)
          {
            return default!;
          }
          catch (ArgumentException e)
          {
            return default!;
          }
        });
    }
  }
}