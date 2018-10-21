using TddXt.AnyExtensibility;

namespace TddXt.AnyGenerators.Generic.ImplementationDetails
{
  public class SynchronizedBasicGenerator : BasicGenerator
  {
    private readonly object _syncRoot;
    private readonly AllGenerator _allGenerator;

    public SynchronizedBasicGenerator(AllGenerator allGenerator, object syncRoot)
    {
      _allGenerator = allGenerator;
      _syncRoot = syncRoot;
    }

    public T Instance<T>()
    {
      lock (_syncRoot)
      {
        return _allGenerator.Instance<T>();
      }
    }

    public T Instance<T>(params GenerationCustomization[] customizations)
    {
      lock (_syncRoot)
      {
        return _allGenerator.Instance<T>(customizations);
      }
    }

    public T InstanceOf<T>(InlineGenerator<T> gen)
    {
      lock (_syncRoot)
      {
        return _allGenerator.InstanceOf(gen);
      }
    }
  }
}