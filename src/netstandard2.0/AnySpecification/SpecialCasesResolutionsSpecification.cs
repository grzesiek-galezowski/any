using NUnit.Framework;
using TddToolkitSpecification.Fixtures;
using TddXt.AnyExtensibility;
using TddXt.AnyGenerators.Root.ImplementationDetails;
using static TddXt.AnyRoot.Root;

namespace TddToolkitSpecification
{
  public class SpecialCasesResolutionsSpecification
  {
    [Test]
    public void ShouldCreateResolutionCapableOfGeneratingArrays()
    {
      //GIVEN
      var resolution = new SpecialCasesOfResolutions<RecursiveInterface[]>().CreateResolutionOfArray();
      
      //WHEN

      //THEN
      Assert.True(resolution.Applies());
      Assert.NotNull(resolution.Apply(Any.Instance<InstanceGenerator>(), Any.Instance<GenerationTrace>()));
      Assert.AreEqual(3, resolution.Apply(Any.Instance<InstanceGenerator>(), Any.Instance<GenerationTrace>()).Length);
    }
  }
}
