using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using AnySpecification.Fixtures;
using AnySpecification.GraphComparison;
using Core.NullableReferenceTypesExtensions;
using AwesomeAssertions;
using Functional.Maybe;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NSubstitute;
using Optional;
using Optional.Unsafe;
using TddXt.AnyExtensibility;
using TddXt.AnyRoot;
using TddXt.AnyRoot.Collections;
using TddXt.AnyRoot.Enums;
using TddXt.AnyRoot.Invokable;
using TddXt.AnyRoot.Math;
using TddXt.AnyRoot.Network;
using TddXt.AnyRoot.Numbers;
using TddXt.AnyRoot.Reflection;
using TddXt.AnyRoot.Strings;
using TddXt.TypeResolution.FakeChainElements.Interceptors;

// ReSharper disable PublicConstructorInAbstractClass

namespace AnySpecification;

public partial class AnySpecification
{
  [Test, Parallelizable]
  public void ShouldGenerateDifferentIntegerEachTime()
  {
    Enumerable.Range(1, 1000)
      .Select(n => Any.Integer())
      .Should().OnlyHaveUniqueItems();
  }

  [Test, Parallelizable]
  public void ShouldGenerateDifferentByteEachTime()
  {
    Enumerable.Range(1, byte.MaxValue)
      .Select(n => Any.Byte())
      .Should().OnlyHaveUniqueItems();
  }

  [Test, Parallelizable]
  public void ShouldGenerateDifferentDoubleEachTime()
  {
    Enumerable.Range(1, 1000)
      .Select(n => Any.Double())
      .Should().OnlyHaveUniqueItems();
  }

  [Test, Parallelizable]
  public void ShouldGenerateDifferentFloatEachTime()
  {
    Enumerable.Range(1, 1000)
      .Select(n => Any.Float())
      .Should().OnlyHaveUniqueItems();
  }

  [Test, Parallelizable]
  public void ShouldGenerateDifferentLongEachTime()
  {
    Enumerable.Range(1, 1000)
      .Select(n => Any.Long())
      .Should().OnlyHaveUniqueItems();
  }

  [Test, Parallelizable]
  public void ShouldGenerateDifferentUnsignedLongEachTime()
  {
    Enumerable.Range(1, 1000)
      .Select(n => Any.UnsignedLong())
      .Should().OnlyHaveUniqueItems();
  }

  [Test, Parallelizable]
  public void ShouldGenerateDifferentDecimalEachTime()
  {
    Enumerable.Range(1, 1000)
      .Select(n => Any.Decimal())
      .Should().OnlyHaveUniqueItems();
  }

  [Test, Parallelizable]
  public void ShouldGenerateDifferentUnsignedIntEachTime()
  {
    Enumerable.Range(1, 1000)
      .Select(n => Any.UnsignedInt())
      .Should().OnlyHaveUniqueItems();
  }

  [Test, Parallelizable]
  public void ShouldGenerateDifferentUnsignedShortEachTime()
  {
    Enumerable.Range(1, 1000)
      .Select(n => Any.UnsignedShort())
      .Should().OnlyHaveUniqueItems();
  }

  [Test, Parallelizable]
  public void ShouldGenerateDifferentShortEachTime()
  {
    Enumerable.Range(1, 1000)
      .Select(n => Any.Short())
      .Should().OnlyHaveUniqueItems();
  }

  [Test, Parallelizable]
  public void ShouldGenerateDifferentInt64EachTime()
  {
    Enumerable.Range(1, 1000)
      .Select(n => Any.Instance<Int64>())
      .Should().OnlyHaveUniqueItems();
  }

  [Test, Parallelizable]
  public void ShouldGenerateDifferentInt128EachTime()
  {
    Enumerable.Range(1, 1000)
      .Select(n => Any.Instance<Int128>())
      .Should().OnlyHaveUniqueItems();
  }
  
  [Test, Parallelizable]
  public void ShouldGenerateDifferentUInt128EachTime()
  {
    Enumerable.Range(1, 1000)
      .Select(n => Any.Instance<UInt128>())
      .Should().OnlyHaveUniqueItems();
  }

  [Test, Parallelizable]
  public void ShouldGenerateDifferentHalfEachTime()
  {
    Enumerable.Range(1, 1000)
      .Select(n => Any.Instance<Half>())
      .Should().OnlyHaveUniqueItems();
  }

  [Test, Parallelizable]
  public void ShouldGenerateDifferentNullableIntegerEachTime()
  {
    Enumerable.Range(1, 1000)
      .Select(n => Any.Instance<int?>())
      .Should().OnlyHaveUniqueItems();
  }

  [Test, Repeat(10)]
  public void ShouldGenerateDifferentDigitEachTime()
  {
    //GIVEN
    var digit1 = Any.Digit();
    var digit2 = Any.Digit();

    //THEN
    Assert.That(digit1, Is.GreaterThanOrEqualTo(0));
    Assert.That(digit1, Is.LessThanOrEqualTo(9));
    Assert.That(digit2, Is.GreaterThanOrEqualTo(0));
    Assert.That(digit2, Is.LessThanOrEqualTo(9));
    Assert.That(digit2, Is.Not.EqualTo(digit1));
  }

  [Test, Repeat(10)]
  public void ShouldGenerateDifferentPositiveDigitEachTime()
  {
    //GIVEN
    var digit1 = Any.PositiveDigit();
    var digit2 = Any.PositiveDigit();

    //THEN
    Assert.That(digit1, Is.GreaterThanOrEqualTo(1));
    Assert.That(digit1, Is.LessThanOrEqualTo(9));
    Assert.That(digit2, Is.GreaterThanOrEqualTo(1));
    Assert.That(digit2, Is.LessThanOrEqualTo(9));
    Assert.That(digit2, Is.Not.EqualTo(digit1));
  }

  [Test, Parallelizable]
  public void ShouldGenerateDifferentIpAddressEachTime()
  {
    //GIVEN
    var address1 = Any.Instance<IPAddress>();
    var address2 = Any.Instance<IPAddress>();

    //THEN
    Assert.That(address2, Is.Not.EqualTo(address1));
    Assert.That(address2.ToString(), Is.Not.EqualTo(address1.ToString()));
  }

  [Test, Parallelizable]
  public void ShouldGenerateIpAddressThroughExtension()
  {
    //GIVEN
    var address = Any.IpAddress();

    //THEN
    Assert.That(address, Is.Not.Null);
    Assert.That(IPAddress.TryParse(address.ToString(), out _), Is.True);
  }

  [Test, Parallelizable]
  public void ShouldGeneratePortThroughExtension()
  {
    //WHEN
    var port = Any.Port();

    //THEN
    Assert.That(port, Is.GreaterThanOrEqualTo(0));
    Assert.That(port, Is.LessThan(65535));
  }

  [Test, Parallelizable]
  public void ShouldGenerateIpStringThroughExtension()
  {
    //WHEN
    var ipString = Any.IpString();

    //THEN
    Assert.That(IPAddress.TryParse(ipString, out var parsedIpAddress), Is.True);
    Assert.That(parsedIpAddress, Is.Not.Null);
  }

  [Test, Parallelizable]
  public void ShouldGenerateUrlStringThroughExtension()
  {
    //WHEN
    var urlString = Any.UrlString();

    //THEN
    Assert.That(Uri.TryCreate(urlString, UriKind.Absolute, out var parsedUrl), Is.True);
    Assert.That(parsedUrl, Is.Not.Null);
  }

  [Test, Parallelizable]
  public void ShouldGenerateOctetThroughExtension()
  {
    //WHEN
    var octet = Any.Octet();

    //THEN
    Assert.That(octet, Is.GreaterThanOrEqualTo(byte.MinValue));
    Assert.That(octet, Is.LessThanOrEqualTo(byte.MaxValue));
  }

  [Test] 
  //this test cannot be parallelizable because it can have conflict with other tests that generate type objects
  public void ShouldGenerateDifferentTypeEachTimeUpTo13Times()
  {
    Enumerable.Range(0, 13).Select(_ => Any.Type())
      .ToList().Should().NotBeEmpty().And.OnlyHaveUniqueItems();
    Enumerable.Range(0, 13).Select(_ => Any.Instance<Type>())
      .ToList().Should().NotBeEmpty().And.OnlyHaveUniqueItems();
  }

  [Test, Parallelizable]
  public void ShouldBeAbleToProxyConcreteReturnTypesOfMethods()
  {
    var obj = Any.Instance<ISimple>();

    Assert.That(obj.GetInt(), Is.Not.Default);
    Assert.That(obj.GetString(), Is.Not.Empty);
    Assert.That(obj.GetStringProperty, Is.Not.Empty);
    Assert.That(obj.GetString(), Is.Not.Null);
    Assert.That(obj.GetStringProperty, Is.Not.Null);
  }

  [Test, Parallelizable]
  public void ShouldBeAbleToProxyMethodsThatReturnInterfaces()
  {
    //GIVEN
    var obj = Any.Instance<ISimple>();

    //WHEN
    obj = obj.GetInterface();

    //THEN
    Assert.That(obj, Is.Not.Null);
    Assert.That(obj.GetInt(), Is.Not.Default);
    Assert.That(obj.GetString(), Is.Not.Empty);
    Assert.That(obj.GetStringProperty, Is.Not.Empty);
    Assert.That(obj.GetString(), Is.Not.Null);
    Assert.That(obj.GetStringProperty, Is.Not.Null);
  }

  [Test, Parallelizable]
  public void ShouldAlwaysReturnTheSameValueFromProxiedMethodOnTheSameObject()
  {
    //GIVEN
    var obj = Any.Instance<ISimple>();

    //WHEN
    var valueFirstTime = obj.GetString();
    var valueSecondTime = obj.GetString();

    //THEN
    Assert.That(valueSecondTime, Is.EqualTo(valueFirstTime));
  }

  [Test, Parallelizable]
  public void ShouldAlwaysReturnTheDifferentValueFromProxiedTheSameMethodOnDifferentObject()
  {
    //GIVEN
    var obj = Any.Instance<ISimple>();
    var obj2 = Any.Instance<ISimple>();
    //WHEN
    var valueFromFirstInstance = obj.GetString();
    var valueFromSecondInstance = obj2.GetString();

    //THEN
    Assert.That(valueFromSecondInstance, Is.Not.EqualTo(valueFromFirstInstance));
  }

