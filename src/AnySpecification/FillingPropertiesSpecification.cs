using AnySpecification.Fixtures;
using Core.NullableReferenceTypesExtensions;
using AwesomeAssertions;

namespace AnySpecification;

public class FillingPropertiesSpecification
{
  [Test, Parallelizable]
  public void ShouldFillPropertiesAndFieldsWhenCreatingDataStructures()
  {
    //WHEN
    var instance = Any.Instance<ConcreteDataStructure>();

    //THEN
    Assert.That(instance.Data, Is.Not.Null);
    Assert.That(instance.Field, Is.Not.Null);
    Assert.That(instance.Data.OrThrow().Text, Is.Not.Null);
    Assert.That(instance.AnImmutableList, Is.Not.Empty);
  }

  [Test, Parallelizable]
  public void ShouldNotFillFieldsAndPropertiesThatOnlyHavePublicGetters()
  {
    var maybeObject1 = Any.Instance<ObjectWithGettableMaybe>();

    maybeObject1.Property.Should().Be(Core.Maybe.Maybe<string>.Nothing);
    maybeObject1.GetFieldValue().Should().Be(Core.Maybe.Maybe<string>.Nothing);
  }
}
