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
    Assert.That(element1, Is.EqualTo(returnedElement1));
    Assert.That(element2, Is.EqualTo(returnedElement2));
    Assert.That(element3, Is.EqualTo(returnedElement3));
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
    Assert.That(element1, Is.EqualTo(returnedElement1));
    Assert.That(element2, Is.EqualTo(returnedElement2));
    Assert.That(element1, Is.EqualTo(returnedElement3));
    Assert.That(element2, Is.EqualTo(returnedElement4));
  }
}