  [Test, Parallelizable]
  public void ShouldThrowExceptionWhenItsMethodInvokedDuringReceivedInOrder()
  {
    //GIVEN
    var obj = Any.Instance<ISimple>();
    //WHEN

    //THEN
    obj.Invoking(o => Received.InOrder(() => o.GetString())).Should()
      .Throw<AnyInstanceUsedInsteadOfNSubstituteDuringAQueryException>();
  }

  [Test, Parallelizable]
  public void ShouldNotThrowExceptionWhenItsPropertyGetterInvokedDuringReceivedInOrder()
  {
    //GIVEN
    var obj = Any.Instance<ISimple>();
    //WHEN

    //THEN
    obj.Invoking(o => Received.InOrder(() =>
      {
        var x = o.GetStringProperty;
      })).Should()
      .NotThrow();
  }

  [Test, Parallelizable]
  public void ShouldCreateNonNullUri()
  {
    Assert.That(Any.Uri(), Is.Not.Null);
  }

  [Test, Parallelizable]
  public void ShouldGenerateDifferentMethodInfoEachTime()
  {
    //GIVEN
    var method1 = Any.Method();
    var method2 = Any.Method();

    //THEN
    Assert.That(method1, Is.Not.Null);
    Assert.That(method2, Is.Not.Null);
    Assert.That(method2, Is.Not.EqualTo(method1));
  }

  [Test, Parallelizable]
  public void ShouldGenerateFiniteEnumerables()
  {
    //GIVEN
    var o = Any.Instance<ISimple>();

    //WHEN
    var enumerable = o.Simples;

    //THEN

    foreach (var simple in enumerable)
    {
      Assert.That(simple, Is.Not.Null);
    }
  }

  [Test, Parallelizable]
  public void ShouldGenerateMembersReturningTypeOfType()
  {
    //GIVEN
    var obj1 = Any.Instance<ISimple>();
    var obj2 = Any.Instance<ISimple>();

    //THEN
    using (Assert.EnterMultipleScope())
    {
      Assert.That(obj1.GetTypeProperty, Is.Not.Null);
      Assert.That(obj2.GetTypeProperty, Is.Not.Null);
      Assert.That(obj2.GetTypeProperty, Is.Not.EqualTo(obj1.GetTypeProperty));
      Assert.That(obj1.GetTypeProperty, Is.EqualTo(obj1.GetTypeProperty));
      Assert.That(obj2.GetTypeProperty, Is.EqualTo(obj2.GetTypeProperty));
    }
  }

  [Test, Parallelizable]
  public void ShouldBeAbleToGenerateInstancesOfConcreteClassesWithInterfacesAsTheirConstructorArguments()
  {
    //GIVEN
    var createdProxy = Any.Instance<ObjectWithInterfaceInConstructor>();

    //THEN
    Assert.That(createdProxy._constructorArgument, Is.Not.Null);
    Assert.That(createdProxy._constructorNestedArgument, Is.Not.Null);
  }

  [Test, Parallelizable]
  public void ShouldBeAbleToGenerateInstancesOfAbstractClasses()
  {
    //GIVEN
    var createdProxy = Any.Instance<AbstractObjectWithInterfaceInConstructor>();

    //THEN
    using (Assert.EnterMultipleScope())
    {
      Assert.That(createdProxy._constructorArgument, Is.Not.Null);
      Assert.That(createdProxy._constructorNestedArgument, Is.Not.Null);
      Assert.That(createdProxy.AbstractInt, Is.Not.Default);
      Assert.That(createdProxy.SettableInt, Is.Not.Default);
    }
  }

  [Test, Parallelizable]
  public void ShouldOverrideVirtualMethodsThatReturnDefaultTypeValuesOnAbstractClassProxy()
  {
    //GIVEN
    var obj = Any.Instance<AbstractObjectWithVirtualMethods>();

    //THEN
    Assert.That(obj.GetSomething(), Is.Not.Null);
    Assert.That(obj.GetSomething2(), Is.Not.EqualTo("Something"));
  }

  [Test, Parallelizable]
  public void ShouldBeAbleToCreateInstanceOfClassWithInConstructorParameter()
  {
    //GIVEN
    var obj = Any.Instance<ObjectWithInConstructorParameter>();

    //THEN
    obj.Should().NotBeNull();
    obj.A.Should().NotBe(default);
  }

  [Test, Parallelizable]
  public void ShouldOverrideVirtualMethodsThatThrowExceptionsOnAbstractClassProxy()
  {
    //GIVEN
    var obj = Any.Instance<AbstractObjectWithVirtualMethods>();

    //THEN
    Assert.That(obj.GetSomethingButThrowExceptionWhileGettingIt(), Is.Not.Null);
  }

  [Test, Parallelizable]
  public void ShouldNotCreateTheSameMethodInfoTwiceInARow()
  {
    //GIVEN
    var x = Any.Instance<MethodInfo>();
    var y = Any.Instance<MethodInfo>();
    var z = Any.Instance<ObjectWithMethodInfo>();

    //THEN
    Assert.That(y, Is.Not.EqualTo(x));
    Assert.That(z.Method, Is.Not.Null);
    Assert.That(z.Method, Is.Not.EqualTo(y));
  }

  [Test, Parallelizable]
  public void ShouldCreateDifferentExceptionEachTime()
  {
    //GIVEN
    var exception1 = Any.Instance<Exception>();
    var exception2 = Any.Instance<Exception>();

    //THEN
    NotAlike(exception2, exception1);
  }

  [Test, Repeat(5)]
  public void ShouldGenerateDifferentEnumValueEachTimeBesidesSpecified()
  {
    //WHEN
    var dof1 = Any.OtherThan(DayOfWeek.Sunday);
    var dof2 = Any.OtherThan(DayOfWeek.Sunday);
    var dof3 = Any.OtherThan(DayOfWeek.Sunday);
    var dof4 = Any.OtherThan(DayOfWeek.Sunday);
    var dof5 = Any.OtherThan(DayOfWeek.Sunday);
    var dof6 = Any.OtherThan(DayOfWeek.Sunday);
    var dof7 = Any.OtherThan(DayOfWeek.Sunday);

    //THEN
    Assert.That(new[] { dof1, dof2, dof3, dof4, dof5, dof6 }, Is.Unique);
    Assert.That(new[] { dof1, dof2, dof3, dof4, dof5, dof6, dof7 }, Has.No.Member(DayOfWeek.Sunday));
  }

  [Test, Parallelizable]
  public void ShouldDisallowSkippingTheSameValueTwiceWhenGeneratingAnyValueOtherThan()
  {
    Any.Invoking(a => a.OtherThan(2, 2))
      .Should().Throw<Exception>();
  }

  [Test, CancelAfter(2000)]
  public void ShouldDisallowSkippingAllEnumMembers()
  {
    Any.Invoking(a => a.OtherThan(
        LolEnum.Value2,
        LolEnum.Value1,
        LolEnum.Value4,
        LolEnum.Value5,
        LolEnum.Value3,
        LolEnum.Value6))
      .Should().Throw<Exception>();
  }

  [Test, Parallelizable]
  public void ShouldGenerateDifferentValueEachTime()
  {
    //WHEN
    var dof1 = Any.Instance<DayOfWeek>();
    var dof2 = Any.Instance<DayOfWeek>();
    var dof3 = Any.Instance<DayOfWeek>();
    var dof4 = Any.Instance<DayOfWeek>();
    var dof5 = Any.Instance<DayOfWeek>();
    var dof6 = Any.Instance<DayOfWeek>();
    var dof7 = Any.Instance<DayOfWeek>();

    //THEN
    Assert.That(new[] { dof1, dof2, dof3, dof4, dof5, dof6, dof7 }, Is.Unique);
  }

  [Test, Parallelizable]
  public void ShouldGenerateDifferentValueEachTimeAndNotAmongPassedOnesWhenAskedToCreateAnyValueBesidesGiven()
  {
    //WHEN
    var int1 = Any.OtherThan(1, 2, 3, 4, 5, 6, 7, 8, 9, 10);
    var int2 = Any.OtherThan(1, 2, 3, 4, 5, 6, 7, 8, 9, 10);

    //THEN
    Assert.That(int2, Is.Not.EqualTo(int1));
    Assert.That(int1, Is.Not.InRange(1, 10));
    Assert.That(int2, Is.Not.InRange(1, 10));
  }

  [Test, Parallelizable]
  [Description("Non-deterministic statement - check it out")]
  public void ShouldGeneratePickNextValueEachTimeFromPassedOnesWhenAskedToCreateAnyValueFromGiven()
  {
    //WHEN
    var int1 = Any.From(Enumerable.Range(1, 3).ToArray());
    var int2 = Any.From(Enumerable.Range(1, 3).ToArray());
    var int3 = Any.From(Enumerable.Range(1, 3).ToArray());
    var int4 = Any.From(Enumerable.Range(1, 3).ToArray());
    var int5 = Any.From(Enumerable.Range(5, 2).ToArray());
    var int6 = Any.From(Enumerable.Range(10, 4).ToArray());

    //THEN
    using (Assert.EnterMultipleScope())
    {
      Assert.That(Enumerable.Range(1, 3).Contains(int1), Is.True);
      Assert.That(Enumerable.Range(1, 3).Contains(int2), Is.True);
      Assert.That(Enumerable.Range(1, 3).Contains(int3), Is.True);
      Assert.That(Enumerable.Range(1, 3).Contains(int4), Is.True);
      Assert.That(int2, Is.Not.EqualTo(int1));
      Assert.That(int3, Is.Not.EqualTo(int2));
      Assert.That(int4, Is.Not.EqualTo(int3));

      Assert.That(Enumerable.Range(5, 2).Contains(int5), Is.True);
      Assert.That(Enumerable.Range(10, 4).Contains(int6), Is.True);
    }
  }

  [Test, Parallelizable]
  public void ShouldGenerateStringAccordingtoRegex()
  {
    //GIVEN
    const string exampleRegex = @"content/([A-Za-z0-9\-]+)\.aspx$";

    //WHEN
    var result = Any.StringMatching(exampleRegex);

    //THEN
    Assert.That(ExampleRegex().IsMatch(result), Is.True);
  }

  [TestCase(2)]
  [TestCase(5)]
  [TestCase(12)]
  public void ShouldGenerateStringOfGivenLength(int stringLength)
  {
    //WHEN
    var str = Any.String(stringLength);

    //THEN
    Assert.That(str.Length, Is.EqualTo(stringLength));
  }

  [Test, Parallelizable]
  public void ShouldCreateSortedSetWithThreeDistinctValues()
  {
    //WHEN
    var set = Any.SortedSet<int>();

    //THEN
    Assert.That(set, Is.Ordered);
    Assert.That(set, Is.Unique);
    Assert.That(set.Count, Is.EqualTo(3));
  }

