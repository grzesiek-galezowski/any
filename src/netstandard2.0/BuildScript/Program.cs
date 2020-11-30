using System;
using System.Collections.Generic;
using System.IO;
using AtmaFileSystem;
using AtmaFileSystem.IO;
using static Bullseye.Targets;
using static SimpleExec.Command;

// Define directories.
var root = AbsoluteFilePath.OfThisFile().ParentDirectory(3).Value;
var buildDir = root.AddDirectoryName("build");
var publishDir = root.AddDirectoryName("publish");
var srcDir = root.AddDirectoryName("src");
var configuration = "Release";
var srcNetStandardDir = srcDir.AddDirectoryName("netstandard2.0");
var nugetPath = root.AddDirectoryName("nuget");
// bug Func<ProcessArgumentBuilder, ProcessArgumentBuilder> versionCustomization = 
// bug     args => args.Append("-p:VersionPrefix=" + version); 
var version="5.0.0";
// bug 
// bug var defaultNugetPackSettings = new DotNetCorePackSettings 
// bug {
// bug 	IncludeSymbols = true,
// bug 	Configuration = configuration,
// bug 	OutputDirectory = "./nuget",
// bug 	ArgumentCustomization = args => args.Append("--include-symbols -p:SymbolPackageFormat=snupkg -p:VersionPrefix=" + version)
// bug };

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Target("Clean" , () =>
{
    buildDir.Delete(true);
    publishDir.Delete(true);
    nugetPath.Delete(true);
});

Target("Build" , () =>
{
    Run($"dotnet", 
        "build " +
        $"-c {configuration} " +
        $"-o {buildDir} " +
        $"-p:VersionPrefix={version}", 
        workingDirectory: srcNetStandardDir.AddDirectoryName("AnyRoot").ToString());
});

Target("Test", new[] {"Build"}, () =>
{
    Run($"dotnet",
        "test" +
        $" --no-build" +
        $" -c {configuration}" +
        $" -o {buildDir}" +
        $" -p:VersionPrefix={version}", 
        workingDirectory: srcNetStandardDir.ToString());
});

Target("Pack", new[] {"Test"}, () =>
{
    Run("dotnet",
        $"pack" +
        $" --include-symbols" +
        $" --no-build" +
        $" -p:SymbolPackageFormat=snupkg" +
        $" -p:VersionPrefix={version}" +
        $" -o {nugetPath}",
        workingDirectory: srcNetStandardDir.AddDirectoryName("AnyRoot").ToString());
});

Target("Push", new[] {"Clean", "Pack"}, () =>
{
    foreach (var file in nugetPath.Info().GetFiles("*.nupkg"))
    {
        Run("dotnet", $"push {file.FullName}" +
                      $" --interactive" +
                      $" --source https://api.nuget.org/v3/index.json");
    }
    foreach (var file in nugetPath.Info().GetFiles("*.snupkg"))
    {
        Run("dotnet", $"push {file.FullName}" +
                      $" --interactive" +
                      $" --source https://api.nuget.org/v3/index.json");
    }
});

Target("default", DependsOn("Pack"));

RunTargetsAndExit(args);