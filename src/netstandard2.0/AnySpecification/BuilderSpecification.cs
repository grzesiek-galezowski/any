using System;
using Core.NullableReferenceTypesExtensions;
using FluentAssertions;
using TddXt.AnyRoot.Builder;

namespace AnySpecification;

class BuilderSpecification
{
  [Test, Parallelizable]
  public void ShouldAllowSettingNestedSettableAutoProperties()
  {
    Any.Instance<DataStructure>().WithProperty(d => d.Nested!.SettableStringValue, "lol")
      .Nested.OrThrow().SettableStringValue.Should().Be("lol");
  }

  [Test, Parallelizable]
  public void ShouldAllowSettingNestedNonSettableAutoProperties()
  {
    Any.Instance<DataStructure>().WithProperty(d => d.Nested!.StringValue, "lol")
      .Nested.OrThrow().StringValue.Should().Be("lol");
  }

  [Test, Parallelizable]
  public void ShouldThrowWhenSettingPropertyOfUninitializedProperty()
  {
    Any.Instance<DataStructure>().Invoking(d => d.WithProperty(d => d.NestedNotInitializedFromConstructor!.StringValue, "lol"))
      .Should().Throw<Exception>();
  }

  [Test, Parallelizable]
  public void ShouldThrowWhenInvokedOnNull()
  {
    new Action(() => (null as DataStructure)!.WithProperty(d => d.NestedNotInitializedFromConstructor!.StringValue, "lol"))
      .Should().Throw<Exception>();
  }

  [Test, Parallelizable]
  public void ShouldThrowWhenSettingPropertyOfMethodReturnValue()
  {
    Any.Instance<DataStructure>().Invoking(d => d.WithProperty(d2 => d2.GetNested().StringValue, "lol"))
      .Should().Throw<Exception>();
  }

  [Test, Parallelizable]
  public void ShouldThrowWhenSettingNonAutoNonSettableProperty()
  {
    Any.Instance<DataStructure>().Invoking(d => d.WithProperty(d => d.Nested!.NonAutoStringValue, "lol"))
      .Should().Throw<Exception>();
  }
    
  //fields

  [Test, Parallelizable]
  public void ShouldAllowSettingPublicFieldThroughProperty()
  {
    Any.Instance<DataStructure>().WithProperty(d => d.Nested!.StringField, "lol")
      .Nested.OrThrow().StringField.Should().Be("lol");
  }
    
  [Test, Parallelizable]
  public void ShouldAllowSettingPublicFieldThroughField()
  {
    Any.Instance<DataStructure>().WithProperty(d => d.NestedField!.StringField, "lol")
      .NestedField.OrThrow().StringField.Should().Be("lol");
  }
    
  [Test, Parallelizable]
  public void ShouldAllowSettingPublicFieldThroughReadOnlyField()
  {
    var dataStructure = Any.Instance<DataStructure>();
    dataStructure.WithProperty(d => d.NestedReadOnlyField!.StringField, "lol")
      .NestedReadOnlyField.OrThrow().StringField.Should().Be("lol");
  }
    
  [Test, Parallelizable]
  public void ShouldAllowSettingPropertyThroughPublicField()
  {
    var dataStructure = Any.Instance<DataStructure>();
    dataStructure.WithProperty(d => d.NestedReadOnlyField!.SettableStringValue, "lol")
      .NestedReadOnlyField.OrThrow().SettableStringValue.Should().Be("lol");
  }
}

public class DataStructure
{
  public DataStructure(NestedDataStructure nested)
  {
    Nested = nested;
  }

  public NestedDataStructure? Nested { get; }
  public NestedDataStructure? NestedField;
  public NestedDataStructure? NestedReadOnlyField;
  public NestedDataStructure? NestedNotInitializedFromConstructor { get; }
  public NestedDataStructure GetNested() => new();
}

public class NestedDataStructure
{
  public string? StringField;
  public string? StringValue { get; }
  public string? SettableStringValue { get; set; }
  public string NonAutoStringValue => "";
}
