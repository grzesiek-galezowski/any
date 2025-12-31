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
    Assert.That(resolution.AppliesTo(typeof(RecursiveInterface[])), Is.True);
    Assert.That(resolution.Apply(Any.Instance<InstanceGenerator>(), Any.Instance<GenerationRequest>(), typeof(RecursiveInterface[])), Is.Not.Null);
    Assert.That(((RecursiveInterface[])resolution.Apply(Any.Instance<InstanceGenerator>(), generationRequest, typeof(RecursiveInterface[]))!).Length,
      Is.EqualTo(generationRequest.Many));
  }
}
