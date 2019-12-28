using System.Collections.Generic;

namespace AnySpecification.Fixtures
{
  public interface RecursiveInterface
  {
    IDictionary<string, RecursiveInterface> NestedAsDictionary { get; }
    RecursiveInterface Nested { get; }
    int Number { get; }
    List<RecursiveInterface> GetNestedWithArguments(int a, int b);
    List<RecursiveInterface> GetNested();
    void VoidMethod();
    int GetNumber();
  }
}