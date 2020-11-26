using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture.Kernel;
using TddXt.TypeReflection;

namespace TddXt.AnyGenerators.AutoFixtureWrapper
{
    public class PatchedFactoryMethodQuery : IMethodQuery
    {
      public IEnumerable<IMethod> SelectMethods(Type type)
        {
            return FactoryMethods(type);
        }

      public static IEnumerable<IMethod> FactoryMethods(Type type)
        {
            var factoryMethods = SmartType.For(type).FactoryMethods().Select(cw => new ConstructorWrapperToIMethod(cw));
            return factoryMethods;
        }
    }
}