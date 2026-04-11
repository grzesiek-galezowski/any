using System;
using TddXt.AnyExtensibility;
using TddXt.TypeReflection;
using TddXt.TypeResolution.CustomCollections;

namespace TddXt.TypeResolution.FakeChainElements;

public class TypeObjectsGenerator : IResolution
{
  private readonly CircularList<Type> _types = CircularList.CreateStartingFromRandom(
  typeof(Type1),
  typeof(Type2),
    typeof(Type3),
    typeof(Type4),
    typeof(Type5),
    typeof(Type6),
    typeof(Type7),
    typeof(Type8),
    typeof(Type9),
    typeof(Type10),
    typeof(Type11),
    typeof(Type12),
    typeof(Type13));

  public bool AppliesTo(Type type)
  {
    return 
      TypeOfType.Is(type);
  }

  public object Apply(InstanceGenerator instanceGenerator, GenerationRequest request, Type type)
  {
    return _types.Next();
  }

  public class Type1 { }
  public class Type2 { }
  public class Type3 { }
  public class Type4 { }
  public class Type5 { }
  public class Type6 { }
  public class Type7 { }
  public class Type8 { }
  public class Type9 { }
  public class Type10 { }
  public class Type11 { }
  public class Type12 { }
  public class Type13 { }
}