  [Test, Parallelizable]
  public void ShouldBeAbleToGenerateDistinctLettersEachTime()
  {
    //WHEN
    var char1 = Any.AlphaChar();
    var char2 = Any.AlphaChar();
    var char3 = Any.AlphaChar();

    //THEN
    using (Assert.EnterMultipleScope())
    {
      Assert.That(char2, Is.Not.EqualTo(char1));
      Assert.That(char3, Is.Not.EqualTo(char2));
      Assert.That(char.IsLetter(char1), Is.True);
      Assert.That(char.IsLetter(char2), Is.True);
      Assert.That(char.IsLetter(char3), Is.True);
    }
  }

  [Test, Parallelizable]
  public void ShouldBeAbleToGenerateDistinctDigitsEachTime()
  {
    //WHEN
    var char1 = Any.DigitChar();
    var char2 = Any.DigitChar();
    var char3 = Any.DigitChar();

    //THEN
    using (Assert.EnterMultipleScope())
    {
      Assert.That(char2, Is.Not.EqualTo(char1));
      Assert.That(char3, Is.Not.EqualTo(char2));
      Assert.That(char.IsDigit(char1), Is.True);
      Assert.That(char.IsDigit(char2), Is.True);
      Assert.That(char.IsDigit(char3), Is.True);
    }
  }

  [Test, CancelAfter(1000)]
  public void ShouldHandleEmptyExcludedStringsWhenGeneratingAnyStringNotContainingGiven()
  {
    Assert.DoesNotThrow(() => Any.StringNotContaining(string.Empty));
  }

  [Test, Repeat(100)]
  public void ShouldGenerateStringsWithoutASpecificSubstring() =>
    Any.StringNotContaining("0").Should().NotContain("0");

  [Test, Repeat(100)]
  public void ShouldGenerateStringsWithoutGenericExcludedObject()
  {
    //WHEN
    var excludedObject = Guid.Parse("12345678-1234-1234-1234-123456789abc");

    //THEN
    Any.StringNotContaining(excludedObject).Should().NotContain(excludedObject.ToString());
  }

  [Test, Repeat(100)]
  public void ShouldGenerateSeededString()
  {
    Any.String("0").Should().StartWith("0");
    Any.String("0").Length.Should().BeGreaterThan(1);
    Any.String("0").Should().NotBe(Any.String("0"));
  }

  [Test, Parallelizable]
  public void ShouldGenerateStringThroughExtension()
  {
    //WHEN
    var value1 = Any.String();
    var value2 = Any.String();

    //THEN
    Assert.That(value1, Is.Not.Null.And.Not.Empty);
    Assert.That(value2, Is.Not.Null.And.Not.Empty);
    Assert.That(value2, Is.Not.EqualTo(value1));
  }

  [Test, Parallelizable]
  public void ShouldGenerateLowerCaseStringThroughExtension()
  {
    //WHEN
    var value = Any.LowerCaseString();

    //THEN
    Assert.That(value, Is.Not.Null.And.Not.Empty);
    Assert.That(value, Is.EqualTo(value.ToLowerInvariant()));
  }

  [Test, Parallelizable]
  public void ShouldGenerateUpperCaseStringThroughExtension()
  {
    //WHEN
    var value = Any.UpperCaseString();

    //THEN
    Assert.That(value, Is.Not.Null.And.Not.Empty);
    Assert.That(value, Is.EqualTo(value.ToUpperInvariant()));
  }

  [Test, Parallelizable]
  public void ShouldGenerateLowerCaseAlphaStringThroughExtension()
  {
    //WHEN
    var value = Any.LowerCaseAlphaString();

    //THEN
    Assert.That(value, Is.Not.Null.And.Not.Empty);
    Assert.That(value.All(c => char.IsLower(c) && char.IsLetter(c)), Is.True);
  }

  [Test, Parallelizable]
  public void ShouldGenerateUpperCaseAlphaStringThroughExtension()
  {
    //WHEN
    var value = Any.UpperCaseAlphaString();

    //THEN
    Assert.That(value, Is.Not.Null.And.Not.Empty);
    Assert.That(value.All(c => char.IsUpper(c) && char.IsLetter(c)), Is.True);
  }

  [Test, Parallelizable]
  public void ShouldGenerateStringContainingStringThroughExtension()
  {
    //WHEN
    var value = Any.StringContaining("copilot");

    //THEN
    Assert.That(value, Is.Not.Null.And.Not.Empty);
    Assert.That(value, Does.Contain("copilot"));
  }

  [Test, Parallelizable]
  public void ShouldGenerateStringContainingObjectThroughExtension()
  {
    //WHEN
    var value = Any.StringContaining(12345);

    //THEN
    Assert.That(value, Is.Not.Null.And.Not.Empty);
    Assert.That(value, Does.Contain("12345"));
  }

  [Test, Parallelizable]
  public void ShouldGenerateStringOtherThanThroughExtension()
  {
    //WHEN
    var value = Any.StringOtherThan("alpha", "beta");

    //THEN
    Assert.That(value, Is.Not.Null.And.Not.Empty);
    Assert.That(value, Is.Not.AnyOf("alpha", "beta"));
  }

  [Test, Parallelizable]
  public void ShouldGenerateAlphaStringThroughExtension()
  {
    //WHEN
    var value = Any.AlphaString();

    //THEN
    Assert.That(value, Is.Not.Null.And.Not.Empty);
    Assert.That(value.All(char.IsLetter), Is.True);
  }

  [Test, Parallelizable]
  public void ShouldGenerateAlphaStringOfSpecifiedLengthThroughExtension()
  {
    //WHEN
    var value = Any.AlphaString(5);

    //THEN
    Assert.That(value, Has.Length.EqualTo(5));
    Assert.That(value.All(char.IsLetter), Is.True);
  }

  [Test, Parallelizable]
  public void ShouldGenerateIdentifierThroughExtension()
  {
    //WHEN
    var value = Any.Identifier();

    //THEN
    Assert.That(value, Does.Match("^[A-Za-z](?:[0-9][A-Za-z]){5}$"));
  }

  [Test, Parallelizable]
  public void ShouldGenerateCharThroughExtension()
  {
    //WHEN
    var value = Any.Char();

    //THEN
    Assert.That(value, Is.Not.EqualTo(default(char)));
  }

  [Test, Parallelizable]
  public void ShouldGenerateLowerCaseAlphaCharThroughExtension()
  {
    //WHEN
    var value = Any.LowerCaseAlphaChar();

    //THEN
    Assert.That(char.IsLower(value), Is.True);
    Assert.That(char.IsLetter(value), Is.True);
  }

  [Test, Parallelizable]
  public void ShouldGenerateUpperCaseAlphaCharThroughExtension()
  {
    //WHEN
    var value = Any.UpperCaseAlphaChar();

    //THEN
    Assert.That(char.IsUpper(value), Is.True);
    Assert.That(char.IsLetter(value), Is.True);
  }


  [Test, Parallelizable]
  public void ShouldBeAbleToGenerateBothPrimitiveTypeInstanceAndInterfaceUsingNewInstanceMethod()
  {
    var primitive = Any.Instance<int>();
    var interfaceImplementation = Any.Instance<ISimple>();

    using (Assert.EnterMultipleScope())
    {
      Assert.That(interfaceImplementation, Is.Not.Null);
      Assert.That(primitive, Is.Not.Default);
    }
  }

  [Test, Parallelizable]
  public void ShouldSupportRecursiveInterfacesWithLists()
  {
    var recursiveObjects = Any.Enumerable<RecursiveInterface>().ToList();

    var x2 = recursiveObjects[0].GetNested()[0].Nested.Number;

    Assert.That(x2, Is.Not.Default);
  }

  [Test, Parallelizable]
  public void ShouldSupportGeneratingOtherObjectsThanNull()
  {
    Assert.DoesNotThrow(() => Any.OtherThan<string>(null!));
  }

  [Test, Parallelizable]
  public void ShouldSupportRecursiveInterfacesWithDictionaries()
  {
    var factories = Any.Enumerable<RecursiveInterface>().ToList();

    var x = factories[0];
    var y = x.NestedAsDictionary;
    var y1 = y.Keys.First();
    var y2 = y.Values.First();

    using (Assert.EnterMultipleScope())
    {
      Assert.That(y.Count, Is.EqualTo(3));
      Assert.That(y1, Is.Not.Null);
      Assert.That(y2, Is.Not.Null);
    }
  }

  [Test, Parallelizable]
  public void ShouldSupportGeneratingRangedCollections()
  {
    const int anyCount = 4;
    var list = Any.List<RecursiveInterface>(anyCount);
    var array = Any.Array<RecursiveInterface>(anyCount);
    var set = Any.Set<RecursiveInterface>(anyCount);
    var dictionary = Any.Dictionary<RecursiveInterface, ISimple>(anyCount);
    var sortedList = Any.SortedList<string, ISimple>(anyCount);
    var sortedDictionary = Any.SortedDictionary<string, ISimple>(anyCount);
    var sortedEnumerable = Any.EnumerableSortedDescending<string>(anyCount);
    var enumerable = Any.Enumerable<RecursiveInterface>(anyCount);
    var concurrentDictionary = Any.ConcurrentDictionary<string, ISimple>(anyCount);
    var concurrentBag = Any.ConcurrentBag<string>(anyCount);
    var concurrentQueue = Any.ConcurrentQueue<string>(anyCount);
    var concurrentStack = Any.ConcurrentStack<string>(anyCount);

    using (Assert.EnterMultipleScope())
    {
      Assert.That(list.Count, Is.EqualTo(anyCount));
      Assert.That(enumerable.Count(), Is.EqualTo(anyCount));
      Assert.That(array.Length, Is.EqualTo(anyCount));
      Assert.That(set.Count, Is.EqualTo(anyCount));
      Assert.That(dictionary.Count, Is.EqualTo(anyCount));
      Assert.That(sortedList.Count, Is.EqualTo(anyCount));
      Assert.That(sortedDictionary.Count, Is.EqualTo(anyCount));
      Assert.That(concurrentDictionary.Count, Is.EqualTo(anyCount));
      Assert.That(sortedEnumerable.Count(), Is.EqualTo(anyCount));
      Assert.That(concurrentBag.Count, Is.EqualTo(anyCount));
      Assert.That(concurrentStack.Count, Is.EqualTo(anyCount));
      Assert.That(concurrentQueue.Count, Is.EqualTo(anyCount));
    }
  }

