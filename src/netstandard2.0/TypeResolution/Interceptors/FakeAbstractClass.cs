using Castle.DynamicProxy;
using TddXt.AnyExtensibility;
using TddXt.CommonTypes;
using TddXt.TypeResolution.FakeChainElements;

namespace TddXt.TypeResolution.Interceptors
{
  public class FakeAbstractClass<T> : IResolution<T>
  {
    private readonly CachedReturnValueGeneration _generation;
    private readonly ProxyGenerator _proxyGenerator;
    private readonly FallbackTypeGenerator<T> _fallbackTypeGenerator;

    public FakeAbstractClass(
      CachedReturnValueGeneration generation, 
      ProxyGenerator proxyGenerator, 
      FallbackTypeGenerator<T> fallbackTypeGenerator)
    {
      _generation = generation;
      _proxyGenerator = proxyGenerator;
      _fallbackTypeGenerator = fallbackTypeGenerator; //bug extract?
    }

    public bool Applies()
    {
      return typeof (T).IsAbstract;
    }

    public T Apply(InstanceGenerator instanceGenerator, GenerationTrace trace)
    {
      var result = (T)_proxyGenerator.CreateClassProxy(
        typeof(T),
        _fallbackTypeGenerator.GenerateConstructorParameters(instanceGenerator, trace).ToArray(), 
        new AbstractClassInterceptor(_generation, 
          instanceGenerator.Instance, trace));
      _fallbackTypeGenerator.FillFieldsAndPropertiesOf(result, instanceGenerator, trace);
      return result;
    }
  }
}