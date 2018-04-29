using AnyCore;
using NUnit.Framework;
using TddEbook.TddToolkit.Generators;
using TddEbook.TddToolkitSpecification.Fixtures;
using TddEbook.TypeReflection;
using static AnyCore.Core;

namespace TddEbook.TddToolkitSpecification
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
      Assert.NotNull(resolution.Apply(Any.Instance<InstanceGenerator>()));
      Assert.AreEqual(3, resolution.Apply(Any.Instance<InstanceGenerator>()).Length);

    }

  }
}
