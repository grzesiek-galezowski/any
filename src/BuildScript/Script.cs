using AtmaFileSystem;
using AtmaFileSystem.IO;
using static Bullseye.Targets;
using static DotnetExeCommandLineBuilder.DotnetExeCommands;
using static SimpleExec.Command;

const string configuration = "Release";
const string version = "10.2.1";

// Define directories.
var root = AbsoluteFilePath.OfThisFile().ParentDirectory(2).Value();
var srcDir = root.AddDirectoryName("src");
var nugetPath = root.AddDirectoryName("build").AddDirectoryName(configuration);

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Target("Clean", () =>
{
  Run("dotnet", Clean().Configuration(configuration),
    workingDirectory: srcDir.ToString());
});

Target("Build", () =>
{
  Run("dotnet",
    Build()
      .Configuration(configuration)
      .WithArg($"-p:VersionPrefix={version}")
      .WithArg($"-p:SymbolPackageFormat=snupkg"),
    workingDirectory: srcDir.ToString());

});

Target("NScan", ["Build"], () =>
{
  //NScanMain.Run(
  //  new InputArgumentsDto
  //  {
  //    RulesFilePath = AbsoluteDirectoryPath.OfThisFile().AddFileName("rules.txt").AsAnyFilePath(),
  //    SolutionPath = srcDir.AddFileName("Any.slnx").AsAnyFilePath()
  //  },
  //  new ConsoleOutput(),
  //  new ConsoleSupport(Console.WriteLine)
  //).Should().Be(0);
});

Target("Test", ["NScan"], () =>
{
  Run("dotnet",
    Test()
      .NoBuild()
      .Configuration(configuration)
      .WithArg($"-p:VersionPrefix={version}"),
    workingDirectory: srcDir.ToString());
});

Target("Push", ["Clean", "Test"], () =>
{
  foreach (var nupkgPath in nugetPath.GetFiles("*.nupkg"))
  {
    Run("dotnet", NugetPush(nupkgPath).Source("https://api.nuget.org/v3/index.json"));
  }
});

Target("default", ["Test"]);

await RunTargetsAndExitAsync(args);
