using System;
using FluentAssertions;
using NUnit.Framework;
using TddXt.AnyRoot.Builder;
using static TddXt.AnyRoot.Root;

namespace AnySpecification
{
  class BuilderSpecification
  {
    [Test, Parallelizable]
    public void ShouldAllowSettingNestedSettableAutoProperties()
    {
      Any.Instance<DataStructure>().WithProperty(d => d.Nested.SettableStringValue, "lol")
          .Nested.SettableStringValue.Should().Be("lol");
    }

    [Test, Parallelizable]
    public void ShouldAllowSettingNestedNonSettableAutoProperties()
    {
      Any.Instance<DataStructure>().WithProperty(d => d.Nested.StringValue, "lol")
          .Nested.StringValue.Should().Be("lol");
    }

    [Test, Parallelizable]
    public void ShouldThrowWhenSettingPropertyOfUninitializedProperty()
    {
      Any.Instance<DataStructure>().Invoking(d => d.WithProperty(d => d.NestedNotInitializedFromConstructor.StringValue, "lol"))
          .Should().Throw<Exception>();
    }

    [Test, Parallelizable]
    public void ShouldThrowWhenInvokedOnNull()
    {
      new Action(() => (null as DataStructure)!.WithProperty(d => d!.NestedNotInitializedFromConstructor.StringValue, "lol"))
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
      Any.Instance<DataStructure>().Invoking(d => d.WithProperty(d => d.Nested.NonAutoStringValue, "lol"))
          .Should().Throw<Exception>();
    }
  }

  public class DataStructure
  {
    public DataStructure(NestedDataStructure nested)
    {
      Nested = nested;
    }

    public NestedDataStructure Nested { get; }
    public NestedDataStructure NestedNotInitializedFromConstructor { get; }
    public NestedDataStructure GetNested() => new NestedDataStructure();
  }

  public class NestedDataStructure
  {
    public string StringValue { get; }
    public string SettableStringValue { get; set; }
    public string NonAutoStringValue => "";
  }
}
