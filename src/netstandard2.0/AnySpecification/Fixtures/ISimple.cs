using System;
using System.Collections.Generic;

namespace TddToolkitSpecification.Fixtures
{
  public interface ISimple
  {
    string GetStringProperty { get; }
    Type GetTypeProperty { get; }
    IEnumerable<ISimple> Simples { get; }
    int GetInt();
    string GetString();
    ISimple GetInterface();
  }
}