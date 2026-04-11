using System;

namespace TddXt.TypeResolution.FakeChainElements.Interceptors;

public class BoooooomException(string name)
  : Exception("Method " + name + " is a part of exploding fake - it should not have been called");
