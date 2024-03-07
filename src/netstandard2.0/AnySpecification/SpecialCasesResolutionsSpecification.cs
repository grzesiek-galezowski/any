using AnySpecification.Fixtures;
using NUnit.Framework.Legacy;
using TddXt.AnyExtensibility;
using TddXt.TypeResolution.FakeChainElements.InlineGeneratorTypes;

namespace AnySpecification;

public class SpecialCasesResolutionsSpecification
{
  [Test, Parallelizable]
  public void ShouldCreateResolutionCapableOfGeneratingArrays()
  {
    //GIVEN
    var resolution = new SpecialCasesOfResolutions().CreateResolutionOfArray();
    var generationRequest = Any.Instance<GenerationRequest>();

    //WHEN

    //THEN
    ClassicAssert.True(resolution.AppliesTo(typeof(RecursiveInterface[])));
    ClassicAssert.NotNull(resolution.Apply(Any.Instance<InstanceGenerator>(), Any.Instance<GenerationRequest>(), typeof(RecursiveInterface[])));
    ClassicAssert.AreEqual(generationRequest.Many, 
      ((RecursiveInterface[])resolution.Apply(Any.Instance<InstanceGenerator>(), generationRequest, typeof(RecursiveInterface[]))!).Length);
  }
}
