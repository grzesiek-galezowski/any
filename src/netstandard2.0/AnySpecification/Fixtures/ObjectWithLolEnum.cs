namespace AnySpecification.Fixtures;

public class ObjectWithLolEnum
{
  public ObjectWithLolEnum(LolEnum lol)
  {
    Lol = lol;
  }

  public LolEnum Lol { get; }
}