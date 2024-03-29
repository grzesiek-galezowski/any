using System;
using System.Diagnostics.CodeAnalysis;

namespace AnySpecification.Fixtures;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
[Serializable]
public class ComplexObjectWithFactoryMethodAndRecursiveConstructor
{
  private readonly string _initialValue;

  private ComplexObjectWithFactoryMethodAndRecursiveConstructor(string parameterName)
  {
    if (string.IsNullOrEmpty(parameterName))
    {
      if (parameterName != null)
      {
        IsEmpty = true;
      }
      else
      {
        throw new ArgumentNullException(nameof(parameterName));
      }
    }

    _initialValue = parameterName;
  }

  public ComplexObjectWithFactoryMethodAndRecursiveConstructor(
    ComplexObjectWithFactoryMethodAndRecursiveConstructor obj) : this(obj._initialValue)
  {
  }

  public static ComplexObjectWithFactoryMethodAndRecursiveConstructor Empty => new(string.Empty);

  private bool IsEmpty { get; set; }

  public static ComplexObjectWithFactoryMethodAndRecursiveConstructor Create(string parameterName)
  {
    var createdWrapper =
      new ComplexObjectWithFactoryMethodAndRecursiveConstructor(parameterName);
    return createdWrapper;
  }

  public static int GetInt()
  {
    return 123;
  }

  public override string ToString()
  {
    return _initialValue;
  }

  public override int GetHashCode()
  {
    return _initialValue.GetHashCode();
  }
}