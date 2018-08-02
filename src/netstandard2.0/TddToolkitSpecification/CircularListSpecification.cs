using NUnit.Framework;
using TddXt.AnyRoot;
using TddXt.AnyRoot.Numbers;
using TddXt.CommonTypes;

namespace TddToolkitSpecification
{
  public class CircularListSpecification
  {
    [Test]
    public void ShouldReturnAllElementsInOrderTheyWereAdded()
    {
      //GIVEN
      var element1 = Root.Any.Integer();
      var element2 = Root.Any.Integer();
      var element3 = Root.Any.Integer();
      var list = CircularList.StartingFrom0(element1, element2, element3);

      //WHEN
      var returnedElement1 = list.Next();
      var returnedElement2 = list.Next();
      var returnedElement3 = list.Next();

      //THEN
      Assert.AreEqual(returnedElement1, element1);
      Assert.AreEqual(returnedElement2, element2);
      Assert.AreEqual(returnedElement3, element3);
    }

    [Test]
    public void ShouldStartOverReturningElementsWhenItRunsOutOfElements()
    {
      //GIVEN
      var element1 = Root.Any.Integer();
      var element2 = Root.Any.Integer();

      var list = CircularList.StartingFrom0(element1, element2);

      //WHEN
      var returnedElement1 = list.Next();
      var returnedElement2 = list.Next();
      var returnedElement3 = list.Next();
      var returnedElement4 = list.Next();

      //THEN
      Assert.AreEqual(returnedElement1, element1);
      Assert.AreEqual(returnedElement2, element2);
      Assert.AreEqual(returnedElement3, element1);
      Assert.AreEqual(returnedElement4, element2);
    }
  }
}
