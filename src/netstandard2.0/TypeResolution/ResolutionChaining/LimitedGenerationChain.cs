using System;
using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution.ResolutionChaining;

public class LimitedGenerationChain(IGenerationChain generationChain) : IGenerationChain
{
  public object Resolve(InstanceGenerator instanceGenerator, GenerationRequest request, Type type)
  {
    return request.ResolveNextNestingLevel(generationChain, instanceGenerator, type);

    //return request.WithNextNestingLevel(
    //  type,
    //  () => _generationChain.Resolve(instanceGenerator, request, type),
    //  () =>
    //  {
    //    try
    //    {
    //      request.Trace.RecursionLimitReachedTryingDummy();
    //      return instanceGenerator.Dummy(request, type)!; //TODO
    //    }
    //    catch (TargetInvocationException e)
    //    {
    //      return default!;
    //    }
    //    catch (MemberAccessException e)
    //    {
    //      return default!;
    //    }
    //    catch (ArgumentException e)
    //    {
    //      return default!;
    //    }
    //  });
  }
}
