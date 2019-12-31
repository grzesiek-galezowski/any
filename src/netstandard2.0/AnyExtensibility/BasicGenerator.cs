using System;
using System.ComponentModel;

namespace TddXt.AnyExtensibility
{
  public interface BasicGenerator
  {
    [Obsolete("Do not use this method. It does not generate strings")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    string ToString();

    T Instance<T>();
    T Instance<T>(params GenerationCustomization[] customizations);
    T InstanceOf<T>(InlineGenerator<T> gen);
  }
}