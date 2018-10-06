using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using TddToolkitSpecification.Fixtures;
using TddToolkitSpecification.GraphComparison;
using TddXt.AnyExtensibility;
using TddXt.AnyRoot;
using TddXt.AnyRoot.Collections;
using TddXt.AnyRoot.Invokable;
using TddXt.AnyRoot.Math;
using TddXt.AnyRoot.Network;
using TddXt.AnyRoot.Numbers;
using TddXt.AnyRoot.Strings;
using TddXt.CommonTypes;
using static TddXt.AnyRoot.Root;

// ReSharper disable PublicConstructorInAbstractClass

namespace TddToolkitSpecification
{
  public class AnySpecification
  {
    [Test]
    public void ShouldGenerateDifferentIntegerEachTime()
    {
      //GIVEN
      var int1 = Any.Integer();
      var int2 = Any.Integer();

      //THEN
      Assert.AreNotEqual(int1, int2);
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


    [Test]
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
    public void ShouldGenerateDifferentTypeEachTime()
    {
      //GIVEN
      var type1 = Any.Instance<Type>();
      var type2 = Any.Instance<Type>();
      var type3 = Any.Instance<Type>();

      //THEN
      Assert.Multiple(() =>
      {
        Assert.NotNull(type1);
        Assert.NotNull(type2);
        Assert.NotNull(type3);
      });

      Assert.Multiple(() =>
      {
        Assert.AreNotEqual(type1, type2);
        Assert.AreNotEqual(type2, type3);
        Assert.AreNotEqual(type3, type1);
      });
    }

    [Test]
    public void ShouldBeAbleToProxyConcreteReturnTypesOfMethods()
    {
      var obj = Any.Instance<ISimple>();

      Assert.AreNotEqual(default(int), obj.GetInt());
      Assert.AreNotEqual(string.Empty, obj.GetString());
      Assert.AreNotEqual(string.Empty, obj.GetStringProperty);
      Assert.NotNull(obj.GetString());
      Assert.NotNull(obj.GetStringProperty);
    }

    [Test]
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

    [Test]
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

    [Test]
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

    [Test]
    public void ShouldCreateNonNullUri()
    {
      Assert.NotNull(Any.Uri());
    }

    [Test]
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

    [Test]
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

    [Test]
    public void ShouldBeAbleToGenerateInstancesOfConcreteClassesWithInterfacesAsTheirConstructorArguments()
    {
      //GIVEN
      var createdProxy = Any.Instance<ObjectWithInterfaceInConstructor>();

      //THEN
      Assert.NotNull(createdProxy._constructorArgument);
      Assert.NotNull(createdProxy._constructorNestedArgument);
    }

    [Test]
    public void ShouldFillPropertiesWhenCreatingDataStructures()
    {
      //WHEN
      var instance = Any.Instance<ConcreteDataStructure>();

      //THEN
      Assert.NotNull(instance.Data);
      Assert.NotNull(instance._field);
      Assert.NotNull(instance.Data.Text);
    }

    [Test]
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

    [Test]
    public void ShouldOverrideVirtualMethodsThatReturnDefaultTypeValuesOnAbstractClassProxy()
    {
      //GIVEN
      var obj = Any.Instance<AbstractObjectWithVirtualMethods>();

      //THEN
      Assert.AreNotEqual(default(string), obj.GetSomething());
      Assert.AreNotEqual("Something", obj.GetSomething2());
    }

    [Test]
    public void ShouldOverrideVirtualMethodsThatThrowExceptionsOnAbstractClassProxy()
    {
      //GIVEN
      var obj = Any.Instance<AbstractObjectWithVirtualMethods>();

      //THEN
      Assert.AreNotEqual(default(string), obj.GetSomethingButThrowExceptionWhileGettingIt());
    }

    [Test]
    public void ShouldNotCreateTheSameMethodInfoTwiceInARow()
    {
      //GIVEN
      var x = Any.Instance<MethodInfo>();
      var y = Any.Instance<MethodInfo>();

      //THEN
      Assert.AreNotEqual(x, y);
    }

    [Test]
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
      CollectionAssert.AllItemsAreUnique(new [] {dof1, dof2, dof3, dof4, dof5, dof6});
      CollectionAssert.DoesNotContain(new [] {dof1, dof2, dof3, dof4, dof5, dof6, dof7}, DayOfWeek.Sunday);
    }

