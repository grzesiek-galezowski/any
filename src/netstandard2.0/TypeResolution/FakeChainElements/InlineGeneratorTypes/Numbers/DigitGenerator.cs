﻿using TddXt.AnyExtensibility;
using TddXt.TypeResolution.CustomCollections;

namespace TddXt.TypeResolution.FakeChainElements.InlineGeneratorTypes.Numbers;

public class DigitGenerator : InlineGenerator<byte>
{
  private static readonly CircularList<byte> Digits =
    CircularList.CreateStartingFromRandom(new byte[] { 5, 6, 4, 7, 3, 8, 2, 9, 1, 0 });

  public byte GenerateInstance(InstanceGenerator gen, GenerationRequest request) => Digits.Next();
}
