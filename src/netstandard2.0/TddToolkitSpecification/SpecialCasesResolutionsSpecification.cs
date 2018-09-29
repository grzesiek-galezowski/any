using NUnit.Framework;
using TddToolkitSpecification.Fixtures;
using TddXt.AnyExtensibility;
using TddXt.AnyGenerators.Root.ImplementationDetails;
using TddXt.CommonTypes;
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
      Assert.NotNull(resolution.Apply(Any.Instance<InstanceGenerator>(), Any.Instance<ListBasedGenerationTrace>()));
      Assert.AreEqual(3, resolution.Apply(Any.Instance<InstanceGenerator>(), Any.Instance<ListBasedGenerationTrace>()).Length);

    }

  }
}