    [Test]
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
      CollectionAssert.AllItemsAreUnique(new[] { dof1, dof2, dof3, dof4, dof5, dof6, dof7 });
    }


    [Test]
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

    [Test]
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
        Assert.True(Enumerable.Range(1,3).Contains(int1));
        Assert.True(Enumerable.Range(1,3).Contains(int2));
        Assert.True(Enumerable.Range(1,3).Contains(int3));
        Assert.True(Enumerable.Range(1,3).Contains(int4));
        Assert.AreNotEqual(int1, int2);
        Assert.AreNotEqual(int2, int3);
        Assert.AreNotEqual(int3, int4);

        Assert.True(Enumerable.Range(5, 2).Contains(int5));
        Assert.True(Enumerable.Range(10, 4).Contains(int6));
      });
    }

    [Test]
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

    [Test]
    public void ShouldCreateSortedSetWithThreeDistinctValues()
    {
      //WHEN
      var set = Any.SortedSet<int>();

      //THEN
      CollectionAssert.IsOrdered(set);
      CollectionAssert.AllItemsAreUnique(set);
      Assert.AreEqual(3, set.Count);
    }

    [Test]
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
        Assert.True(Char.IsLetter(char1));
        Assert.True(Char.IsLetter(char2));
        Assert.True(Char.IsLetter(char3));
      });
    }

    [Test]
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
        Assert.True(Char.IsDigit(char1));
        Assert.True(Char.IsDigit(char2));
        Assert.True(Char.IsDigit(char3));
      });
    }

    [Test, Timeout(1000)]
    public void ShouldHandleEmptyExcludedStringsWhenGeneratingAnyStringNotContainingGiven()
    {
      Assert.DoesNotThrow(() => Any.StringNotContaining(string.Empty));
    }

    [Test]
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


    [Test]
    public void ShouldSupportRecursiveInterfacesWithLists()
    {
      var factories = Any.Enumerable<RecursiveInterface>().ToList();

      var x = factories[0];
      var y = x.GetNested();
      var y2 = y[0];
      var z = y2.Nested;
      var x1 = z.GetNested()[1];
      var x2 = x1.Nested.Number;

      Assert.AreNotEqual(default(int), x2);
    }

    [Test]
    public void ShouldSupportGeneratingOtherObjectsThanNull()
    {
      Assert.DoesNotThrow(() => Any.OtherThan<string>(null));
    }

    [Test]
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

    [Test]
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

    [Test]
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

    [Test]
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

    [Test]
    public void ShouldAllowCreatingCustomCollectionInstances()
    {
      var customCollection = Any.Instance<MyOwnCollection<RecursiveInterface>>();

      Assert.AreEqual(3, customCollection.Count);
      foreach (var recursiveInterface in customCollection)
      {
        Assert.NotNull(recursiveInterface);
      }
    }

    [Test]
    public void ShouldSupportCreatingArraysWithSpecificLiteralElements()
    {
      var array = Any.ArrayWith(1, 2, 3);

      CollectionAssert.Contains(array, 1);
      CollectionAssert.Contains(array, 2);
      CollectionAssert.Contains(array, 3);
      Assert.GreaterOrEqual(array.Length, 3);
    }

    [Test]
    public void ShouldSupportCreatingListsWithSpecificLiteralElements()
    {
      var list = Any.ListWith(1, 2, 3);

      CollectionAssert.Contains(list, 1);
      CollectionAssert.Contains(list, 2);
      CollectionAssert.Contains(list, 3);
      Assert.GreaterOrEqual(list.Count, 3);
    }

    [Test]
    public void ShouldSupportCreatingListsWithSpecificEnumerableOfElements()
    {
      var array = Any.ListWith<int>(new List<int> {1, 2, 3});

      CollectionAssert.Contains(array, 1);
      CollectionAssert.Contains(array, 2);
      CollectionAssert.Contains(array, 3);
      Assert.GreaterOrEqual(array.Count, 3);
    }

    [Test]
    public void ShouldSupportCreatingArraysWithSpecificEnumerableOfElements()
    {
      var array = Any.ArrayWith<int>(new List<int> {1, 2, 3});

      CollectionAssert.Contains(array, 1);
      CollectionAssert.Contains(array, 2);
      CollectionAssert.Contains(array, 3);
      Assert.GreaterOrEqual(array.Length, 3);
    }

    [Test]
    public void ShouldSupportCreationOfKeyValuePairs()
    {
      var kvp = Any.Instance<KeyValuePair<string, RecursiveInterface>>();
      Assert.Multiple(() =>
      {
        Assert.NotNull(kvp.Key);
        Assert.NotNull(kvp.Value);
      });
    }

    [Test]
    public void ShouldSupportActions()
    {
      //WHEN
      var action = Any.Instance<Action<ISimple, string>>();

      //THEN
      Assert.NotNull(action);
    }

    [Test]
    public void ShouldAllowCreatingDifferentMaybesOfConcreteClasses()
    {
      var maybeString1 = Any.Instance<Maybe<string>>();
      var maybeString2 = Any.Instance<Maybe<string>>();

      Assert.AreNotEqual(maybeString1, maybeString2);
    }

    [Test]
    public void ShouldAllowCreatingDifferentMaybesOfInterfaces()
    {
      var maybeImplementation1 = Any.Instance<Maybe<RecursiveInterface>>();
      var maybeImplementation2 = Any.Instance<Maybe<RecursiveInterface>>();

      Assert.AreNotEqual(maybeImplementation1, maybeImplementation2);
    }

    [Test]
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

    [Test]
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


    [Test]
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

    [Test]
    public void ShouldAllowGeneratingIntegersFromSequence()
    {
      var value1 = Any.IntegerFromSequence(startingValue: 12, step: 112);
      var value2 = Any.IntegerFromSequence(startingValue: 12, step: 112);

      Assert.AreEqual(value1, value2 - 112);
      Assert.Greater(value1, 12);
    }

    [Test]
    public void ShouldAllowGeneratingDivisibleIntegers()
    {
      var value1 = Any.IntegerDivisibleBy(5);
      var value2 = Any.IntegerDivisibleBy(5);

      Assert.AreNotEqual(value1, value2);
      Assert.AreEqual(0, value1%5);
      Assert.AreEqual(0, value2%5);
    }

    [Test]
    public void ShouldAllowGeneratingNotDivisibleIntegers()
    {
      var value1 = Any.IntegerNotDivisibleBy(5);
      var value2 = Any.IntegerNotDivisibleBy(5);

      Assert.AreNotEqual(value1, value2);
      Assert.AreNotEqual(0, value1%5);
      Assert.AreNotEqual(0, value2%5);
    }

    [Test]
    public void ShouldAllowGeneratingDummyObjectsBypassingConstructors()
    {
      Alike(Enumerable.Empty<string>(), Any.Dummy<IEnumerable<string>>());
      Alike(new List<string>(), Any.Dummy<List<string>>());
      Alike(new List<string>(), Any.Dummy<IList<string>>());
      Alike(new List<string>(), Any.Dummy<ICollection<string>>());
      Alike(new string[] {}, Any.Dummy<string[]>());
      Alike(new RecursiveClass[] {}, Any.Dummy<RecursiveClass[]>());
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

    [Test]
    public void ShouldAllowCustomizingTheFallbackGenerator()
    {
      Assert.DoesNotThrow(() =>
        Any.Instance<ObjectWithThrowingDependency>(
          new SingleTypeCustomization<ThrowingInConstructor>(
            (gen, trace) => gen.Dummy<ThrowingInConstructor>(trace))));
    }

    [Test]
    public void ShouldGenerateComplexGraphsWithNonNullPublicProperties()
    {
      var entity = Any.Instance<AreaEntity>();
      Assert.NotNull(entity.Feature);
    }

    [Test]
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

    [Test]
    public void ShouldAllowSettingPropertiesOnInterfaceInstancesWhenOnlySetIsPublic()
    {
      //GIVEN
      var someValue = Any.Integer();
      var obj = Any.Instance<ISettable<int>>();

      //WHEN - THEN
      Assert.DoesNotThrow(() => { obj.Value = someValue; });
    }

    [Test]
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

    [Test]
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

    [Test]
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

    [Test]
    public void ShouldHandleCopyConstructorsSomehow()
    {
      var o = Any.Instance<ObjectWithCopyConstructor>();
      Assert.Null(o._field);
    }

    [Test]
    public void ShouldTryToUsePublicStaticNonRecursiveFactoryMethodsOverPublicRecursiveConstructors()
    {
      var o2 = Any.Instance<ComplexObjectWithFactoryMethodAndRecursiveConstructor>();

      Assert.NotNull(o2.ToString());
      Assert.IsNotEmpty(o2.ToString());
    }

    [Test]
    public void ShouldNotUseStaticCreationMethodsWithWordParseInThem()
    {
      var o2 = Any.Instance<ObjectWithStaticParseMethod>();

      Assert.NotNull(o2);
      Assert.AreNotEqual(0, o2.X);
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

    [Test, Ignore("Currently per type nesting guard is disabled - it proved very slow in practical applications")]
    public void ShouldHaveRecursionLimitSetPerType()
    {
      //GIVEN
      var instance = Any.Instance<RecursiveClass>();
      
      Assert.NotNull(instance.Same.Same.Same);
      Assert.Null(instance.Same.Same.Same.Same);
      Assert.Null(instance.Same.Same.Same.Same);
      Assert.NotNull(instance.Same.Same.Whatever);

      Assert.NotNull(instance.Other.Other2.Other.Other2.Other.Other2);
      Assert.Null(instance.Other.Other2.Other.Other2.Other.Other2.Other);
    }

    [Test]
    public void ShouldUseEmptyCollectionWhenRunning()
    {
      //GIVEN
      var instance = Any.Instance<RecursiveClass>();
      
      Assert.NotNull(instance.Same.Same.Same);
      Assert.Null(instance.Same.Same.Same.Same.Same.Same);
      Assert.AreEqual(0, instance.Same.Same.Same.Same.Others.Length);
      Assert.NotNull(instance.Same.Same.Whatever);

      Assert.NotNull(instance.Other.Other2.Other.Other2.Other);
      Assert.Null(instance.Other.Other2.Other.Other2.Other.Other2);
    }

    [Test]
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

    [Test]
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

    [Test]
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

    [Test]
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

    [Test]
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

    [Test]
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

    [Test]
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

    [Test]
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

    [Test]
    public void ShouldAllowGeneratingFuncs()
    {
      //GIVEN
      var func0 = Any.Func<int>();
      var func1 = Any.Func<int, int>();
      var func = Any.Func<int, int, string>();
      
      //WHEN
      var result1 = func(1, 2);
      var result2 = func(1, 3);

      //THEN
      Assert.AreEqual(result2, result1);
    }

    [Test]
    public void ShouldBeAbleToCreateJsonSerializerSettingsFromNewtonsoftJson()
    {
      //WHEN
      var serializerSettings = Any.Instance<JsonSerializerSettings>();
      
      //THEN
      Assert.NotNull(serializerSettings);
    }

    [Test]
    public void ShouldAllowGeneratingActions()
    {
      //GIVEN
      var func = Any.Action<int, int, string>();

      //WHEN-THEN
      Assert.DoesNotThrow(() => func(1,2,"2"));
    }

    private static void CallSomeMethodsOn(AbstractObjectWithInterfaceInConstructor x1, AbstractObjectWithVirtualMethods x2,
      RecursiveInterface x3)
    {
      // ReSharper disable once UnusedVariable
      var arr = new object[] {x1.AbstractInt, x2.GetSomething(), x3.NestedAsDictionary, x2.GetSomething2(), x3.Nested};
    }

    private static void SerializeAnyInstanceOf<T>()
    {
      Serialize<T>(Any.Instance<T>());
    }

    private static void Serialize<T>(T instance)
    {
      using (MemoryStream stream = new MemoryStream())
      {
        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(stream, instance);
      }
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


    public static void Alike<T>(T expected, T actual)
    {
      var comparison = ObjectGraph.Comparison();
      var result = comparison.Compare(expected, actual);
      result.ExceededDifferences.Should().BeFalse(result.DifferencesString);
    }

    public static void NotAlike<T>(T expected, T actual)
    {
      var comparison = ObjectGraph.Comparison();
      var result = comparison.Compare(expected, actual);
      result.ExceededDifferences.Should().BeTrue(result.DifferencesString);
    }
  }

  public class ObjectWithStaticParseMethod
  {
    private ObjectWithStaticParseMethod(int x)
    {
      X = x;
    }

    public int X { get; set; }

    public static ObjectWithStaticParseMethod ParseInt(int x)
    {
      throw new Exception("Thou shalt not pass!");
    }
  }
}
