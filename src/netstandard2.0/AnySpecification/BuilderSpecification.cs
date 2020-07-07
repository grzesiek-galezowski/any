using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using NUnit.Framework;
using TddXt.AnyRoot.Builder;
using static TddXt.AnyRoot.Root;

namespace AnySpecification
{
    class BuilderSpecification
    {
        [Test]
        public void ShouldAllowSettingNestedSettableAutoProperties()
        {
            Any.Instance<DataStructure>().WithProperty(d => d.Nested.SettableStringValue, "lol")
                .Nested.SettableStringValue.Should().Be("lol");
        }

        [Test]
        public void ShouldAllowSettingNestedNonSettableAutoProperties()
        {
            Any.Instance<DataStructure>().WithProperty(d => d.Nested.StringValue, "lol")
                .Nested.StringValue.Should().Be("lol");
        }

        [Test]
        public void ShouldThrowWhenSettingPropertyOfUninitializedProperty()
        {
            Any.Instance<DataStructure>().Invoking(d => d.WithProperty(d => d.NestedNotInitializedFromConstructor.StringValue, "lol"))
                .Should().Throw<Exception>();
        }
        
        [Test]
        public void ShouldThrowWhenInvokedOnNull()
        {
            (null as DataStructure).Invoking(d => d.WithProperty(d => d!.NestedNotInitializedFromConstructor.StringValue, "lol"))
                .Should().Throw<Exception>();
        }

        [Test]
        public void ShouldThrowWhenSettingPropertyOfMethodReturnValue()
        {
            Any.Instance<DataStructure>().Invoking(d => d.WithProperty(d => d.GetNested().StringValue, "lol"))
                .Should().Throw<Exception>();
        }
        
        [Test]
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

        //bug non auto properties
        //bug methods
    }

    public class NestedDataStructure
    {
        public string StringValue { get; }
        public string SettableStringValue { get; set; }
        public string NonAutoStringValue => "";
    }
}
