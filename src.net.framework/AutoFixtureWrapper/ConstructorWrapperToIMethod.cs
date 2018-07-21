using System.Collections.Generic;
using System.Reflection;
using TypeReflection.Interfaces;
using IMethod = Ploeh.AutoFixture.Kernel.IMethod;

namespace AutoFixtureWrapper
{
    public class ConstructorWrapperToIMethod : IMethod
    {
      private readonly IConstructorWrapper _cw;

      public ConstructorWrapperToIMethod(IConstructorWrapper cw)
      {
        _cw = cw;
      }

      public object Invoke(IEnumerable<object> parameters) => _cw.Invoke(parameters);
      public IEnumerable<ParameterInfo> Parameters => _cw.Parameters;
    }
}