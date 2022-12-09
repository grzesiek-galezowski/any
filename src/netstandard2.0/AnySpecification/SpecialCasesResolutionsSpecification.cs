using AnySpecification.Fixtures;
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
    Assert.True(resolution.AppliesTo(typeof(RecursiveInterface[])));
    Assert.NotNull(resolution.Apply(Any.Instance<InstanceGenerator>(), Any.Instance<GenerationRequest>(), typeof(RecursiveInterface[])));
    Assert.AreEqual(generationRequest.Many, ((RecursiveInterface[])resolution.Apply(Any.Instance<InstanceGenerator>(), generationRequest, typeof(RecursiveInterface[]))).Length);
  }
}
