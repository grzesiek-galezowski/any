using System;

namespace TypeResolution.Interceptors
{
  public class BoooooomException : Exception
  {
      public BoooooomException(string name) 
      : base("Method " + name + " is a part of exploding fake - it should not have been called")
      {
        
      }
  }
}

