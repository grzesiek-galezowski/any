using AnySpecification.Fixtures;
using TddXt.AnyExtensibility;
using TddXt.AnyGenerators.Root.ImplementationDetails;
using static TddXt.AnyRoot.Root;

namespace AnySpecification;

public class SpecialCasesResolutionsSpecification
{
  [Test, Parallelizable]
  public void ShouldCreateResolutionCapableOfGeneratingArrays()
  {
    //GIVEN
    var resolution = new SpecialCasesOfResolutions().CreateResolutionOfArray();

    //WHEN

    //THEN
    Assert.True(resolution.AppliesTo(typeof(RecursiveInterface[])));
    Assert.NotNull(resolution.Apply(Any.Instance<InstanceGenerator>(), Any.Instance<GenerationRequest>(), typeof(RecursiveInterface[])));
    Assert.AreEqual(3, ((RecursiveInterface[])resolution.Apply(Any.Instance<InstanceGenerator>(), Any.Instance<GenerationRequest>(), typeof(RecursiveInterface[]))).Length);
  }
}