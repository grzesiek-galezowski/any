using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution.FakeChainElements.InlineGeneratorTypes.Generic.ImplementationDetails;

[Serializable]
public class ListBasedGenerationTrace : GenerationTrace
{
  [NonSerialized]
  private readonly List<string> _messages = [];
  private int _nesting = 0;

  public GenerationTrace Trace => this;

  public void AddNestingAndCheckWith(int nesting, Type type)
  {
    _nesting++;
    _messages.Add($"{Spaces()}START: {type}");
  }

  public void RemoveNestingAndCheckWith(int nesting, Type type)
  {
    _messages.Add($"{Spaces()}END: {type}");
    _nesting--;
  }

  public void GeneratingSeededValue<T>(Type type, T seed)
  {
    _messages.Add($"Generating seeded value of type {type} with seed {seed}");
  }

  public void SelectedResolution(Type type, object resolution)
  {
    _messages.Add($"{Spaces()}Resolution: " + resolution.GetType());
  }

  public void RecursionLimitReachedTryingDummy()
  {
    _messages.Add($"{Spaces()}Nesting limit reached, trying dummy implementation.");
  }

  public void ThirdPartyGeneratorFailedTryingFallback(Exception exception)
  {
    _messages.Add($"{Spaces()}3rd party generator failed. Trying fallback. Reason: {exception.Message}");
  }

  public void ChosenParameterlessConstructor()
  {
    _messages.Add($"{Spaces()}Picked default parameterless constructor");
  }

  public void ChosenConstructor(string constructorName, IEnumerable<TypeInfo> parameterTypes)
  {
    _messages.Add($"{Spaces()}Picked constructor {constructorName}{ParametersString(parameterTypes)}");
  }

  public override string ToString()
  {
    return string.Join(Environment.NewLine, _messages);
  }

  private string Spaces()
  {
    return new string(Enumerable.Repeat(' ', _nesting).ToArray());
  }

  public void BeginCreatingInstanceGraphWith(Type type)
  {
    _messages.Add($"{Spaces()}BUILT-IN ROOT: {type}");
  }

  public void BeginCreatingInstanceGraphWithInlineGenerator(Type type, object gen)
  {
    _messages.Add($"{Spaces()}INLINE({gen.GetType()}): {type}");
  }

  public void BeginCreatingDummyInstanceOf(Type type)
  {
    _messages.Add($"{Spaces()}DUMMY ROOT: {type}");
  }

  private static string ParametersString(IEnumerable<TypeInfo> parameterTypes)
  {
    return "(" + (parameterTypes.Any() ? string.Join(", ", parameterTypes) + ") - parameter generation will follow." : "NONE)");
  }
}