  [Test, Parallelizable]
  public void ShouldSupportGeneratingCollections()
  {
    const int anyCount = 3;
    var list = Any.List<RecursiveInterface>();
    var array = Any.Array<RecursiveInterface>();
    var set = Any.Set<RecursiveInterface>();
    var dictionary = Any.Dictionary<RecursiveInterface, ISimple>();
    var sortedList = Any.SortedList<string, ISimple>();
    var sortedDictionary = Any.SortedDictionary<string, ISimple>();
    var sortedEnumerable = Any.EnumerableSortedDescending<string>();
    var enumerable = Any.Enumerable<RecursiveInterface>();
    var concurrentDictionary = Any.ConcurrentDictionary<string, ISimple>();
    var concurrentBag = Any.ConcurrentBag<string>();
    var concurrentQueue = Any.ConcurrentQueue<string>();
    var concurrentStack = Any.ConcurrentStack<string>();
    var frozenSet = Any.FrozenSet<string>();
    var frozenDictionary = Any.FrozenDictionary<string, ISimple>();

    using (Assert.EnterMultipleScope())
    {
      Assert.That(list.Count, Is.EqualTo(anyCount));
      Assert.That(enumerable.Count(), Is.EqualTo(anyCount));
      Assert.That(array.Length, Is.EqualTo(anyCount));
      Assert.That(set.Count, Is.EqualTo(anyCount));
      Assert.That(dictionary.Count, Is.EqualTo(anyCount));
      Assert.That(sortedList.Count, Is.EqualTo(anyCount));
      Assert.That(sortedDictionary.Count, Is.EqualTo(anyCount));
      Assert.That(sortedEnumerable.Count(), Is.EqualTo(anyCount));
      Assert.That(concurrentDictionary.Count, Is.EqualTo(anyCount));
      Assert.That(concurrentBag.Count, Is.EqualTo(anyCount));
      Assert.That(concurrentStack.Count, Is.EqualTo(anyCount));
      Assert.That(concurrentQueue.Count, Is.EqualTo(anyCount));
      Assert.That(frozenSet.Count, Is.EqualTo(anyCount));
      Assert.That(frozenDictionary.Count, Is.EqualTo(anyCount));
    }
  }

  [Test, Parallelizable]
  public void ShouldSupportGeneratingCollectionsUsingGenericInstanceMethod()
  {
    const int anyCount = 3;
    var list = Any.Instance<List<RecursiveInterface>>();
    var array = Any.Instance<RecursiveInterface[]>();
    var set = Any.Instance<HashSet<RecursiveInterface>>();
    var dictionary = Any.Instance<Dictionary<RecursiveInterface, ISimple>>();
    var sortedList = Any.Instance<SortedList<string, ISimple>>();
    var sortedDictionary = Any.Instance<SortedDictionary<string, ISimple>>();
    var enumerable = Any.Instance<IEnumerable<RecursiveInterface>>();
    var concurrentDictionary = Any.Instance<ConcurrentDictionary<string, ISimple>>();
    var concurrentStack = Any.Instance<ConcurrentStack<string>>();
    var concurrentBag = Any.Instance<ConcurrentBag<string>>();
    var concurrentQueue = Any.Instance<ConcurrentQueue<string>>();
    var frozenSet = Any.Instance<FrozenSet<string>>();
    var frozenDictionary = Any.Instance<FrozenDictionary<string, ISimple>>();

    using (Assert.EnterMultipleScope())
    {
      Assert.That(list.Count, Is.EqualTo(anyCount));
      Assert.That(enumerable.Count(), Is.EqualTo(anyCount));
      Assert.That(array.Length, Is.EqualTo(anyCount));
      Assert.That(set.Count, Is.EqualTo(anyCount));
      Assert.That(dictionary.Count, Is.EqualTo(anyCount));
      Assert.That(sortedList.Count, Is.EqualTo(anyCount));
      Assert.That(sortedDictionary.Count, Is.EqualTo(anyCount));
      Assert.That(concurrentDictionary.Count, Is.EqualTo(anyCount));
      Assert.That(concurrentStack.Count, Is.EqualTo(anyCount));
      Assert.That(concurrentBag.Count, Is.EqualTo(anyCount));
      Assert.That(concurrentQueue.Count, Is.EqualTo(anyCount));
      Assert.That(frozenSet.Count, Is.EqualTo(anyCount));
      Assert.That(frozenDictionary.Count, Is.EqualTo(anyCount));
    }
  }

  [Test, Parallelizable]
  public void ShouldAllowCreatingCustomCollectionInstances()
  {
    var customCollection = Any.Instance<MyOwnCollection<RecursiveInterface>>();

    Assert.That(customCollection.Count, Is.EqualTo(3));
    foreach (var recursiveInterface in customCollection)
    {
      Assert.That(recursiveInterface, Is.Not.Null);
    }
  }

  [Test, Parallelizable]
  public void ShouldAllowCreatingCustomProducedConsumerCollectionInstances()
  {
    var customCollection = Any.Instance<MyOwnPcCollection<RecursiveInterface>>();

    Assert.That(customCollection.Count, Is.EqualTo(3));
    foreach (var recursiveInterface in customCollection)
    {
      Assert.That(recursiveInterface, Is.Not.Null);
    }
  }

  [Test, Parallelizable]
  public void ShouldSupportCreatingArraysWithSpecificLiteralElements()
  {
    var array = Any.ArrayWith(1, 2, 3);

    Assert.That(array, Has.Member(1));
    Assert.That(array, Has.Member(2));
    Assert.That(array, Has.Member(3));
    Assert.That(array.Length, Is.GreaterThanOrEqualTo(3));
  }

  [Test, Parallelizable]
  public void ShouldSupportCreatingListsWithSpecificLiteralElements()
  {
    var list = Any.ListWith(1, 2, 3);

    Assert.That(list, Has.Member(1));
    Assert.That(list, Has.Member(2));
    Assert.That(list, Has.Member(3));
    Assert.That(list.Count, Is.GreaterThanOrEqualTo(3));
  }

  [Test, Parallelizable]
  public void ShouldSupportCreatingListsWithSpecificEnumerableOfElements()
  {
    var array = Any.ListWith<int>(new List<int> {1, 2, 3});

    Assert.That(array, Has.Member(1));
    Assert.That(array, Has.Member(2));
    Assert.That(array, Has.Member(3));
    Assert.That(array.Count, Is.GreaterThanOrEqualTo(3));
  }

  [Test, Parallelizable]
  public void ShouldSupportCreatingArraysWithSpecificEnumerableOfElements()
  {
    var array = Any.ArrayWith<int>(new List<int> {1, 2, 3});

    Assert.That(array, Has.Member(1));
    Assert.That(array, Has.Member(2));
    Assert.That(array, Has.Member(3));
    Assert.That(array.Length, Is.GreaterThanOrEqualTo(3));
  }

  [Test, Parallelizable]
  public void ShouldSupportCreatingArrays()
  {
    Any.Array<int>().Should().HaveCount(3).And.OnlyHaveUniqueItems();
    Any.Instance<int[]>().Should().HaveCount(3).And.OnlyHaveUniqueItems();
  }

  [Test, Parallelizable]
  public void ShouldSupportCreatingLazies()
  {
    Any.Instance<Lazy<string>>().Value.Should().NotBeNull();
  }

  [Test, Parallelizable]
  public void ShouldSupportCreatingMultidimensionalArrays()
  {
    var instance = Any.Instance<string[][]>();
    instance.Should().NotBeNull();
    instance.Should().HaveCount(3);
    instance.Should().OnlyHaveUniqueItems();
    instance[0].Should().NotBeNull();
    instance[0].Should().HaveCount(3);
    instance[0].Should().OnlyHaveUniqueItems();
    instance[1].Should().NotBeNull();
    instance[1].Should().HaveCount(3);
    instance[1].Should().OnlyHaveUniqueItems();
    instance[2].Should().NotBeNull();
    instance[2].Should().HaveCount(3);
    instance[2].Should().OnlyHaveUniqueItems();
  }

  [Test, Parallelizable]
  public void ShouldSupportCreationOfKeyValuePairs()
  {
    var kvp = Any.Instance<KeyValuePair<string, RecursiveInterface>>();
    using (Assert.EnterMultipleScope())
    {
      Assert.That(kvp.Key, Is.Not.Null);
      Assert.That(kvp.Value, Is.Not.Null);
    }
  }

  [Test, Parallelizable]
  public void ShouldSupportActions()
  {
    //WHEN
    var action = Any.Instance<Action<ISimple, string>>();

    //THEN
    Assert.That(action, Is.Not.Null);
  }

  [Test, Parallelizable]
  public void ShouldAllowCreatingDifferentMaybesOfConcreteClasses()
  {
    var maybeString1 = Any.Instance<Maybe<string>>();
    var maybeString2 = Any.Instance<Maybe<string>>();

    Assert.That(maybeString2, Is.Not.EqualTo(maybeString1));
    maybeString1.Should().NotBe(Maybe<string>.Nothing);
    maybeString2.Should().NotBe(Maybe<string>.Nothing);
  }

  [Test, Parallelizable]
  public void ShouldAllowCreatingMaybesAsPartOfOtherClasses()
  {
    var maybeObject1 = Any.Instance<ObjectWithMaybe>();
    var maybeObject2 = Any.Instance<ObjectWithMaybe>();

    Assert.That(maybeObject2.Property, Is.Not.EqualTo(maybeObject1.Property));
    Assert.That(maybeObject2._field, Is.Not.EqualTo(maybeObject1._field));
    maybeObject1.Property.Should().NotBe(Maybe<ObjectWithMaybe>.Nothing);
    maybeObject1._field.Should().NotBe(Maybe<ObjectWithMaybe>.Nothing);
    maybeObject2.Property.Should().NotBe(Maybe<ObjectWithMaybe>.Nothing);
    maybeObject2._field.Should().NotBe(Maybe<ObjectWithMaybe>.Nothing);
  }

  [Test, Parallelizable]
  public void ShouldAllowCreatingDifferentMaybesOfInterfaces()
  {
    var maybeImplementation1 = Any.Instance<Maybe<RecursiveInterface>>();
    var maybeImplementation2 = Any.Instance<Maybe<RecursiveInterface>>();

    Assert.That(maybeImplementation2, Is.Not.EqualTo(maybeImplementation1));
    maybeImplementation1.Should().NotBe(Maybe<string>.Nothing);
    maybeImplementation2.Should().NotBe(Maybe<string>.Nothing);
  }

