using System;
using System.Reflection;
using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution.FakeChainElements
{
  public class LimitedGenerationChain<T> : IGenerationChain<T>
  {
    private readonly IGenerationChain<T> _generationChain;

    public LimitedGenerationChain(IGenerationChain<T> generationChain)
    {
      _generationChain = generationChain;
    }

    public object Resolve(InstanceGenerator instanceGenerator, GenerationRequest request, Type type)
    {
      return request.WithNextNestingLevel(
        () =>
        {
          return _generationChain.Resolve(instanceGenerator, request, type);
        }, 
        () =>
        {
          try
          {
            request.Trace.NestingLimitReachedTryingDummy();
            return instanceGenerator.Dummy<T>(request)!; //TODO
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
