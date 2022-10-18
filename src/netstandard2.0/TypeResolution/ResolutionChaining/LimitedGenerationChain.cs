using System;
using System.Reflection;
using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution.ResolutionChaining;

public class LimitedGenerationChain : IGenerationChain
{
  private readonly IGenerationChain _generationChain;

  public LimitedGenerationChain(IGenerationChain generationChain)
  {
    _generationChain = generationChain;
  }

  public object Resolve(InstanceGenerator instanceGenerator, GenerationRequest request, Type type)
  {
    return request.WithNextNestingLevel(
      () => _generationChain.Resolve(instanceGenerator, request, type),
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
