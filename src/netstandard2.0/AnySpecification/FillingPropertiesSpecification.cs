using AnySpecification.Fixtures;
using FluentAssertions;

namespace AnySpecification;

public class FillingPropertiesSpecification
{
  [Test, Parallelizable]
  public void ShouldFillPropertiesAndFieldsWhenCreatingDataStructures()
  {
    //WHEN
    var instance = Any.Instance<ConcreteDataStructure>();

    //THEN
    Assert.NotNull(instance.Data);
    Assert.NotNull(instance._field);
    Assert.NotNull(instance.Data.Text);
    Assert.IsNotEmpty(instance.AnImmutableList);
  }

  [Test, Parallelizable]
  public void ShouldNotFillFieldsAndPropertiesThatOnlyHavePublicGetters()
  {
    var maybeObject1 = Any.Instance<ObjectWithGettableMaybe>();

    maybeObject1.Property.Should().Be(Core.Maybe.Maybe<string>.Nothing);
    maybeObject1.GetFieldValue().Should().Be(Core.Maybe.Maybe<string>.Nothing);
  }
}
