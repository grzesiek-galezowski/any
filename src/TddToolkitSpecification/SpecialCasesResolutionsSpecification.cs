using NUnit.Framework;
using TddToolkitSpecification.Fixtures;
using TddXt.AnyExtensibility;
using TddXt.AnyGenerators.Root.ImplementationDetails;
using TddXt.AnyRoot;

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
      Assert.NotNull(resolution.Apply(Root.Any.Instance<InstanceGenerator>()));
      Assert.AreEqual(3, resolution.Apply(Root.Any.Instance<InstanceGenerator>()).Length);

    }

  }
}
