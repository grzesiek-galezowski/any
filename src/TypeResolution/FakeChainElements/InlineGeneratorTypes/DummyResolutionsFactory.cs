using TddXt.TypeResolution.FakeChainElements.DummyChainElements;
using TddXt.TypeResolution.FakeChainElements.InlineGeneratorTypes.Generic.ImplementationDetails;

namespace TddXt.TypeResolution.FakeChainElements.InlineGeneratorTypes;

public class DummyResolutionsFactory(EmptyCollectionInstantiation emptyCollectionInstantiation)
{
  public IResolution FallbackDummyObjectResolution() 
    => new FallbackDummyObjectResolution();
  public IResolution ResolveDummyAbstractType() 
    => new ResolveDummyAbstractType();
  public IResolution ResolveDummyOpenGenericIEnumerable() 
    => new ResolveDummyOpenGenericIEnumerable(emptyCollectionInstantiation);
  public IResolution ResolveDummyOpenGenericImplementationOfIEnumerable() 
    => new ResolveDummyOpenGenericImplementationOfIEnumerable(emptyCollectionInstantiation);
  public IResolution ResolveDummyString() 
    => new ResolveDummyString();
  public IResolution ResolveDummyPrimitiveTypeInstance() 
    => new ResolveDummyPrimitiveTypeInstance();
}
