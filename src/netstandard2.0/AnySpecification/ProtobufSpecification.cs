using FluentAssertions;
using Google.Protobuf.Collections;

namespace AnySpecification;

internal class ProtobufSpecification
{
  [Test]
  public void ShouldFillExistingCollections()
  {
    //WHEN
    var objectWithRepeatedField = Any.Instance<ObjectWithRepeatedField>();

    //THEN
    objectWithRepeatedField.Field.Count.Should().Be(3);
    objectWithRepeatedField.AbstractField.Count.Should().Be(3);
  }
}

public abstract class ObjectWithRepeatedField
{
  public RepeatedField<int> Field { get; } = new RepeatedField<int>();
  private RepeatedField<int> PrivateField { get; } = new RepeatedField<int>();
  public abstract RepeatedField<int> AbstractField { get; }
}
