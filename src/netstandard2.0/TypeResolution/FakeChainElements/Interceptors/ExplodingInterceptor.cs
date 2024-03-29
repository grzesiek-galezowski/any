using Castle.DynamicProxy;

namespace TddXt.TypeResolution.FakeChainElements.Interceptors;

public class ExplodingInterceptor : IInterceptor
{
  public void Intercept(IInvocation invocation)
  {
    throw new BoooooomException(invocation.Method.Name);
  }
}