using System.Collections.Generic;

namespace AnySpecification.Fixtures;

public class ComplexCollectionAndInterfaceSubclass : Dictionary<string, List<List<object>>>, IVisionOutput
{
}

internal interface IVisionOutput
{
}
