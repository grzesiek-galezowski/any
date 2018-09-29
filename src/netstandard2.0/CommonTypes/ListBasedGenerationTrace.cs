using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using static System.Environment;

namespace TddXt.CommonTypes
{
  public interface GenerationTrace
  {
    void AddNestingAndCheckWith(int nesting, Type type);
    void RemoveNestingAndCheckWith(int nesting, Type type);
    void BeginCreatingInstanceGraphWith(Type type);
    void GeneratingSeedeedValue<T>(Type type, T seed);
    void BeginCreatingInstanceGraphWithInlineGenerator(Type type, object gen);
    void SelectedResolution(Type type, object resolution);
    void NestingLimitReachedTryingDummy();
    void ThirdPartyGeneratorFailedTryingFallback(Exception exception);
    void ChosenParameterlessConstructor();
    void ChosenConstructor(string constructorName, IEnumerable<TypeInfo> parameterTypes);
    string ToString();
  }

  [Serializable]
  public class ListBasedGenerationTrace : GenerationTrace
  {
    private int _nesting;
    private readonly List<string> _messages;

    public ListBasedGenerationTrace()
    {
      _nesting = 0;
      _messages = new List<string>();
    }

    public void AddNestingAndCheckWith(int nesting, Type type)
    {
      _nesting++;
      _messages.Add($"{Spaces()}START: {type}");
      AssertNestingCoherentWith(nesting);
    }

    private string Spaces()
    {
      return new string(Enumerable.Repeat(' ', _nesting).ToArray());
    }

    private void AssertNestingCoherentWith(int nesting)
    {
      if (_nesting != nesting)
      {
        throw new Exception($"Trolololo {_nesting} vs. {nesting}"); //bug
      }
    }

    public void RemoveNestingAndCheckWith(int nesting, Type type)
    {
      _messages.Add($"{Spaces()}END: {type}");
      _nesting--;
      AssertNestingCoherentWith(nesting);
    }

    public void BeginCreatingInstanceGraphWith(Type type)
    {
      _messages.Add($"{Spaces()}BUILT-IN ROOT: {type}");
    }

    public void GeneratingSeedeedValue<T>(Type type, T seed)
    {
      _messages.Add($"Generating seeded value of type {type} with seed {seed}");
    }

    public void BeginCreatingInstanceGraphWithInlineGenerator(Type type, object gen)
    {
      _messages.Add($"{Spaces()}INLINE({gen.GetType()}): {type}");
    }

    public void BeginCreatingDummyInstanceOf(Type type)
    {
      _messages.Add($"{Spaces()}DUMMY ROOT: {type}");
    }

    public void SelectedResolution(Type type, object resolution)
    {
      _messages.Add($"{Spaces()}Resolution: " + resolution.GetType());
    }

    public void NestingLimitReachedTryingDummy()
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

    private static string ParametersString(IEnumerable<TypeInfo> parameterTypes)
    {
      return "(" + (parameterTypes.Any() ? string.Join(", ", parameterTypes) + ") - parameter generation will follow." : "NONE)");
    }

    public override string ToString()
    {
      return string.Join(NewLine, _messages);
    }

  }
}