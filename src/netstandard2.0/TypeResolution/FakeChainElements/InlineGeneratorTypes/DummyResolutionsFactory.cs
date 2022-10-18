using TddXt.TypeResolution.FakeChainElements.DummyChainElements;
using TddXt.TypeResolution.FakeChainElements.InlineGeneratorTypes.Generic.ImplementationDetails;

namespace TddXt.TypeResolution.FakeChainElements.InlineGeneratorTypes;

public class DummyResolutionsFactory
{
  private readonly EmptyCollectionInstantiation _emptyCollectionInstantiation;

  public DummyResolutionsFactory(EmptyCollectionInstantiation emptyCollectionInstantiation)
  {
    _emptyCollectionInstantiation = emptyCollectionInstantiation;
  }

  public IResolution FallbackDummyObjectResolution() 
    => new FallbackDummyObjectResolution();
  public IResolution ResolveDummyAbstractType() 
    => new ResolveDummyAbstractType();
  public IResolution ResolveDummyOpenGenericIEnumerable() 
    => new ResolveDummyOpenGenericIEnumerable(_emptyCollectionInstantiation);
  public IResolution ResolveDummyOpenGenericImplementationOfIEnumerable() 
    => new ResolveDummyOpenGenericImplementationOfIEnumerable(_emptyCollectionInstantiation);
  public IResolution ResolveDummyString() 
    => new ResolveDummyString();
  public IResolution ResolveDummyPrimitiveTypeInstance() 
    => new ResolveDummyPrimitiveTypeInstance();
}