  [Test, Parallelizable]
  public void ShouldAllowCreatingEnumValuesOutsideTheValidRange()
  {
    var invalidEnumMember1 = Any.Invalid<LolEnum>();
    var invalidEnumMember2 = Any.Invalid<LolEnum>();
    invalidEnumMember1.Should().NotBe(invalidEnumMember2);
    Enum.GetValues<LolEnum>().Should().NotContain(invalidEnumMember1);
    Enum.GetValues<LolEnum>().Should().NotContain(invalidEnumMember2);

    var invalidEnumMember3 = Any.Invalid<LolEnumShort>();
    var invalidEnumMember4 = Any.Invalid<LolEnumShort>();

    invalidEnumMember3.Should().NotBe(invalidEnumMember4);
    Enum.GetValues<LolEnumShort>().Should().NotContain(invalidEnumMember3);
    Enum.GetValues<LolEnumShort>().Should().NotContain(invalidEnumMember4);

    var invalidEnumMember5 = Any.Invalid<LolEnumByte>();
    var invalidEnumMember6 = Any.Invalid<LolEnumByte>();

    invalidEnumMember5.Should().NotBe(invalidEnumMember6);
    Enum.GetValues<LolEnumByte>().Should().NotContain(invalidEnumMember5);
    Enum.GetValues<LolEnumByte>().Should().NotContain(invalidEnumMember6);
  }

  [Test, Parallelizable]
  public void ShouldBeAbleToGenerateFiniteInstancesOfGenericEnumerators()
  {
    //GIVEN
    var enumerator = Any.Instance<IEnumerator<string>>();

    //WHEN
    var element1 = enumerator.Current;
    enumerator.MoveNext();
    var element2 = enumerator.Current;

    //THEN
    Assert.That(element1, Is.Not.EqualTo(element2));
  }

  [Test, Parallelizable]
  public void ShouldBeAbleToGenerateFiniteInstancesOfNonGenericEnumerators()
  {
    //GIVEN
    var enumerator = Any.Instance<IEnumerator>();

    //WHEN
    enumerator.MoveNext();
    var element1 = enumerator.Current;
    enumerator.MoveNext();
    var element2 = enumerator.Current;

    //THEN
    Assert.That(element1, Is.Not.EqualTo(element2));
  }

  [Test, Parallelizable]
  public void ShouldBeAbleToGenerateInstancesOfGenericKeyValueEnumerables()
  {
    //GIVEN
    var instance = Any.Instance<IObservableConcurrentDictionary<string, string>>();

    //WHEN
    var element1 = instance.GetEnumerator().Current;
    instance.GetEnumerator().MoveNext();
    var element2 = instance.GetEnumerator().Current;

    //THEN
    Assert.That(element1, Is.Not.EqualTo(element2));
  }

  [Test, Parallelizable]
  public void ShouldAllowGeneratingIntegersFromSequence()
  {
    var value1 = Any.IntegerFromSequence(startingValue: 12, step: 112);
    var value2 = Any.IntegerFromSequence(startingValue: 12, step: 112);

    Assert.That(value2 - 112, Is.EqualTo(value1));
    Assert.That(value1, Is.GreaterThan(12));
  }

  [Test, Parallelizable]
  public void ShouldAllowGeneratingDivisibleIntegers()
  {
    var value1 = Any.IntegerDivisibleBy(5);
    var value2 = Any.IntegerDivisibleBy(5);

    Assert.That(value2, Is.Not.EqualTo(value1));
    Assert.That(value1 % 5, Is.Zero);
    Assert.That(value2 % 5, Is.Zero);
  }

  [Test, Parallelizable]
  public void ShouldAllowGeneratingNotDivisibleIntegers()
  {
    var value1 = Any.IntegerNotDivisibleBy(5);
    var value2 = Any.IntegerNotDivisibleBy(5);

    Assert.That(value2, Is.Not.EqualTo(value1));
    Assert.That(value1 % 5, Is.Not.Zero);
    Assert.That(value2 % 5, Is.Not.Zero);
  }

  [Test, Parallelizable]
  public void ShouldAllowGeneratingDummyObjectsBypassingConstructors()
  {
    Alike(Enumerable.Empty<string>(), Any.Dummy<IEnumerable<string>>());
    Alike(new List<string>(), Any.Dummy<List<string>>());
    Alike(new List<string>(), Any.Dummy<IList<string>>());
    Alike(new List<string>(), Any.Dummy<ICollection<string>>());
    Alike(Array.Empty<string>(), Any.Dummy<string[]>());
    Alike(Array.Empty<RecursiveClass>(), Any.Dummy<RecursiveClass[]>());
    Alike(new Dictionary<int, int>(), Any.Dummy<IDictionary<int, int>>());
    Alike(new Dictionary<int, int>(), Any.Dummy<IDictionary<int, int>>());
    using (Assert.EnterMultipleScope())
    {
      Assert.Throws<GenerationFailedException>(() => Any.Instance<ThrowingInConstructor>());
      Assert.That(Any.Dummy<ThrowingInConstructor>(), Is.Not.Null);
      Assert.That(Any.Dummy<string>(), Is.Not.Null);
      Assert.That(Any.Dummy<int>(), Is.Not.Default);
    }
  }

  [Test, Parallelizable]
  public void ShouldAllowCustomizingTheFallbackGenerator()
  {
    Assert.DoesNotThrow(() =>
      Any.Instance<ObjectWithThrowingDependency>(
        new SingleTypeCustomization<ThrowingInConstructor>(
          (gen, trace) => gen.Dummy<ThrowingInConstructor>(trace))));
  }

  [Test, Parallelizable]
  public void ShouldAllowCustomizationsToReachInnerAutoFixture()
  {

    var anyConcrete = Any.Instance<MyComplexObject>(
      new SingleTypeCustomization<string>((gen, trace) => "CustomString"),
      new SingleTypeCustomization<MyInnerObject>((gen, trace) => new MyInnerObject
      {
        InnerDummyInt = 123,
        InnerDummyString = "InnerCustomString"
      }));

    anyConcrete.DummyString.Should().Be("CustomString");
    anyConcrete.Inner.OrThrow().InnerDummyInt.Should().Be(123);
    anyConcrete.Inner.OrThrow().InnerDummyString.Should().Be("InnerCustomString");
  }

  [TestCase(LolEnum.Value1)]
  [TestCase(LolEnum.Value2)]
  [TestCase(LolEnum.Value3)]
  [TestCase(LolEnum.Value4)]
  [TestCase(LolEnum.Value5)]
  [TestCase(LolEnum.Value6)]
  public void ShouldAllowCustomizationsForSingleEnumElement(LolEnum value)
  {
    var anyConcrete = Any.Instance<ObjectWithLolEnum>(new SingleTypeCustomization<LolEnum>((_, _) => value));
    anyConcrete.Lol.Should().Be(value);
  }

  [Test, Parallelizable]
  public void ShouldAllowGenericCustomizations()
  {
    var anyConcrete = Any.Instance<ObjectWithGenericCollection<int>>(
      new GenericListCustomization());
    anyConcrete.MyList.Should().HaveCount(4);
  }

  [Test, Parallelizable]
  public void ShouldAllowGenericCustomizationsForOptional()
  {
    var anyConcrete = Any.Instance<ObjectWithGenericOption<string>>();
    var anyConcrete2 = Any.Instance<ObjectWithGenericOption<string>>();
    var anyOptional = Any.Instance<Option<string>>();

    anyConcrete.MyOption.HasValue.Should().BeTrue();
    var value1 = anyConcrete.MyOption.ValueOr(() => throw new Exception());
    value1.Should().NotBe(null);
    anyConcrete2.MyOption.HasValue.Should().BeTrue();
    var value2 = anyConcrete2.MyOption.ValueOr(() => throw new Exception());
    value2.Should().NotBe(null);
    value1.Should().NotBe(value2);
    anyOptional.HasValue.Should().BeTrue();
    anyOptional.ValueOrDefault().Should().NotBeNull();
  }

  [Test, Parallelizable]
  public void ShouldAllowGenericCustomizationsForOptionalWithDeeperNesting()
  {
    var anyConcrete = Any.Instance<ObjectWrappingObjectWithGenericOption<string>>();
    var anyConcrete2 = Any.Instance<ObjectWrappingObjectWithGenericOption<string>>();

    anyConcrete.Obj.MyOption.HasValue.Should().BeTrue();
    var value1 = anyConcrete.Obj.MyOption.ValueOr(() => throw new Exception());
    value1.Should().NotBe(null);
    anyConcrete2.Obj.MyOption.HasValue.Should().BeTrue();
    var value2 = anyConcrete2.Obj.MyOption.ValueOr(() => throw new Exception());
    value2.Should().NotBe(null);
    value1.Should().NotBe(value2);
  }

  [Test, Parallelizable]
  public void ShouldGenerateComplexGraphsWithNonNullPublicProperties()
  {
    var entity = Any.Instance<AreaEntity>();
    Assert.That(entity.Feature, Is.Not.Null);
  }

  [Test, Parallelizable]
  public void ShouldAllowAccessToValuesSetOnPropertiesOnInterfaceInstancesWhenBothGetAndSetArePublic()
  {
    //GIVEN
    var someValue = Any.Integer();
    var obj = Any.Instance<IGetSettable<int>>();

    //WHEN
    obj.Value = 123;
    obj.Value = someValue;

    //THEN
    Assert.That(obj.Value, Is.EqualTo(someValue));
  }

  [Test, Parallelizable]
  public void ShouldAllowSettingPropertiesOnInterfaceInstancesWhenOnlySetIsPublic()
  {
    //GIVEN
    var someValue = Any.Integer();
    var obj = Any.Instance<ISettable<int>>();

    //WHEN - THEN
    Assert.DoesNotThrow(() => { obj.Value = someValue; });
  }

  [Test, Parallelizable]
  public void ShouldAllowAccessToValueSetOnPropertiesOnAbstractClassesWhenBothGetAndSetArePublic()
  {
    //GIVEN
    var someValue = Any.Integer();
    var obj = Any.Instance<GetSettable<int>>();

    //WHEN
    obj.Value = 123;
    obj.Value = someValue;

    //THEN
    Assert.That(obj.Value, Is.EqualTo(someValue));
    Assert.That(obj.Value, Is.EqualTo(someValue));
  }

  [Test, Parallelizable]
  public void ShouldAllowSettingPropertiesOnAbstractClassesInstancesWhenOnlySetIsPublic()
  {
    //GIVEN
    var someValue = Any.Integer();
    var obj = Any.Instance<Settable<int>>();

    //WHEN - THEN
    Assert.DoesNotThrow(() => { obj.Value = someValue; });
  }

