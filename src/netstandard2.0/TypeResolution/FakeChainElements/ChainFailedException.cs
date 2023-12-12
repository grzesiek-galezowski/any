using System;

namespace TddXt.TypeResolution.FakeChainElements;

internal class ChainFailedException(Type type) : Exception("Chain failed while trying to create " + type);
