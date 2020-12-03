using AtmaFileSystem;
using AtmaFileSystem.IO;
using static Bullseye.Targets;
using static SimpleExec.Command;

var configuration = "Release";

// Define directories.
var root = AbsoluteFilePath.OfThisFile().ParentDirectory(3).Value;
var srcDir = root.AddDirectoryName("src");
var srcNetStandardDir = srcDir.AddDirectoryName("netstandard2.0");
var nugetPath = root.AddDirectoryName("nuget");
var version="5.0.1";

//////////////////////////////////////////////////////////////////////
// HELPER FUNCTIONS
//////////////////////////////////////////////////////////////////////
void Pack(AbsoluteDirectoryPath outputPath, AbsoluteDirectoryPath rootSourceDir, string projectName)
{
    Run("dotnet",
        $"pack" +
        $" --include-symbols" +
        $" --no-build" +
        $" -p:SymbolPackageFormat=snupkg" +
        $" -p:VersionPrefix={version}" +
        $" -o {outputPath}",
        workingDirectory: rootSourceDir.AddDirectoryName(projectName).ToString());
}
 
//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Target("Clean" , () =>
{
    nugetPath.Delete(true);
    Run($"dotnet", 
        "clean " +
        $"-c {configuration} ",
        workingDirectory: srcNetStandardDir.ToString());
});

Target("Build" , () =>
{
    Run($"dotnet", 
        "build " +
        $"-c {configuration} " +
        $"-p:VersionPrefix={version}", 
        workingDirectory: srcNetStandardDir.ToString());
});

Target("Test", new[] {"Build"}, () =>
{
    Run($"dotnet",
        "test" +
        $" --no-build" +
        $" -c {configuration}" +
        $" -p:VersionPrefix={version}", 
        workingDirectory: srcNetStandardDir.ToString());
});

Target("Pack", new[] {"Test"}, () =>
{
    Pack(nugetPath, srcNetStandardDir, "AnyExtensibility");
    Pack(nugetPath, srcNetStandardDir, "AnyGenerators");
    Pack(nugetPath, srcNetStandardDir, "AnyRoot");
    Pack(nugetPath, srcNetStandardDir, "TypeReflection");
    Pack(nugetPath, srcNetStandardDir, "TypeResolution");
});

Target("Push", new[] {"Clean", "Pack"}, () =>
{
    foreach (var file in nugetPath.Info().GetFiles("*.nupkg"))
    {
        Run("dotnet", $"nuget push {file.FullName}" +
                      $" --source https://api.nuget.org/v3/index.json");
    }
});

Target("default", DependsOn("Pack"));

RunTargetsAndExit(args);
