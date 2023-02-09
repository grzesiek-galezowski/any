using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using AnySpecification.Fixtures;
using AnySpecification.GraphComparison;
using FluentAssertions;
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

public class AnySpecification
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
    Assert.AreNotEqual(digit1, digit2);
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
    Assert.AreNotEqual(digit1, digit2);
  }

  [Test, Parallelizable]
  public void ShouldGenerateDifferentIpAddressEachTime()
  {
    //GIVEN
    var address1 = Any.Instance<IPAddress>();
    var address2 = Any.Instance<IPAddress>();

    //THEN
    Assert.AreNotEqual(address1, address2);
    Assert.AreNotEqual(address1.ToString(), address2.ToString());
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

    Assert.AreNotEqual(default(int), obj.GetInt());
    Assert.AreNotEqual(string.Empty, obj.GetString());
    Assert.AreNotEqual(string.Empty, obj.GetStringProperty);
    Assert.NotNull(obj.GetString());
    Assert.NotNull(obj.GetStringProperty);
  }

  [Test, Parallelizable]
  public void ShouldBeAbleToProxyMethodsThatReturnInterfaces()
  {
    //GIVEN
    var obj = Any.Instance<ISimple>();

    //WHEN
    obj = obj.GetInterface();

    //THEN
    Assert.NotNull(obj);
    Assert.AreNotEqual(default(int), obj.GetInt());
    Assert.AreNotEqual(string.Empty, obj.GetString());
    Assert.AreNotEqual(string.Empty, obj.GetStringProperty);
    Assert.NotNull(obj.GetString());
    Assert.NotNull(obj.GetStringProperty);
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
    Assert.AreEqual(valueFirstTime, valueSecondTime);
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
    Assert.AreNotEqual(valueFromFirstInstance, valueFromSecondInstance);
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
    Assert.NotNull(Any.Uri());
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
      Assert.NotNull(simple);
    }
  }

  [Test, Parallelizable]
  public void ShouldGenerateMembersReturningTypeOfType()
  {
    //GIVEN
    var obj1 = Any.Instance<ISimple>();
    var obj2 = Any.Instance<ISimple>();

    //THEN
    Assert.Multiple(() =>
    {
      Assert.NotNull(obj1.GetTypeProperty);
      Assert.NotNull(obj2.GetTypeProperty);
      Assert.AreNotEqual(obj1.GetTypeProperty, obj2.GetTypeProperty);
      Assert.AreEqual(obj1.GetTypeProperty, obj1.GetTypeProperty);
      Assert.AreEqual(obj2.GetTypeProperty, obj2.GetTypeProperty);
    });
  }

  [Test, Parallelizable]
  public void ShouldBeAbleToGenerateInstancesOfConcreteClassesWithInterfacesAsTheirConstructorArguments()
  {
    //GIVEN
    var createdProxy = Any.Instance<ObjectWithInterfaceInConstructor>();

    //THEN
    Assert.NotNull(createdProxy._constructorArgument);
    Assert.NotNull(createdProxy._constructorNestedArgument);
  }

  [Test, Parallelizable]
  public void ShouldBeAbleToGenerateInstancesOfAbstractClasses()
  {
    //GIVEN
    var createdProxy = Any.Instance<AbstractObjectWithInterfaceInConstructor>();

    //THEN
    Assert.Multiple(() =>
    {
      Assert.NotNull(createdProxy._constructorArgument);
      Assert.NotNull(createdProxy._constructorNestedArgument);
      Assert.AreNotEqual(default(int), createdProxy.AbstractInt);
      Assert.AreNotEqual(default(int), createdProxy.SettableInt);
    });
  }

  [Test, Parallelizable]
  public void ShouldOverrideVirtualMethodsThatReturnDefaultTypeValuesOnAbstractClassProxy()
  {
    //GIVEN
    var obj = Any.Instance<AbstractObjectWithVirtualMethods>();

    //THEN
    Assert.AreNotEqual(default(string), obj.GetSomething());
    Assert.AreNotEqual("Something", obj.GetSomething2());
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
    Assert.AreNotEqual(default(string), obj.GetSomethingButThrowExceptionWhileGettingIt());
  }

  [Test, Parallelizable]
  public void ShouldNotCreateTheSameMethodInfoTwiceInARow()
  {
    //GIVEN
    var x = Any.Instance<MethodInfo>();
    var y = Any.Instance<MethodInfo>();
    var z = Any.Instance<ObjectWithMethodInfo>();

    //THEN
    Assert.AreNotEqual(x, y);
    Assert.NotNull(z.Method);
    Assert.AreNotEqual(y, z.Method);
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
    CollectionAssert.AllItemsAreUnique(new[] {dof1, dof2, dof3, dof4, dof5, dof6});
    CollectionAssert.DoesNotContain(new[] {dof1, dof2, dof3, dof4, dof5, dof6, dof7}, DayOfWeek.Sunday);
  }

  [Test, Parallelizable]
  public void ShouldDisallowSkippingTheSameValueTwiceWhenGeneratingAnyValueOtherThan()
  {
    Any.Invoking(a => a.OtherThan(2, 2))
      .Should().Throw<Exception>();
  }

  [Test, Timeout(2000)]
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
    CollectionAssert.AllItemsAreUnique(new[] {dof1, dof2, dof3, dof4, dof5, dof6, dof7});
  }

  [Test, Parallelizable]
  public void ShouldGenerateDifferentValueEachTimeAndNotAmongPassedOnesWhenAskedToCreateAnyValueBesidesGiven()
  {
    //WHEN
    var int1 = Any.OtherThan(1, 2, 3, 4, 5, 6, 7, 8, 9, 10);
    var int2 = Any.OtherThan(1, 2, 3, 4, 5, 6, 7, 8, 9, 10);

    //THEN
    Assert.AreNotEqual(int1, int2);
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
    Assert.Multiple(() =>
    {
      Assert.True(Enumerable.Range(1, 3).Contains(int1));
      Assert.True(Enumerable.Range(1, 3).Contains(int2));
      Assert.True(Enumerable.Range(1, 3).Contains(int3));
      Assert.True(Enumerable.Range(1, 3).Contains(int4));
      Assert.AreNotEqual(int1, int2);
      Assert.AreNotEqual(int2, int3);
      Assert.AreNotEqual(int3, int4);

      Assert.True(Enumerable.Range(5, 2).Contains(int5));
      Assert.True(Enumerable.Range(10, 4).Contains(int6));
    });
  }

  [Test, Parallelizable]
  public void ShouldGenerateStringAccordingtoRegex()
  {
    //GIVEN
    const string exampleRegex = @"content/([A-Za-z0-9\-]+)\.aspx$";

    //WHEN
    var result = Any.StringMatching(exampleRegex);

    //THEN
    Assert.True(Regex.IsMatch(result, exampleRegex));
  }

  [TestCase(2)]
  [TestCase(5)]
  [TestCase(12)]
  public void ShouldGenerateStringOfGivenLength(int stringLength)
  {
    //WHEN
    var str = Any.String(stringLength);

    //THEN
    Assert.AreEqual(stringLength, str.Length);
  }

  [Test, Parallelizable]
  public void ShouldCreateSortedSetWithThreeDistinctValues()
  {
    //WHEN
    var set = Any.SortedSet<int>();

    //THEN
    CollectionAssert.IsOrdered(set);
    CollectionAssert.AllItemsAreUnique(set);
    Assert.AreEqual(3, set.Count);
  }

  [Test, Parallelizable]
  public void ShouldBeAbleToGenerateDistinctLettersEachTime()
  {
    //WHEN
    var char1 = Any.AlphaChar();
    var char2 = Any.AlphaChar();
    var char3 = Any.AlphaChar();

    //THEN
    Assert.Multiple(() =>
    {
      Assert.AreNotEqual(char1, char2);
      Assert.AreNotEqual(char2, char3);
      Assert.True(char.IsLetter(char1));
      Assert.True(char.IsLetter(char2));
      Assert.True(char.IsLetter(char3));
    });
  }

  [Test, Parallelizable]
  public void ShouldBeAbleToGenerateDistinctDigitsEachTime()
  {
    //WHEN
    var char1 = Any.DigitChar();
    var char2 = Any.DigitChar();
    var char3 = Any.DigitChar();

    //THEN
    Assert.Multiple(() =>
    {
      Assert.AreNotEqual(char1, char2);
      Assert.AreNotEqual(char2, char3);
      Assert.True(char.IsDigit(char1));
      Assert.True(char.IsDigit(char2));
      Assert.True(char.IsDigit(char3));
    });
  }

  [Test, Timeout(1000)]
  public void ShouldHandleEmptyExcludedStringsWhenGeneratingAnyStringNotContainingGiven()
  {
    Assert.DoesNotThrow(() => Any.StringNotContaining(string.Empty));
  }

  [Test, Repeat(100)]
  public void ShouldGenerateStringsWithoutASpecificSubstring() =>
    Any.StringNotContaining("0").Should().NotContain("0");

  [Test, Repeat(100)]
  public void ShouldGenerateSeededString()
  {
    Any.String("0").Should().StartWith("0");
    Any.String("0").Length.Should().BeGreaterThan(1);
    Any.String("0").Should().NotBe(Any.String("0"));
  }


  [Test, Parallelizable]
  public void ShouldBeAbleToGenerateBothPrimitiveTypeInstanceAndInterfaceUsingNewInstanceMethod()
  {
    var primitive = Any.Instance<int>();
    var interfaceImplementation = Any.Instance<ISimple>();

    Assert.Multiple(() =>
    {
      Assert.NotNull(interfaceImplementation);
      Assert.AreNotEqual(default(int), primitive);
    });
  }

  [Test, Parallelizable]
  public void ShouldSupportRecursiveInterfacesWithLists()
  {
    var recursiveObjects = Any.Enumerable<RecursiveInterface>().ToList();

    var x2 = recursiveObjects[0].GetNested()[0].Nested.Number;

    Assert.AreNotEqual(default(int), x2);
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

    Assert.Multiple(() =>
    {
      Assert.AreEqual(3, y.Count);
      Assert.NotNull(y1);
      Assert.NotNull(y2);
    });
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

    Assert.Multiple(() =>
    {
      Assert.AreEqual(anyCount, list.Count);
      Assert.AreEqual(anyCount, enumerable.Count());
      Assert.AreEqual(anyCount, array.Length);
      Assert.AreEqual(anyCount, set.Count);
      Assert.AreEqual(anyCount, dictionary.Count);
      Assert.AreEqual(anyCount, sortedList.Count);
      Assert.AreEqual(anyCount, sortedDictionary.Count);
      Assert.AreEqual(anyCount, concurrentDictionary.Count);
      Assert.AreEqual(anyCount, sortedEnumerable.Count());
      Assert.AreEqual(anyCount, concurrentBag.Count);
      Assert.AreEqual(anyCount, concurrentStack.Count);
      Assert.AreEqual(anyCount, concurrentQueue.Count);
    });
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

    Assert.Multiple(() =>
    {
      Assert.AreEqual(anyCount, list.Count);
      Assert.AreEqual(anyCount, enumerable.Count());
      Assert.AreEqual(anyCount, array.Length);
      Assert.AreEqual(anyCount, set.Count);
      Assert.AreEqual(anyCount, dictionary.Count);
      Assert.AreEqual(anyCount, sortedList.Count);
      Assert.AreEqual(anyCount, sortedDictionary.Count);
      Assert.AreEqual(anyCount, sortedEnumerable.Count());
      Assert.AreEqual(anyCount, concurrentDictionary.Count);
      Assert.AreEqual(anyCount, concurrentBag.Count);
      Assert.AreEqual(anyCount, concurrentStack.Count);
      Assert.AreEqual(anyCount, concurrentQueue.Count);
    });
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

    Assert.Multiple(() =>
    {
      Assert.AreEqual(anyCount, list.Count);
      Assert.AreEqual(anyCount, enumerable.Count());
      Assert.AreEqual(anyCount, array.Length);
      Assert.AreEqual(anyCount, set.Count);
      Assert.AreEqual(anyCount, dictionary.Count);
      Assert.AreEqual(anyCount, sortedList.Count);
      Assert.AreEqual(anyCount, sortedDictionary.Count);
      Assert.AreEqual(anyCount, concurrentDictionary.Count);
      Assert.AreEqual(anyCount, concurrentStack.Count);
      Assert.AreEqual(anyCount, concurrentBag.Count);
      Assert.AreEqual(anyCount, concurrentQueue.Count);
    });
  }

  [Test, Parallelizable]
  public void ShouldAllowCreatingCustomCollectionInstances()
  {
    var customCollection = Any.Instance<MyOwnCollection<RecursiveInterface>>();

    Assert.AreEqual(3, customCollection.Count);
    foreach (var recursiveInterface in customCollection)
    {
      Assert.NotNull(recursiveInterface);
    }
  }

  [Test, Parallelizable]
  public void ShouldAllowCreatingCustomProducedConsumerCollectionInstances()
  {
    var customCollection = Any.Instance<MyOwnPcCollection<RecursiveInterface>>();

    Assert.AreEqual(3, customCollection.Count);
    foreach (var recursiveInterface in customCollection)
    {
      Assert.NotNull(recursiveInterface);
    }
  }

  [Test, Parallelizable]
  public void ShouldSupportCreatingArraysWithSpecificLiteralElements()
  {
    var array = Any.ArrayWith(1, 2, 3);

    CollectionAssert.Contains(array, 1);
    CollectionAssert.Contains(array, 2);
    CollectionAssert.Contains(array, 3);
    Assert.GreaterOrEqual(array.Length, 3);
  }

  [Test, Parallelizable]
  public void ShouldSupportCreatingListsWithSpecificLiteralElements()
  {
    var list = Any.ListWith(1, 2, 3);

    CollectionAssert.Contains(list, 1);
    CollectionAssert.Contains(list, 2);
    CollectionAssert.Contains(list, 3);
    Assert.GreaterOrEqual(list.Count, 3);
  }

  [Test, Parallelizable]
  public void ShouldSupportCreatingListsWithSpecificEnumerableOfElements()
  {
    var array = Any.ListWith<int>(new List<int> {1, 2, 3});

    CollectionAssert.Contains(array, 1);
    CollectionAssert.Contains(array, 2);
    CollectionAssert.Contains(array, 3);
    Assert.GreaterOrEqual(array.Count, 3);
  }

  [Test, Parallelizable]
  public void ShouldSupportCreatingArraysWithSpecificEnumerableOfElements()
  {
    var array = Any.ArrayWith<int>(new List<int> {1, 2, 3});

    CollectionAssert.Contains(array, 1);
    CollectionAssert.Contains(array, 2);
    CollectionAssert.Contains(array, 3);
    Assert.GreaterOrEqual(array.Length, 3);
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
    Assert.Multiple(() =>
    {
      Assert.NotNull(kvp.Key);
      Assert.NotNull(kvp.Value);
    });
  }

  [Test, Parallelizable]
  public void ShouldSupportActions()
  {
    //WHEN
    var action = Any.Instance<Action<ISimple, string>>();

    //THEN
    Assert.NotNull(action);
  }

  [Test, Parallelizable]
  public void ShouldAllowCreatingDifferentMaybesOfConcreteClasses()
  {
    var maybeString1 = Any.Instance<Maybe<string>>();
    var maybeString2 = Any.Instance<Maybe<string>>();

    Assert.AreNotEqual(maybeString1, maybeString2);
    maybeString1.Should().NotBe(Maybe<string>.Nothing);
    maybeString2.Should().NotBe(Maybe<string>.Nothing);
  }

  [Test, Parallelizable]
  public void ShouldAllowCreatingMaybesAsPartOfOtherClasses()
  {
    var maybeObject1 = Any.Instance<ObjectWithMaybe>();
    var maybeObject2 = Any.Instance<ObjectWithMaybe>();

    Assert.AreNotEqual(maybeObject1.Property, maybeObject2.Property);
    Assert.AreNotEqual(maybeObject1._field, maybeObject2._field);
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

    Assert.AreNotEqual(maybeImplementation1, maybeImplementation2);
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
    Assert.AreNotEqual(element2, element1);
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
    Assert.AreNotEqual(element2, element1);
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
    Assert.AreNotEqual(element2, element1);
  }

  [Test, Parallelizable]
  public void ShouldAllowGeneratingIntegersFromSequence()
  {
    var value1 = Any.IntegerFromSequence(startingValue: 12, step: 112);
    var value2 = Any.IntegerFromSequence(startingValue: 12, step: 112);

    Assert.AreEqual(value1, value2 - 112);
    Assert.Greater(value1, 12);
  }

  [Test, Parallelizable]
  public void ShouldAllowGeneratingDivisibleIntegers()
  {
    var value1 = Any.IntegerDivisibleBy(5);
    var value2 = Any.IntegerDivisibleBy(5);

    Assert.AreNotEqual(value1, value2);
    Assert.AreEqual(0, value1 % 5);
    Assert.AreEqual(0, value2 % 5);
  }

  [Test, Parallelizable]
  public void ShouldAllowGeneratingNotDivisibleIntegers()
  {
    var value1 = Any.IntegerNotDivisibleBy(5);
    var value2 = Any.IntegerNotDivisibleBy(5);

    Assert.AreNotEqual(value1, value2);
    Assert.AreNotEqual(0, value1 % 5);
    Assert.AreNotEqual(0, value2 % 5);
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
    Assert.Multiple(() =>
    {
      Assert.Throws<GenerationFailedException>(() => Any.Instance<ThrowingInConstructor>());
      Assert.NotNull(Any.Dummy<ThrowingInConstructor>());
      Assert.NotNull(Any.Dummy<string>());
      Assert.NotNull(Any.Dummy<int>());
    });
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
    anyConcrete.Inner.InnerDummyInt.Should().Be(123);
    anyConcrete.Inner.InnerDummyString.Should().Be("InnerCustomString");
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
    Assert.NotNull(entity.Feature);
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
    Assert.AreEqual(someValue, obj.Value);
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
    Assert.AreEqual(someValue, obj.Value);
    Assert.AreEqual(someValue, obj.Value);
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

    Assert.AreEqual(maxLength,
      value1.ToString().Length,
      value1.ToString());
    Assert.AreEqual(maxLength,
      value2.ToString().Length, value2.ToString());
    Assert.AreNotEqual(value1, value2);
  }

  [Test, Repeat(100)]
  public void ShouldAllowGeneratingDistinctUnsignedIntegersWithMaxNumberOfDigits()
  {
    var maxLength = MaxLengthOfUInt();
    var value1 = Any.UnsignedIntegerWithExactDigitsCount(maxLength);
    var value2 = Any.UnsignedIntegerWithExactDigitsCount(maxLength);

    Assert.AreEqual(maxLength,
      value1.ToString().Length,
      value1.ToString());
    Assert.AreEqual(maxLength,
      value2.ToString().Length, value2.ToString());
    Assert.AreNotEqual(value1, value2);
  }

  [Test, Repeat(100)]
  public void ShouldAllowGeneratingDistinctLongWithMaxNumberOfDigits()
  {
    var maxLength = MaxLengthOfLong();
    var value1 = Any.LongIntegerWithExactDigitsCount(maxLength);
    var value2 = Any.LongIntegerWithExactDigitsCount(maxLength);

    Assert.AreEqual(maxLength,
      value1.ToString().Length,
      value1.ToString());
    Assert.AreEqual(maxLength,
      value2.ToString().Length, value2.ToString());
    Assert.AreNotEqual(value1, value2);
  }

  [Test, Repeat(100)]
  public void ShouldAllowGeneratingDistinctUnsignedLongWithMaxNumberOfDigits()
  {
    var maxLength = MaxLengthOfULong();
    var value1 = Any.UnsignedLongIntegerWithExactDigitsCount(maxLength);
    var value2 = Any.UnsignedLongIntegerWithExactDigitsCount(maxLength);

    Assert.AreEqual(maxLength,
      value1.ToString().Length,
      value1.ToString());
    Assert.AreEqual(maxLength,
      value2.ToString().Length, value2.ToString());
    Assert.AreNotEqual(value1, value2);
  }

  [Test, Repeat(100)]
  public void ShouldAllowGeneratingDistinctIntegersWithExactNumberOfDigits()
  {
    var length = MaxLengthOfInt() - 1;
    var value1 = Any.IntegerWithExactDigitsCount(length);
    var value2 = Any.IntegerWithExactDigitsCount(length);

    Assert.AreEqual(length, value1.ToString().Length, value1.ToString());
    Assert.AreEqual(length, value2.ToString().Length, value2.ToString());
    Assert.AreNotEqual(value1, value2);
  }

  [Test, Repeat(100)]
  public void ShouldAllowGeneratingDistinctUnsignedIntegersWithExactNumberOfDigits()
  {
    var length = MaxLengthOfUInt() - 1;
    var value1 = Any.UnsignedIntegerWithExactDigitsCount(length);
    var value2 = Any.UnsignedIntegerWithExactDigitsCount(length);

    Assert.AreEqual(length,
      value1.ToString().Length,
      value1.ToString());
    Assert.AreEqual(length,
      value2.ToString().Length, value2.ToString());
    Assert.AreNotEqual(value1, value2);
  }

  [Test, Repeat(100)]
  public void ShouldAllowGeneratingDistinctLongWithExactNumberOfDigits()
  {
    var length = MaxLengthOfLong() - 1;
    var value1 = Any.LongIntegerWithExactDigitsCount(length);
    var value2 = Any.LongIntegerWithExactDigitsCount(length);

    Assert.AreEqual(length,
      value1.ToString().Length,
      value1.ToString());
    Assert.AreEqual(length,
      value2.ToString().Length, value2.ToString());
    Assert.AreNotEqual(value1, value2);
  }

  [Test, Repeat(100)]
  public void ShouldAllowGeneratingDistinctUnsignedLongWithExactNumberOfDigits()
  {
    var length = MaxLengthOfULong() - 1;
    var value1 = Any.UnsignedLongIntegerWithExactDigitsCount(length);
    var value2 = Any.UnsignedLongIntegerWithExactDigitsCount(length);

    Assert.AreEqual(length,
      value1.ToString().Length,
      value1.ToString());
    Assert.AreEqual(length,
      value2.ToString().Length, value2.ToString());
    Assert.AreNotEqual(value1, value2);
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
    Assert.Null(o._field);
  }

  [Test, Parallelizable]
  public void ShouldTryToUsePublicStaticNonRecursiveFactoryMethodsOverPublicRecursiveConstructors()
  {
    var o2 = Any.Instance<ComplexObjectWithFactoryMethodAndRecursiveConstructor>();

    Assert.NotNull(o2.ToString());
    Assert.IsNotEmpty(o2.ToString());
  }

  [Test, Parallelizable]
  public void ShouldNotUseStaticCreationMethodsWithWordParseInThem()
  {
    var o2 = Any.Instance<ObjectWithStaticParseMethod>();

    Assert.NotNull(o2);
    Assert.AreNotEqual(0, o2.X);
  }

  [Test, Parallelizable]
  public void ShouldCreateCultureInfo()
  {
    var c1 = Any.Instance<CultureInfo>();
    var c2 = Any.Instance<CultureInfo>();

    Assert.NotNull(c1);
    Assert.NotNull(c2);
    Assert.AreNotEqual(c1, c2);
  }

  [Test, Repeat(10)]
  public void ShouldCreateSerializableInstances()
  {
    //SerializeAnyInstanceOf<AbstractObjectWithInterfaceInConstructor>();
    //SerializeAnyInstanceOf<AbstractObjectWithVirtualMethods>();
    SerializeAnyInstanceOf<ObjectWithCopyConstructor>();
    SerializeAnyInstanceOf<ComplexObjectWithFactoryMethodAndRecursiveConstructor>();
    //SerializeAnyInstanceOf<RecursiveInterface>();

    var x1 = Any.Instance<AbstractObjectWithInterfaceInConstructor>();
    var x2 = Any.Instance<AbstractObjectWithVirtualMethods>();
    var x3 = Any.Instance<RecursiveInterface>();
    var x4 = Any.Instance<ObjectWithCopyConstructor>();
    var x5 = Any.Instance<ComplexObjectWithFactoryMethodAndRecursiveConstructor>();
    CallSomeMethodsOn(x1, x2, x3);
    //Serialize(x1);
    //Serialize(x2);
    //Serialize(x3);
    Serialize(x4);
    Serialize(x5);
  }

  [Test, Parallelizable]
  public void ShouldGenerateVoidNotStartedTasks()
  {
    //WHEN
    var voidTask1 = Any.NotStartedTask();
    var voidTask2 = Any.NotStartedTask();
    //THEN
    Assert.NotNull(voidTask1);
    Assert.NotNull(voidTask2);
    Assert.AreNotEqual(voidTask1, voidTask2);
    Assert.DoesNotThrow(() => voidTask1.Start());
    Assert.DoesNotThrow(() => voidTask2.Start());
  }

  [Test, Parallelizable]
  public void ShouldGenerateNotStartedTasks()
  {
    //WHEN
    var task1 = Any.NotStartedTask<int>();
    var task2 = Any.NotStartedTask<int>();
    //THEN
    Assert.NotNull(task1);
    Assert.NotNull(task2);
    Assert.AreNotEqual(task1, task2);
    Assert.DoesNotThrow(() => task1.Start());
    Assert.DoesNotThrow(() => task2.Start());
  }

  [Test, Parallelizable]
  public void ShouldGenerateVoidStartedTasks()
  {
    //WHEN
    var voidTask1 = Any.StartedTask();
    var voidTask2 = Any.StartedTask();
    //THEN
    Assert.NotNull(voidTask1);
    Assert.NotNull(voidTask2);
    Assert.AreNotEqual(voidTask1, voidTask2);
    Assert.Throws<InvalidOperationException>(() => voidTask1.Start());
    Assert.Throws<InvalidOperationException>(() => voidTask2.Start());
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
    Assert.NotNull(task1);
    Assert.NotNull(task2);
    Assert.AreNotEqual(task1, task2);
    Assert.Throws<InvalidOperationException>(() => task1.Start());
    Assert.Throws<InvalidOperationException>(() => task2.Start());
    Assert.NotNull(task1.Result);
    Assert.NotNull(task2.Result);
  }

  [Test, Parallelizable]
  public void ShouldAllowGeneratingReadOnlyLists()
  {
    //GIVEN
    var readOnlyList = Any.ReadOnlyList<int>();
    //WHEN

    //THEN
    Assert.NotNull(readOnlyList);
    Assert.AreEqual(3, readOnlyList.Count);
    CollectionAssert.AllItemsAreNotNull(readOnlyList);
    CollectionAssert.AllItemsAreUnique(readOnlyList);
  }

  [Test, Parallelizable]
  public void ShouldAllowGeneratingReadOnlyListsThroughGenericMethod()
  {
    //GIVEN
    var readOnlyList = Any.Instance<IReadOnlyList<int>>();
    //WHEN

    //THEN
    Assert.NotNull(readOnlyList);
    Assert.AreEqual(3, readOnlyList.Count);
    CollectionAssert.AllItemsAreNotNull(readOnlyList);
    CollectionAssert.AllItemsAreUnique(readOnlyList);
  }

  [Test, Parallelizable]
  public void ShouldAllowGeneratingReadOnlyDictionariesThroughGenericMethod()
  {
    //GIVEN
    var readonlyDictionary = Any.Instance<IReadOnlyDictionary<int, int>>();
    //WHEN

    //THEN
    Assert.NotNull(readonlyDictionary);
    Assert.AreEqual(3, readonlyDictionary.Count);
    CollectionAssert.AllItemsAreNotNull(readonlyDictionary);
    CollectionAssert.AllItemsAreUnique(readonlyDictionary);
  }

  [Test, Parallelizable]
  public void ShouldAllowGeneratingReadOnlyListsOfSpecifiedLength()
  {
    //GIVEN
    var length = 5;
    var readOnlyList = Any.ReadOnlyList<int>(length);
    //WHEN

    //THEN
    Assert.NotNull(readOnlyList);
    Assert.AreEqual(length, readOnlyList.Count);
    CollectionAssert.AllItemsAreNotNull(readOnlyList);
    CollectionAssert.AllItemsAreUnique(readOnlyList);
  }

  [Test, Parallelizable]
  public void ShouldAllowGeneratingImmutableLists()
  {
    //GIVEN
    var readOnlyList = Any.ImmutableList<int>();
    //WHEN

    //THEN
    Assert.NotNull(readOnlyList);
    Assert.AreEqual(3, readOnlyList.Count);
    CollectionAssert.AllItemsAreNotNull(readOnlyList);
    CollectionAssert.AllItemsAreUnique(readOnlyList);
  }

  [Test, Parallelizable]
  public void ShouldAllowGeneratingDummyImmutableLists()
  {
    //GIVEN
    var readOnlyList = Any.Dummy<ImmutableList<int>>();
    //WHEN

    //THEN
    Assert.NotNull(readOnlyList);
    Assert.AreEqual(0, readOnlyList.Count);
  }

  [Test, Parallelizable]
  public void ShouldAllowGeneratingObjectsWithImmutableLists()
  {
    //GIVEN
    var obj = Any.Instance<ObjectWithImmutableList>();
    //WHEN

    //THEN
    Assert.NotNull(obj);
    Assert.AreEqual(3, obj.Elements.Count);
    CollectionAssert.AllItemsAreNotNull(obj.Elements);
    CollectionAssert.AllItemsAreUnique(obj.Elements);
  }

  [Test, Parallelizable]
  public void ShouldAllowGeneratingImmutableListsThroughGenericMethod()
  {
    //GIVEN
    var readOnlyList = Any.Instance<ImmutableList<int>>();
    //WHEN

    //THEN
    Assert.NotNull(readOnlyList);
    Assert.AreEqual(3, readOnlyList.Count);
    CollectionAssert.AllItemsAreNotNull(readOnlyList);
    CollectionAssert.AllItemsAreUnique(readOnlyList);
  }

  [Test, Parallelizable]
  public void ShouldAllowGeneratingImmutableArrays()
  {
    //GIVEN
    var collection = Any.ImmutableArray<int>();
    //WHEN

    //THEN
    Assert.NotNull(collection);
    Assert.AreEqual(3, collection.Length);
    CollectionAssert.AllItemsAreNotNull(collection);
    CollectionAssert.AllItemsAreUnique(collection);
  }

  [Test, Parallelizable]
  public void ShouldAllowGeneratingDummyImmutableArrays()
  {
    //GIVEN
    var readOnlyList = Any.Dummy<ImmutableArray<int>>();
    //WHEN

    //THEN
    Assert.NotNull(readOnlyList);
    Assert.AreEqual(0, readOnlyList.Count());
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
    Assert.NotNull(collection);
    collection.First().First().First().First().First().Length.Should().Be(1);
  }

  [Test, Parallelizable]
  public void ShouldAllowGeneratingImmutableArraysThroughGenericMethod()
  {
    //GIVEN
    var readOnlyList = Any.Instance<ImmutableList<int>>();
    //WHEN

    //THEN
    Assert.NotNull(readOnlyList);
    Assert.AreEqual(3, readOnlyList.Count);
    CollectionAssert.AllItemsAreNotNull(readOnlyList);
    CollectionAssert.AllItemsAreUnique(readOnlyList);
  }

  [Test, Parallelizable]
  public void ShouldAllowGeneratingImmutableDictionaries()
  {
    //GIVEN
    var readOnlyList = Any.ImmutableDictionary<int, int>();
    //WHEN

    //THEN
    Assert.NotNull(readOnlyList);
    Assert.AreEqual(3, readOnlyList.Count);
    CollectionAssert.AllItemsAreNotNull(readOnlyList);
    CollectionAssert.AllItemsAreUnique(readOnlyList);
  }

  [Test, Parallelizable]
  public void ShouldAllowGeneratingDummyImmutableDictionaries()
  {
    //GIVEN
    var readOnlyList = Any.Dummy<ImmutableDictionary<int, int>>();
    //WHEN

    //THEN
    Assert.NotNull(readOnlyList);
    Assert.AreEqual(0, readOnlyList.Count);
  }

  [Test, Parallelizable]
  public void ShouldAllowGeneratingImmutableDictionariesThroughGenericMethod()
  {
    //GIVEN
    var collection = Any.Instance<ImmutableDictionary<int, int>>();
    //WHEN

    //THEN
    Assert.NotNull(collection);
    Assert.AreEqual(3, collection.Count);
    CollectionAssert.AllItemsAreNotNull(collection);
    CollectionAssert.AllItemsAreUnique(collection);
  }
    
  [Test, Parallelizable]
  public void ShouldAllowGeneratingImmutableHashSets()
  {
    //GIVEN
    var collection = Any.ImmutableHashSet<int>();
    //WHEN

    //THEN
    Assert.NotNull(collection);
    Assert.AreEqual(3, collection.Count);
    CollectionAssert.AllItemsAreNotNull(collection);
    CollectionAssert.AllItemsAreUnique(collection);
  }

  [Test, Parallelizable]
  public void ShouldAllowGeneratingDummyImmutableHashSets()
  {
    //GIVEN
    var collection = Any.Dummy<ImmutableHashSet<int>>();
    //WHEN

    //THEN
    Assert.NotNull(collection);
    Assert.AreEqual(0, collection.Count);
  }

  [Test, Parallelizable]
  public void ShouldAllowGeneratingImmutableHashSetsThroughGenericMethod()
  {
    //GIVEN
    var collection = Any.Instance<ImmutableHashSet<int>>();
    //WHEN

    //THEN
    Assert.NotNull(collection);
    Assert.AreEqual(3, collection.Count);
    CollectionAssert.AllItemsAreNotNull(collection);
    CollectionAssert.AllItemsAreUnique(collection);
  }
    
  [Test, Parallelizable]
  public void ShouldAllowGeneratingImmutableQueues()
  {
    //GIVEN
    var collection = Any.ImmutableQueue<int>();
    //WHEN

    //THEN
    Assert.NotNull(collection);
    Assert.AreEqual(3, collection.Count());
    CollectionAssert.AllItemsAreNotNull(collection);
    CollectionAssert.AllItemsAreUnique(collection);
  }

  [Test, Parallelizable]
  public void ShouldAllowGeneratingDummyImmutableQueues()
  {
    //GIVEN
    var collection = Any.Dummy<ImmutableQueue<int>>();
    //WHEN

    //THEN
    Assert.NotNull(collection);
    Assert.AreEqual(0, collection.Count());
  }

  [Test, Parallelizable]
  public void ShouldAllowGeneratingImmutableQueuesThroughGenericMethod()
  {
    //GIVEN
    var collection = Any.Instance<ImmutableHashSet<int>>();
    //WHEN

    //THEN
    Assert.NotNull(collection);
    Assert.AreEqual(3, collection.Count);
    CollectionAssert.AllItemsAreNotNull(collection);
    CollectionAssert.AllItemsAreUnique(collection);
  }

  [Test, Parallelizable]
  public void ShouldAllowGeneratingImmutableSortedSets()
  {
    //GIVEN
    var collection = Any.ImmutableSortedSet<int>();
    //WHEN

    //THEN
    Assert.NotNull(collection);
    Assert.AreEqual(3, collection.Count());
    CollectionAssert.AllItemsAreNotNull(collection);
    CollectionAssert.AllItemsAreUnique(collection);
  }

  [Test, Parallelizable]
  public void ShouldAllowGeneratingDummyImmutableSortedSets()
  {
    //GIVEN
    var collection = Any.Dummy<ImmutableSortedSet<int>>();
    //WHEN

    //THEN
    Assert.NotNull(collection);
    Assert.AreEqual(0, collection.Count());
  }

  [Test, Parallelizable]
  public void ShouldAllowGeneratingImmutableSortedSetsThroughGenericMethod()
  {
    //GIVEN
    var collection = Any.Instance<ImmutableSortedSet<int>>();
    //WHEN

    //THEN
    Assert.NotNull(collection);
    Assert.AreEqual(3, collection.Count);
    CollectionAssert.AllItemsAreNotNull(collection);
    CollectionAssert.AllItemsAreUnique(collection);
  }

  [Test, Parallelizable]
  public void ShouldAllowGeneratingImmutableSortedDictionaries()
  {
    //GIVEN
    var collection = Any.ImmutableSortedDictionary<int, int>();
    //WHEN

    //THEN
    Assert.NotNull(collection);
    Assert.AreEqual(3, collection.Count);
    CollectionAssert.AllItemsAreNotNull(collection);
    CollectionAssert.AllItemsAreUnique(collection);
  }

  [Test, Parallelizable]
  public void ShouldAllowGeneratingDummyImmutableSortedDictionaries()
  {
    //GIVEN
    var collection = Any.Dummy<ImmutableSortedDictionary<int, int>>();
    //WHEN

    //THEN
    Assert.NotNull(collection);
    Assert.AreEqual(0, collection.Count);
  }

  [Test, Parallelizable]
  public void ShouldAllowGeneratingImmutableSortedDictionariesThroughGenericMethod()
  {
    //GIVEN
    var collection = Any.Instance<ImmutableSortedDictionary<int, int>>();
    //WHEN

    //THEN
    Assert.NotNull(collection);
    Assert.AreEqual(3, collection.Count);
    CollectionAssert.AllItemsAreNotNull(collection);
    CollectionAssert.AllItemsAreUnique(collection);
  }

  [Test, Parallelizable]
  public void ShouldAllowGeneratingImmutableStacks()
  {
    //GIVEN
    var collection = Any.ImmutableStack<int>();
    //WHEN

    //THEN
    Assert.NotNull(collection);
    Assert.AreEqual(3, collection.Count());
    CollectionAssert.AllItemsAreNotNull(collection);
    CollectionAssert.AllItemsAreUnique(collection);
  }

  [Test, Parallelizable]
  public void ShouldAllowGeneratingDummyImmutableStacks()
  {
    //GIVEN
    var collection = Any.Dummy<ImmutableStack<int>>();
    //WHEN

    //THEN
    Assert.NotNull(collection);
    Assert.AreEqual(0, collection.Count());
  }

  [Test, Parallelizable]
  public void ShouldAllowGeneratingImmutableStacksThroughGenericMethod()
  {
    //GIVEN
    var collection = Any.Instance<ImmutableStack<int>>();
    //WHEN

    //THEN
    Assert.NotNull(collection);
    Assert.AreEqual(3, collection.Count());
    CollectionAssert.AllItemsAreNotNull(collection);
    CollectionAssert.AllItemsAreUnique(collection);
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
  public void ShouldBeAbleToCreateJsonSerializerSettingsFromNewtonsoftJson()
  {
    //WHEN
    var serializerSettings = Any.Instance<JsonSerializerSettings>();

    //THEN
    Assert.NotNull(serializerSettings);
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
    _ = new object[] {x1.AbstractInt, x2.GetSomething(), x3.NestedAsDictionary, x2.GetSomething2(), x3.Nested};
  }

  private static void SerializeAnyInstanceOf<T>()
  {
    Serialize(Any.Instance<T>());
  }

  private static void Serialize<T>(T instance)
  {
    using var stream = new MemoryStream();
    var formatter = new BinaryFormatter();
#pragma warning disable SYSLIB0011
    formatter.Serialize(stream, instance);
#pragma warning restore SYSLIB0011
  }

  private static void AssertStringIsNumeric(string theString, int expectedLength)
  {
    Assert.AreEqual(expectedLength, theString.Length);
    foreach (var character in theString)
    {
      Assert.True(char.IsDigit(character), $"Expected digit, got {character}");
    }

    Assert.AreNotEqual('0', theString[0]);
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
}
