using AnySpecification.Fixtures;
using FluentAssertions;
using NUnit.Framework.Legacy;

namespace AnySpecification;

public class FillingPropertiesSpecification
{
  [Test, Parallelizable]
  public void ShouldFillPropertiesAndFieldsWhenCreatingDataStructures()
  {
    //WHEN
    var instance = Any.Instance<ConcreteDataStructure>();

    //THEN
    ClassicAssert.NotNull(instance.Data);
    ClassicAssert.NotNull(instance._field);
    ClassicAssert.NotNull(instance.Data.Text);
    ClassicAssert.IsNotEmpty(instance.AnImmutableList);
  }

  [Test, Parallelizable]
  public void ShouldNotFillFieldsAndPropertiesThatOnlyHavePublicGetters()
  {
    var maybeObject1 = Any.Instance<ObjectWithGettableMaybe>();

    maybeObject1.Property.Should().Be(Core.Maybe.Maybe<string>.Nothing);
    maybeObject1.GetFieldValue().Should().Be(Core.Maybe.Maybe<string>.Nothing);
  }
}
