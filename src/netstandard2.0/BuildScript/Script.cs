using System;
using AtmaFileSystem;
using AtmaFileSystem.IO;
using DotnetExeCommandLineBuilder;
using FluentAssertions;
using NScan.Adapter.NotifyingSupport;
using NScan.SharedKernel.WritingProgramOutput.Ports;
using TddXt.NScan;
using static Bullseye.Targets;
using static DotnetExeCommandLineBuilder.DotnetExeCommands;
using static SimpleExec.Command;

const string configuration = "Release";
const string version = "6.7.0";

// Define directories.
var root = AbsoluteFilePath.OfThisFile().ParentDirectory(3).Value;
var srcDir = root.AddDirectoryName("src");
var srcNetStandardDir = srcDir.AddDirectoryName("netstandard2.0");
var nugetPath = root.AddDirectoryName("nuget");

//////////////////////////////////////////////////////////////////////
// HELPER FUNCTIONS
//////////////////////////////////////////////////////////////////////
void Pack(AbsoluteDirectoryPath outputPath, AbsoluteDirectoryPath rootSourceDir, string projectName)
{
  Run("dotnet",
    DotnetExeCommands.Pack()
      .Configuration(configuration)
      .IncludeSymbols()
      .NoBuild()
      .WithArg($"-p:SymbolPackageFormat=snupkg")
      .WithArg($"-p:VersionPrefix={version}")
      .Output(outputPath),
    workingDirectory: rootSourceDir.AddDirectoryName(projectName).ToString());
}

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Target("Clean", () =>
{
  if (nugetPath.Exists())
  {
    nugetPath.Delete(true);
  }

  Run("dotnet", Clean().Configuration(configuration),
    workingDirectory: srcNetStandardDir.ToString());
});

Target("Build", () =>
{
  Run("dotnet",
    Build()
      .Configuration(configuration)
      .WithArg($"-p:VersionPrefix={version}"),
    workingDirectory: srcNetStandardDir.ToString());
});

Target("NScan", DependsOn("Build"), () =>
{
  NScanMain.Run(
    new InputArgumentsDto
    {
      RulesFilePath = AbsoluteDirectoryPath.OfThisFile().AddFileName("rules.txt").AsAnyFilePath(),
      SolutionPath = srcNetStandardDir.AddFileName("Any.sln").AsAnyFilePath()
    },
    new ConsoleOutput(),
    new ConsoleSupport()
  ).Should().Be(0);
});

Target("Test", DependsOn("Build"), () =>
{
  Run("dotnet",
    Test()
      .NoBuild()
      .Configuration(configuration)
      .WithArg($"-p:VersionPrefix={version}"),
    workingDirectory: srcNetStandardDir.ToString());
});

Target("Pack", DependsOn("Test", "NScan"), () =>
{
  Pack(nugetPath, srcNetStandardDir, "AnyExtensibility");
  Pack(nugetPath, srcNetStandardDir, "AnyGenerators");
  Pack(nugetPath, srcNetStandardDir, "AnyRoot");
  Pack(nugetPath, srcNetStandardDir, "TypeReflection");
  Pack(nugetPath, srcNetStandardDir, "TypeResolution");
});

Target("Push", DependsOn("Clean", "Pack"), () =>
{
  foreach (var nupkgPath in nugetPath.GetFiles("*.nupkg"))
  {
    Run("dotnet", NugetPush(nupkgPath).Source("https://api.nuget.org/v3/index.json"));
  }
});

Target("default", DependsOn("Pack"));

RunTargetsAndExit(args);

public class ConsoleOutput : INScanOutput
{
  public void WriteAnalysisReport(string analysisReport)
  {
    Console.WriteLine(analysisReport);
  }

  public void WriteVersion(string coreVersion)
  {
    Console.WriteLine(coreVersion);
  }
}

