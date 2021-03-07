using AnySpecification.Fixtures;
using NUnit.Framework;
using TddXt.AnyExtensibility;
using TddXt.AnyGenerators.Root.ImplementationDetails;
using static TddXt.AnyRoot.Root;

namespace AnySpecification
{
  public class SpecialCasesResolutionsSpecification
  {
    [Test, Parallelizable]
    public void ShouldCreateResolutionCapableOfGeneratingArrays()
    {
      //GIVEN
      var resolution = new SpecialCasesOfResolutions<RecursiveInterface[]>().CreateResolutionOfArray();

      //WHEN

      //THEN
      Assert.True(resolution.Applies());
      Assert.NotNull(resolution.Apply(Any.Instance<InstanceGenerator>(), Any.Instance<GenerationRequest>()));
      Assert.AreEqual(3, resolution.Apply(Any.Instance<InstanceGenerator>(), Any.Instance<GenerationRequest>()).Length);
    }
  }
}
