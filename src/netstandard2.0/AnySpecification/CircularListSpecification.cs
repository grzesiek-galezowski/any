using NUnit.Framework.Legacy;
using TddXt.AnyRoot.Numbers;
using TddXt.TypeResolution.CustomCollections;

namespace AnySpecification;

public class CircularListSpecification
{
  [Test, Parallelizable]
  public void ShouldReturnAllElementsInOrderTheyWereAdded()
  {
    //GIVEN
    var element1 = Any.Integer();
    var element2 = Any.Integer();
    var element3 = Any.Integer();
    var list = CircularList.StartingWithFirstOf(element1, element2, element3);

    //WHEN
    var returnedElement1 = list.Next();
    var returnedElement2 = list.Next();
    var returnedElement3 = list.Next();

    //THEN
    ClassicAssert.AreEqual(returnedElement1, element1);
    ClassicAssert.AreEqual(returnedElement2, element2);
    ClassicAssert.AreEqual(returnedElement3, element3);
  }

  [Test, Parallelizable]
  public void ShouldStartOverReturningElementsWhenItRunsOutOfElements()
  {
    //GIVEN
    var element1 = Any.Integer();
    var element2 = Any.Integer();

    var list = CircularList.StartingWithFirstOf(element1, element2);

    //WHEN
    var returnedElement1 = list.Next();
    var returnedElement2 = list.Next();
    var returnedElement3 = list.Next();
    var returnedElement4 = list.Next();

    //THEN
    ClassicAssert.AreEqual(returnedElement1, element1);
    ClassicAssert.AreEqual(returnedElement2, element2);
    ClassicAssert.AreEqual(returnedElement3, element1);
    ClassicAssert.AreEqual(returnedElement4, element2);
  }
}
