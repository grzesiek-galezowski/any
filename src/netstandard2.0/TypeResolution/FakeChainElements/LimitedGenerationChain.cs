using System;
using System.Reflection;
using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution.FakeChainElements
{
  public class LimitedGenerationChain : IGenerationChain
  {
    private readonly IGenerationChain _generationChain;

    public LimitedGenerationChain(IGenerationChain generationChain, Type type)
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
            return instanceGenerator.Dummy(request, type)!; //TODO
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
