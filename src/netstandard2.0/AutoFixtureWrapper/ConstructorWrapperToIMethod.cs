using System.Collections.Generic;
using System.Reflection;
using TddXt.TypeReflection.Interfaces;
#if NETFRAMEWORK
using IMethod = Ploeh.AutoFixture.Kernel.IMethod;
#else
using IMethod = AutoFixture.Kernel.IMethod;
#endif

namespace TddXt.AutoFixtureWrapper
{
    public class ConstructorWrapperToIMethod : IMethod
    {
      private readonly IConstructorWrapper _cw;
      public ConstructorWrapperToIMethod(IConstructorWrapper cw) => _cw = cw;
      public object Invoke(IEnumerable<object> parameters) => _cw.Invoke(parameters);
      public IEnumerable<ParameterInfo> Parameters => _cw.Parameters;
    }
}