  [Test, Repeat(100)]
  public void ShouldAllowGeneratingDistinctIntegersWithMaxNumberOfDigits()
  {
    var maxLength = MaxLengthOfInt();
    var value1 = Any.IntegerWithExactDigitsCount(maxLength);
    var value2 = Any.IntegerWithExactDigitsCount(maxLength);

    Assert.That(value1.ToString().Length,
      Is.EqualTo(maxLength),
      value1.ToString());
    Assert.That(value2.ToString().Length,
      Is.EqualTo(maxLength), value2.ToString());
    Assert.That(value2, Is.Not.EqualTo(value1));
  }

  [Test, Repeat(100)]
  public void ShouldAllowGeneratingDistinctUnsignedIntegersWithMaxNumberOfDigits()
  {
    var maxLength = MaxLengthOfUInt();
    var value1 = Any.UnsignedIntegerWithExactDigitsCount(maxLength);
    var value2 = Any.UnsignedIntegerWithExactDigitsCount(maxLength);

    Assert.That(value1.ToString().Length,
      Is.EqualTo(maxLength),
      value1.ToString());
    Assert.That(value2.ToString().Length,
      Is.EqualTo(maxLength), value2.ToString());
    Assert.That(value2, Is.Not.EqualTo(value1));
  }

  [Test, Repeat(100)]
  public void ShouldAllowGeneratingDistinctLongWithMaxNumberOfDigits()
  {
    var maxLength = MaxLengthOfLong();
    var value1 = Any.LongIntegerWithExactDigitsCount(maxLength);
    var value2 = Any.LongIntegerWithExactDigitsCount(maxLength);

    Assert.That(value1.ToString().Length,
      Is.EqualTo(maxLength),
      value1.ToString());
    Assert.That(value2.ToString().Length,
      Is.EqualTo(maxLength), value2.ToString());
    Assert.That(value2, Is.Not.EqualTo(value1));
  }

  [Test, Repeat(100)]
  public void ShouldAllowGeneratingDistinctUnsignedLongWithMaxNumberOfDigits()
  {
    var maxLength = MaxLengthOfULong();
    var value1 = Any.UnsignedLongIntegerWithExactDigitsCount(maxLength);
    var value2 = Any.UnsignedLongIntegerWithExactDigitsCount(maxLength);

    Assert.That(value1.ToString().Length,
      Is.EqualTo(maxLength),
      value1.ToString());
    Assert.That(value2.ToString().Length,
      Is.EqualTo(maxLength), value2.ToString());
    Assert.That(value2, Is.Not.EqualTo(value1));
  }

  [Test, Repeat(100)]
  public void ShouldAllowGeneratingDistinctIntegersWithExactNumberOfDigits()
  {
    var length = MaxLengthOfInt() - 1;
    var value1 = Any.IntegerWithExactDigitsCount(length);
    var value2 = Any.IntegerWithExactDigitsCount(length);

    Assert.That(value1.ToString().Length, Is.EqualTo(length), value1.ToString());
    Assert.That(value2.ToString().Length, Is.EqualTo(length), value2.ToString());
    Assert.That(value2, Is.Not.EqualTo(value1));
  }

  [Test, Repeat(100)]
  public void ShouldAllowGeneratingDistinctUnsignedIntegersWithExactNumberOfDigits()
  {
    var length = MaxLengthOfUInt() - 1;
    var value1 = Any.UnsignedIntegerWithExactDigitsCount(length);
    var value2 = Any.UnsignedIntegerWithExactDigitsCount(length);

    Assert.That(value1.ToString().Length,
      Is.EqualTo(length),
      value1.ToString());
    Assert.That(value2.ToString().Length,
      Is.EqualTo(length), value2.ToString());
    Assert.That(value2, Is.Not.EqualTo(value1));
  }

  [Test, Repeat(100)]
  public void ShouldAllowGeneratingDistinctLongWithExactNumberOfDigits()
  {
    var length = MaxLengthOfLong() - 1;
    var value1 = Any.LongIntegerWithExactDigitsCount(length);
    var value2 = Any.LongIntegerWithExactDigitsCount(length);

    Assert.That(value1.ToString().Length,
      Is.EqualTo(length),
      value1.ToString());
    Assert.That(value2.ToString().Length,
      Is.EqualTo(length), value2.ToString());
    Assert.That(value2, Is.Not.EqualTo(value1));
  }

  [Test, Repeat(100)]
  public void ShouldAllowGeneratingDistinctUnsignedLongWithExactNumberOfDigits()
  {
    var length = MaxLengthOfULong() - 1;
    var value1 = Any.UnsignedLongIntegerWithExactDigitsCount(length);
    var value2 = Any.UnsignedLongIntegerWithExactDigitsCount(length);

    Assert.That(value1.ToString().Length,
      Is.EqualTo(length),
      value1.ToString());
    Assert.That(value2.ToString().Length,
      Is.EqualTo(length), value2.ToString());
    Assert.That(value2, Is.Not.EqualTo(value1));
  }

  [Test, Parallelizable]
  public void ShouldThrowArgumentOutOfRangeExceptionWhenGeneratingIntegersWithExactNumberOfDigitsOverflows()
  {
    Assert.Throws<GenerationFailedException>(() => Any.IntegerWithExactDigitsCount(MaxLengthOfInt() + 1));
    Assert.Throws<GenerationFailedException>(() => Any.LongIntegerWithExactDigitsCount(MaxLengthOfLong() + 1));
    Assert.Throws<GenerationFailedException>(() => Any.UnsignedIntegerWithExactDigitsCount(MaxLengthOfUInt() + 1));
    Assert.Throws<GenerationFailedException>(
      () => Any.UnsignedLongIntegerWithExactDigitsCount(MaxLengthOfULong() + 1));
  }

  [TestCase(10)]
  [TestCase(1)]
  [Repeat(100)]
  public void ShouldAllowGeneratingNumericStringOfArbitraryLength(int length)
  {
    var value1 = Any.NumericString(length);

    AssertStringIsNumeric(value1, length);
  }

  [Test, Repeat(100)]
  public void ShouldAllowGeneratingNumericStringOfLength1()
  {
    var value1 = Any.NumericString(1);

    AssertStringIsNumeric(value1, 1);
  }

  [Test, Parallelizable]
  public void ShouldHandleCopyConstructorsSomehow()
  {
    var o = Any.Instance<ObjectWithCopyConstructor>();
    Assert.That(o._field, Is.Null);
  }

  [Test, Parallelizable]
  public void ShouldTryToUsePublicStaticNonRecursiveFactoryMethodsOverPublicRecursiveConstructors()
  {
    var o2 = Any.Instance<ComplexObjectWithFactoryMethodAndRecursiveConstructor>();

    Assert.That(o2.ToString(), Is.Not.Null);
    Assert.That(o2.ToString(), Is.Not.Empty);
  }

  [Test, Parallelizable]
  public void ShouldNotUseStaticCreationMethodsWithWordParseInThem()
  {
    var o2 = Any.Instance<ObjectWithStaticParseMethod>();

    Assert.That(o2, Is.Not.Null);
    Assert.That(o2.X, Is.Not.Zero);
  }

  [Test, Parallelizable]
  public void ShouldCreateCultureInfo()
  {
    var c1 = Any.Instance<CultureInfo>();
    var c2 = Any.Instance<CultureInfo>();

    Assert.That(c1, Is.Not.Null);
    Assert.That(c2, Is.Not.Null);
    Assert.That(c2, Is.Not.EqualTo(c1));
  }

  [Test, Repeat(10)]
  public void ShouldCreateSerializableInstances()
  {
    SerializeAnyInstanceOf<AbstractObjectWithInterfaceInConstructor>();
    SerializeAnyInstanceOf<AbstractObjectWithVirtualMethods>();
    SerializeAnyInstanceOf<ObjectWithCopyConstructor>();
    SerializeAnyInstanceOf<ComplexObjectWithFactoryMethodAndRecursiveConstructor>();
    SerializeAnyInstanceOf<RecursiveInterface>();

    var x1 = Any.Instance<AbstractObjectWithInterfaceInConstructor>();
    var x2 = Any.Instance<AbstractObjectWithVirtualMethods>();
    var x3 = Any.Instance<RecursiveInterface>();
    var x4 = Any.Instance<ObjectWithCopyConstructor>();
    var x5 = Any.Instance<ComplexObjectWithFactoryMethodAndRecursiveConstructor>();
    CallSomeMethodsOn(x1, x2, x3);
    Serialize(x1);
    Serialize(x2);
    Serialize(x3);
    Serialize(x4);
    Serialize(x5);
  }

  [Test, Parallelizable]
  public void ShouldGenerateVoidStartedTasks()
  {
    //WHEN
    var voidTask1 = Any.StartedTask();
    var voidTask2 = Any.StartedTask();
    //THEN
    Assert.That(voidTask1, Is.Not.Null);
    Assert.That(voidTask2, Is.Not.Null);
    Assert.That(voidTask2, Is.Not.EqualTo(voidTask1));
    Assert.Throws<InvalidOperationException>(voidTask1.Start);
    Assert.Throws<InvalidOperationException>(voidTask2.Start);
  }

  //bug uncomment this to test multithreading
  //[Test, Repeat(100), Timeout(0)]
  //public async Task Lolokimono()
  //{
  //  await Task.WhenAll(
  //    Task.Run(NewMethod), 
  //    Task.Run(NewMethod), 
  //    Task.Run(NewMethod), 
  //    Task.Run(NewMethod), 
  //    Task.Run(NewMethod), 
  //    Task.Run(NewMethod));
  //}

  private static void NewMethod()
  {
    for (int i = 0; i < 1000; ++i)
    {
      Any.Instance<ComplexObjectWithFactoryMethodAndRecursiveConstructor>();
      Any.Instance<AbstractObjectWithInterfaceInConstructor>();
      Any.Instance<AbstractObjectWithVirtualMethods>();
      Any.Instance<AreaEntity>();
      Any.Instance<ConcreteDataStructure>();
      Any.Instance<ConcreteDataStructure2>();
      Any.Instance<Feature>();
      Any.Instance<FileExtension>();
      Any.Instance<FileName>();
      Any.Instance<FileNameWithoutExtension>();
      Any.Instance<GetSettable<int>>();
      Any.Instance<IGeometry>();
      Any.Instance<IncrementalType>();
      Any.Instance<IObjectWithAsyncMethod>();
      Any.Instance<IObservableConcurrentDictionary<int, int>>();
      Any.Instance<MyComplexObject>();
      Any.Instance<ISimple>();
      Any.Instance<MyOwnCollection<string>>();
      Any.Instance<MyService>();
    }
  }

