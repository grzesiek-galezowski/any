using System;
using System.Collections.Generic;

namespace AnySpecification.Fixtures;

public interface ISimple
{
  string GetStringProperty { get; }
  Type GetTypeProperty { get; }
  IEnumerable<ISimple> Simples { get; }
  int GetInt();
  string GetString();
  ISimple GetInterface();
}