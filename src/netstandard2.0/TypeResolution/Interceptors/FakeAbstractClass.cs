using Castle.DynamicProxy;
using TddXt.AnyExtensibility;
using TypeResolution.FakeChainElements;

namespace TypeResolution.Interceptors
{
  public class FakeAbstractClass<T> : IResolution<T>
  {
    private readonly CachedReturnValueGeneration _generation;
    private readonly ProxyGenerator _proxyGenerator;
    private readonly FallbackTypeGenerator<T> _fallbackTypeGenerator;

    public FakeAbstractClass(
      CachedReturnValueGeneration generation, 
      ProxyGenerator proxyGenerator)
    {
      _generation = generation;
      _proxyGenerator = proxyGenerator;
      _fallbackTypeGenerator = new FallbackTypeGenerator<T>(); //bug extract?
    }

    public bool Applies()
    {
      return typeof (T).IsAbstract;
    }

    public T Apply(InstanceGenerator instanceGenerator)
    {
      var result = (T)(_proxyGenerator.CreateClassProxy(
        typeof(T),
        _fallbackTypeGenerator.GenerateConstructorParameters(instanceGenerator).ToArray(), 
        new AbstractClassInterceptor(_generation, 
        instanceGenerator.Instance)));
      _fallbackTypeGenerator.FillFieldsAndPropertiesOf(result, instanceGenerator);
      return result;
    }
  }
}