  [Test, Parallelizable]
  public void ShouldGenerateStartedTasks()
  {
    //WHEN
    var task1 = Any.StartedTask<string>();
    var task2 = Any.StartedTask<string>();
    //THEN
    Assert.That(task1, Is.Not.Null);
    Assert.That(task2, Is.Not.Null);
    Assert.That(task2, Is.Not.EqualTo(task1));
    Assert.Throws<InvalidOperationException>(() => task1.Start());
    Assert.Throws<InvalidOperationException>(() => task2.Start());
    Assert.That(task1.Result, Is.Not.Null);
    Assert.That(task2.Result, Is.Not.Null);
  }

  [Test, Parallelizable]
  public void ShouldGenerateCanceledCancellationTokens()
  {
    //WHEN
    var token = Any.CancellationToken();

    //THEN
    Assert.That(token.IsCancellationRequested, Is.True);
  }

  [Test, Parallelizable]
  public void ShouldAllowGeneratingReadOnlyLists()
  {
    //GIVEN
    var readOnlyList = Any.ReadOnlyList<int>();
    //WHEN

    //THEN
    Assert.That(readOnlyList, Is.Not.Null);
    Assert.That(readOnlyList.Count, Is.EqualTo(3));
    Assert.That(readOnlyList, Is.All.Not.Null);
    Assert.That(readOnlyList, Is.Unique);
  }

  [Test, Parallelizable]
  public void ShouldAllowGeneratingReadOnlyListsThroughGenericMethod()
  {
    //GIVEN
    var readOnlyList = Any.Instance<IReadOnlyList<int>>();
    //WHEN

    //THEN
    Assert.That(readOnlyList, Is.Not.Null);
    Assert.That(readOnlyList.Count, Is.EqualTo(3));
    Assert.That(readOnlyList, Is.All.Not.Null);
    Assert.That(readOnlyList, Is.Unique);
  }

  [Test, Parallelizable]
  public void ShouldAllowGeneratingReadOnlyDictionariesThroughGenericMethod()
  {
    //GIVEN
    var readonlyDictionary = Any.Instance<IReadOnlyDictionary<int, int>>();
    //WHEN

    //THEN
    Assert.That(readonlyDictionary, Is.Not.Null);
    Assert.That(readonlyDictionary.Count, Is.EqualTo(3));
    Assert.That(readonlyDictionary, Is.All.Not.Null);
    Assert.That(readonlyDictionary, Is.Unique);
  }

  [Test, Parallelizable]
  public void ShouldAllowGeneratingReadOnlyListsOfSpecifiedLength()
  {
    //GIVEN
    var length = 5;
    var readOnlyList = Any.ReadOnlyList<int>(length);
    //WHEN

    //THEN
    Assert.That(readOnlyList, Is.Not.Null);
    Assert.That(readOnlyList.Count, Is.EqualTo(length));
    Assert.That(readOnlyList, Is.All.Not.Null);
    Assert.That(readOnlyList, Is.Unique);
  }

  [Test, Parallelizable]
  public void ShouldAllowGeneratingImmutableLists()
  {
    //GIVEN
    var readOnlyList = Any.ImmutableList<int>();
    //WHEN

    //THEN
    Assert.That(readOnlyList, Is.Not.Null);
    Assert.That(readOnlyList.Count, Is.EqualTo(3));
    Assert.That(readOnlyList, Is.All.Not.Null);
    Assert.That(readOnlyList, Is.Unique);
  }

  [Test, Parallelizable]
  public void ShouldAllowGeneratingDummyImmutableLists()
  {
    //GIVEN
    var readOnlyList = Any.Dummy<ImmutableList<int>>();
    //WHEN

    //THEN
    Assert.That(readOnlyList, Is.Not.Null);
    Assert.That(readOnlyList.Count, Is.Zero);
  }

  [Test, Parallelizable]
  public void ShouldAllowGeneratingObjectsWithImmutableLists()
  {
    //GIVEN
    var obj = Any.Instance<ObjectWithImmutableList>();
    //WHEN

    //THEN
    Assert.That(obj, Is.Not.Null);
    Assert.That(obj.Elements.Count, Is.EqualTo(3));
    Assert.That(obj.Elements, Is.All.Not.Null);
    Assert.That(obj.Elements, Is.Unique);
  }

  [Test, Parallelizable]
  public void ShouldAllowGeneratingImmutableListsThroughGenericMethod()
  {
    //GIVEN
    var readOnlyList = Any.Instance<ImmutableList<int>>();
    //WHEN

    //THEN
    Assert.That(readOnlyList, Is.Not.Null);
    Assert.That(readOnlyList.Count, Is.EqualTo(3));
    Assert.That(readOnlyList, Is.All.Not.Null);
    Assert.That(readOnlyList, Is.Unique);
  }

  [Test, Parallelizable]
  public void ShouldAllowGeneratingImmutableArrays()
  {
    //GIVEN
    var collection = Any.ImmutableArray<int>();
    //WHEN

    //THEN
    Assert.That(collection, Is.Not.Default);
    Assert.That(collection.Length, Is.EqualTo(3));
    Assert.That(collection, Is.All.Not.Null);
    Assert.That(collection, Is.Unique);
  }

  [Test, Parallelizable]
  public void ShouldAllowGeneratingDummyImmutableArrays()
  {
    //GIVEN
    var immutableArray = Any.Dummy<ImmutableArray<int>>();
    //WHEN

    //THEN
    Assert.That(immutableArray, Is.Not.Default);
    Assert.That(immutableArray.Length, Is.Zero);
  }

  [Test, Parallelizable, Ignore("diminishing disabled for now for backwards compatibility reasons")]
  public void ShouldAllowGeneratingDummyImmutableArraysInTheAnyLoop()
  {
    //GIVEN
    var collection = Any.Instance<
      ImmutableArray<
        ImmutableArray<
          ImmutableArray<
            ImmutableArray<
              ImmutableArray<
                ImmutableArray<int>>>>>>>();
    //WHEN

    //THEN
    Assert.That(collection, Is.Not.Default);
    collection.First().First().First().First().First().Length.Should().Be(1);
  }

  [Test, Parallelizable]
  public void ShouldAllowGeneratingImmutableArraysThroughGenericMethod()
  {
    //GIVEN
    var readOnlyList = Any.Instance<ImmutableList<int>>();
    //WHEN

    //THEN
    Assert.That(readOnlyList, Is.Not.Null);
    Assert.That(readOnlyList.Count, Is.EqualTo(3));
    Assert.That(readOnlyList, Is.All.Not.Null);
    Assert.That(readOnlyList, Is.Unique);
  }

  [Test, Parallelizable]
  public void ShouldAllowGeneratingImmutableDictionaries()
  {
    //GIVEN
    var readOnlyList = Any.ImmutableDictionary<int, int>();
    //WHEN

    //THEN
    Assert.That(readOnlyList, Is.Not.Null);
    Assert.That(readOnlyList.Count, Is.EqualTo(3));
    Assert.That(readOnlyList, Is.All.Not.Null);
    Assert.That(readOnlyList, Is.Unique);
  }

  [Test, Parallelizable]
  public void ShouldAllowGeneratingDummyImmutableDictionaries()
  {
    //GIVEN
    var readOnlyList = Any.Dummy<ImmutableDictionary<int, int>>();
    //WHEN

    //THEN
    Assert.That(readOnlyList, Is.Not.Null);
    Assert.That(readOnlyList.Count, Is.Zero);
  }

  [Test, Parallelizable]
  public void ShouldAllowGeneratingImmutableDictionariesThroughGenericMethod()
  {
    //GIVEN
    var collection = Any.Instance<ImmutableDictionary<int, int>>();
    //WHEN

    //THEN
    Assert.That(collection, Is.Not.Null);
    Assert.That(collection.Count, Is.EqualTo(3));
    Assert.That(collection, Is.All.Not.Null);
    Assert.That(collection, Is.Unique);
  }
    
  [Test, Parallelizable]
  public void ShouldAllowGeneratingImmutableHashSets()
  {
    //GIVEN
    var collection = Any.ImmutableHashSet<int>();
    //WHEN

    //THEN
    Assert.That(collection, Is.Not.Null);
    Assert.That(collection.Count, Is.EqualTo(3));
    Assert.That(collection, Is.All.Not.Null);
    Assert.That(collection, Is.Unique);
  }

  [Test, Parallelizable]
  public void ShouldAllowGeneratingDummyImmutableHashSets()
  {
    //GIVEN
    var collection = Any.Dummy<ImmutableHashSet<int>>();
    //WHEN

    //THEN
    Assert.That(collection, Is.Not.Null);
    Assert.That(collection.Count, Is.Zero);
  }

  [Test, Parallelizable]
  public void ShouldAllowGeneratingImmutableHashSetsThroughGenericMethod()
  {
    //GIVEN
    var collection = Any.Instance<ImmutableHashSet<int>>();
    //WHEN

    //THEN
    Assert.That(collection, Is.Not.Null);
    Assert.That(collection.Count, Is.EqualTo(3));
    Assert.That(collection, Is.All.Not.Null);
    Assert.That(collection, Is.Unique);
  }
    
  [Test, Parallelizable]
  public void ShouldAllowGeneratingImmutableQueues()
  {
    //GIVEN
    var collection = Any.ImmutableQueue<int>();
    //WHEN

    //THEN
    Assert.That(collection, Is.Not.Null);
    Assert.That(collection.Count(), Is.EqualTo(3));
    Assert.That(collection, Is.All.Not.Null);
    Assert.That(collection, Is.Unique);
  }

  [Test, Parallelizable]
  public void ShouldAllowGeneratingDummyImmutableQueues()
  {
    //GIVEN
    var collection = Any.Dummy<ImmutableQueue<int>>();
    //WHEN

    //THEN
    Assert.That(collection, Is.Not.Null);
    Assert.That(collection.Count(), Is.Zero);
  }

  [Test, Parallelizable]
  public void ShouldAllowGeneratingImmutableQueuesThroughGenericMethod()
  {
    //GIVEN
    var collection = Any.Instance<ImmutableHashSet<int>>();
    //WHEN

    //THEN
    Assert.That(collection, Is.Not.Null);
    Assert.That(collection.Count, Is.EqualTo(3));
    Assert.That(collection, Is.All.Not.Null);
    Assert.That(collection, Is.Unique);
  }

