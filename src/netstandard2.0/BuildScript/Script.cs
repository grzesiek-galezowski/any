using System;
using AtmaFileSystem;
using AtmaFileSystem.IO;
using FluentAssertions;
using NScan.Adapters.Secondary.NotifyingSupport;
using NScan.SharedKernel.WritingProgramOutput.Ports;
using TddXt.NScan;
using static Bullseye.Targets;
using static DotnetExeCommandLineBuilder.DotnetExeCommands;
using static SimpleExec.Command;

const string configuration = "Release";
const string version = "9.0.0-alfa";

// Define directories.
var root = AbsoluteFilePath.OfThisFile().ParentDirectory(3).Value();
var srcDir = root.AddDirectoryName("src");
var srcNetStandardDir = srcDir.AddDirectoryName("netstandard2.0");
var nugetPath = root.AddDirectoryName("build").AddDirectoryName(configuration);

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Target("Clean", () =>
{
  Run("dotnet", Clean().Configuration(configuration),
    workingDirectory: srcNetStandardDir.ToString());
});

Target("Build", () =>
{
  Run("dotnet",
    Build()
      .Configuration(configuration)
      .WithArg($"-p:VersionPrefix={version}")
      .WithArg($"-p:SymbolPackageFormat=snupkg"),
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
    new ConsoleSupport(Console.WriteLine)
  ).Should().Be(0);
});

Target("Test", DependsOn("NScan"), () =>
{
  Run("dotnet",
    Test()
      .NoBuild()
      .Configuration(configuration)
      .WithArg($"-p:VersionPrefix={version}"),
    workingDirectory: srcNetStandardDir.ToString());
});

Target("Push", DependsOn("Clean", "Test"), () =>
{
  foreach (var nupkgPath in nugetPath.GetFiles("*.nupkg"))
  {
    Run("dotnet", NugetPush(nupkgPath).Source("https://api.nuget.org/v3/index.json"));
  }
});

Target("default", DependsOn("Test"));

await RunTargetsAndExitAsync(args);

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

