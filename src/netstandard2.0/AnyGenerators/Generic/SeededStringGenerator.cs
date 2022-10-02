using System.Linq;
using AutoFixture;
using AutoFixture.Kernel;
using TddXt.AnyExtensibility;

namespace TddXt.AnyGenerators.Generic;

public class SeededStringGenerator : InlineGenerator<string>
{
  private readonly string _seed;
  private readonly ISpecimenBuilder _stringGenerator;

  public SeededStringGenerator(string seed)
  {
    _seed = seed;
    _stringGenerator = new DefaultPrimitiveBuilders().Single(b => b is StringGenerator);
  }

  public string GenerateInstance(InstanceGenerator instanceGenerator, GenerationRequest request)
  {
    return _stringGenerator.Create(_seed);
  }
}