  [Test, Parallelizable]
  public void ShouldAllowGeneratingImmutableSortedSets()
  {
    //GIVEN
    var collection = Any.ImmutableSortedSet<int>();
    //WHEN

    //THEN
    Assert.That(collection, Is.Not.Null);
    Assert.That(collection.Count, Is.EqualTo(3));
    Assert.That(collection, Is.All.Not.Null);
    Assert.That(collection, Is.Unique);
  }

  [Test, Parallelizable]
  public void ShouldAllowGeneratingDummyImmutableSortedSets()
  {
    //GIVEN
    var collection = Any.Dummy<ImmutableSortedSet<int>>();
    //WHEN

    //THEN
    Assert.That(collection, Is.Not.Null);
    Assert.That(collection.Count, Is.Zero);
  }

  [Test, Parallelizable]
  public void ShouldAllowGeneratingImmutableSortedSetsThroughGenericMethod()
  {
    //GIVEN
    var collection = Any.Instance<ImmutableSortedSet<int>>();
    //WHEN

    //THEN
    Assert.That(collection, Is.Not.Null);
    Assert.That(collection.Count, Is.EqualTo(3));
    Assert.That(collection, Is.All.Not.Null);
    Assert.That(collection, Is.Unique);
  }

  [Test, Parallelizable]
  public void ShouldAllowGeneratingImmutableSortedDictionaries()
  {
    //GIVEN
    var collection = Any.ImmutableSortedDictionary<int, int>();
    //WHEN

    //THEN
    Assert.That(collection, Is.Not.Null);
    Assert.That(collection.Count, Is.EqualTo(3));
    Assert.That(collection, Is.All.Not.Null);
    Assert.That(collection, Is.Unique);
  }

  [Test, Parallelizable]
  public void ShouldAllowGeneratingDummyImmutableSortedDictionaries()
  {
    //GIVEN
    var collection = Any.Dummy<ImmutableSortedDictionary<int, int>>();
    //WHEN

    //THEN
    Assert.That(collection, Is.Not.Null);
    Assert.That(collection.Count, Is.Zero);
  }

  [Test, Parallelizable]
  public void ShouldAllowGeneratingImmutableSortedDictionariesThroughGenericMethod()
  {
    //GIVEN
    var collection = Any.Instance<ImmutableSortedDictionary<int, int>>();
    //WHEN

    //THEN
    Assert.That(collection, Is.Not.Null);
    Assert.That(collection.Count, Is.EqualTo(3));
    Assert.That(collection, Is.All.Not.Null);
    Assert.That(collection, Is.Unique);
  }

  [Test, Parallelizable]
  public void ShouldAllowGeneratingImmutableStacks()
  {
    //GIVEN
    var collection = Any.ImmutableStack<int>();
    //WHEN

    //THEN
    Assert.That(collection, Is.Not.Null);
    Assert.That(collection.Count(), Is.EqualTo(3));
    Assert.That(collection, Is.All.Not.Null);
    Assert.That(collection, Is.Unique);
  }

  [Test, Parallelizable]
  public void ShouldAllowGeneratingDummyImmutableStacks()
  {
    //GIVEN
    var collection = Any.Dummy<ImmutableStack<int>>();
    //WHEN

    //THEN
    Assert.That(collection, Is.Not.Null);
    Assert.That(collection.Count(), Is.Zero);
  }

  [Test, Parallelizable]
  public void ShouldAllowGeneratingImmutableStacksThroughGenericMethod()
  {
    //GIVEN
    var collection = Any.Instance<ImmutableStack<int>>();
    //WHEN

    //THEN
    Assert.That(collection, Is.Not.Null);
    Assert.That(collection.Count(), Is.EqualTo(3));
    Assert.That(collection, Is.All.Not.Null);
    Assert.That(collection, Is.Unique);
  }


  [Test, Parallelizable]
  public void ShouldAllowGeneratingFuncs()
  {
    //GIVEN
    var func1 = Any.Func<int>();
    var func2 = Any.Func<int, int>();
    var func3 = Any.Func<int, int, string>();
    var func4 = Any.Instance<Func<CancellationToken, Task<TestTemplateClass>>>();

    //WHEN
    var result11 = func1();
    var result12 = func1();
    var result21 = func2(1);
    var result22 = func2(1);
    var result31 = func3(1, 3);
    var result32 = func3(1, 3);
    var result41 = func4.Invoke(CancellationToken.None);
    var result42 = func4.Invoke(CancellationToken.None);

    //THEN
    result31.Should().NotBeNullOrEmpty();
    result32.Should().NotBeNullOrEmpty();
    result41.Should().NotBeNull();
    result42.Should().NotBeNull();
    result11.Should().Be(result12);
    result21.Should().Be(result22);
    result31.Should().Be(result32);
    result41.Should().Be(result42);
  }

  [Test, Parallelizable]
  public void ShouldAllowGeneratingLongerFuncs()
  {
    //GIVEN
    var func4 = Any.Func<int, int, int, string>();
    var func5 = Any.Func<int, int, int, int, string>();
    var func6 = Any.Func<int, int, int, int, int, string>();

    //WHEN
    var result41 = func4(1, 2, 3);
    var result42 = func4(1, 2, 3);
    var result51 = func5(1, 2, 3, 4);
    var result52 = func5(1, 2, 3, 4);
    var result61 = func6(1, 2, 3, 4, 5);
    var result62 = func6(1, 2, 3, 4, 5);

    //THEN
    result41.Should().NotBeNullOrEmpty();
    result42.Should().NotBeNullOrEmpty();
    result51.Should().NotBeNullOrEmpty();
    result52.Should().NotBeNullOrEmpty();
    result61.Should().NotBeNullOrEmpty();
    result62.Should().NotBeNullOrEmpty();
    result41.Should().Be(result42);
    result51.Should().Be(result52);
    result61.Should().Be(result62);
  }

  [Test, Parallelizable]
  public void ShouldBeAbleToCreateJsonSerializerSettingsFromNewtonsoftJson()
  {
    //WHEN
    var serializerSettings = Any.Instance<JsonSerializerSettings>();

    //THEN
    Assert.That(serializerSettings, Is.Not.Null);
  }

  [Test, Parallelizable]
  public void ShouldAllowGeneratingActions()
  {
    //GIVEN
    var func = Any.Action<int, int, string>();

    //WHEN-THEN
    Assert.DoesNotThrow(() => func(1, 2, "2"));
  }

  [Test, Parallelizable]
  public void ShouldAllowGeneratingLongerActions()
  {
    //GIVEN
    var action0 = Any.Action();
    var action1 = Any.Action<int>();
    var action2 = Any.Action<int, int>();
    var action4 = Any.Action<int, int, int, int>();
    var action5 = Any.Action<int, int, int, int, int>();
    var action6 = Any.Action<int, int, int, int, int, int>();

    //WHEN-THEN
    Assert.DoesNotThrow(() => action0());
    Assert.DoesNotThrow(() => action1(1));
    Assert.DoesNotThrow(() => action2(1, 2));
    Assert.DoesNotThrow(() => action4(1, 2, 3, 4));
    Assert.DoesNotThrow(() => action5(1, 2, 3, 4, 5));
    Assert.DoesNotThrow(() => action6(1, 2, 3, 4, 5, 6));
  }

  [Test, Parallelizable]
  public void ShouldAllowGeneratingComplexCollectionAndInterfaceSubclasses()
  {
    //GIVEN
    var o = Any.Instance<ComplexCollectionAndInterfaceSubclass>();

    //WHEN-THEN
    o.Should().NotBeNull();
    o.Keys.Should().NotBeEmpty();
    o.Values.Should().NotBeEmpty();
  }

  [Test, Parallelizable]
  public void ShouldAllowGeneratingJObjects()
  {
    var constructor = Any.Instance<JConstructor>();
    var array = Any.Instance<JArray>();
    var jObject = Any.Instance<JObject>();
    var token = Any.Instance<JToken>();
    var container = Any.Instance<JContainer>();
    var property = Any.Instance<JProperty>();

    constructor.Should().NotBeNull();
    array.Should().NotBeNull();
    jObject.Should().NotBeNull();
    token.Should().NotBeNull();
    container.Should().NotBeNull();
    property.Should().NotBeNull();
  }

  private static void CallSomeMethodsOn(AbstractObjectWithInterfaceInConstructor x1,
    AbstractObjectWithVirtualMethods x2,
    RecursiveInterface x3)
  {
    // ReSharper disable once UnusedVariable
    _ = new object[] {x1.AbstractInt, x2.GetSomething().OrThrow(), x3.NestedAsDictionary, x2.GetSomething2(), x3.Nested};
  }

  private static void SerializeAnyInstanceOf<T>()
  {
    Serialize(Any.Instance<T>());
  }

  private static void Serialize<T>(T instance)
  {
    var jsonString = System.Text.Json.JsonSerializer.Serialize(instance);
    using var stream = new MemoryStream();
    var writer = new StreamWriter(stream);
    writer.Write(jsonString);
    writer.Flush();
  }

  private static void AssertStringIsNumeric(string theString, int expectedLength)
  {
    Assert.That(theString.Length, Is.EqualTo(expectedLength));
    foreach (var character in theString)
    {
      Assert.That(char.IsDigit(character), Is.True, $"Expected digit, got {character}");
    }

    Assert.That(theString[0], Is.Not.EqualTo('0'));
  }

  private static int MaxLengthOfInt()
  {
    return int.MaxValue.ToString().Length;
  }

  private static int MaxLengthOfUInt()
  {
    return uint.MaxValue.ToString().Length;
  }

  private static int MaxLengthOfLong()
  {
    return long.MaxValue.ToString().Length;
  }

  private static int MaxLengthOfULong()
  {
    return ulong.MaxValue.ToString().Length;
  }

  private static void Alike<T>(T expected, T actual)
  {
    var comparison = ObjectGraph.Comparison();
    var result = comparison.Compare(expected, actual);
    result.ExceededDifferences.Should().BeFalse(result.DifferencesString);
  }

  private static void NotAlike<T>(T expected, T actual)
  {
    var comparison = ObjectGraph.Comparison();
    var result = comparison.Compare(expected, actual);
    result.ExceededDifferences.Should().BeTrue(result.DifferencesString);
  }

  [GeneratedRegex(@"content/([A-Za-z0-9\-]+)\.aspx$")]
  private static partial Regex ExampleRegex();
}
