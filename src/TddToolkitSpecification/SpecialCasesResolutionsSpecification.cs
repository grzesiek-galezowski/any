using Generators.ImplementationDetails;
using NUnit.Framework;
using TddToolkitSpecification.Fixtures;
using TddXt.AnyCore;
using TddXt.AnyExtensibility;

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
      Assert.NotNull(resolution.Apply(Core.Any.Instance<InstanceGenerator>()));
      Assert.AreEqual(3, resolution.Apply(Core.Any.Instance<InstanceGenerator>()).Length);

    }

  }
}
