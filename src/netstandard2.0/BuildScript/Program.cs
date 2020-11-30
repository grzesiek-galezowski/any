using System;
using AtmaFileSystem;
using AtmaFileSystem.IO;
using static Bullseye.Targets;
using static SimpleExec.Command;

// Define directories.
var root = AbsoluteDirectoryPath.OfThisFile().ParentDirectory(3).Value;
var buildDir = root.AddDirectoryName("build");
var publishDir = root.AddDirectoryName("publish");
var srcDir = root.AddDirectoryName("src");
var configuration = "Release";
var specificationDir = root.AddDirectoryName("specification").AddDirectoryName(configuration);
var buildNetStandardDir = buildDir.AddDirectoryName("netstandard2.0");
var publishNetStandardDir = publishDir.AddDirectoryName("netstandard2.0");
var srcNetStandardDir = srcDir.AddDirectoryName("netstandard2.0");
var slnNetStandard = srcNetStandardDir.AddFileName("Any.sln");
var specificationNetStandardDir = specificationDir.AddDirectoryName("netstandard2.0");
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
    root.AddDirectoryName("nuget").Delete(true);
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

Target("Run-Unit-Tests" , () =>
{
    Run($"dotnet",
        "test " +
        $"--no-build " +
        $"-c {configuration} " +
        $"-o {buildDir} " +
        $"-p:VersionPrefix={version}", 
        workingDirectory: srcNetStandardDir.ToString());
});



Task("Run-Unit-Tests")
	.IsDependentOn("Build")
    .Does(() =>
{
    var projectFiles = GetFiles(srcNetStandardDir.ToString() + "/**/*Specification.csproj");
    foreach(var file in projectFiles)
    {
        DotNetCoreTest(file.FullPath, new DotNetCoreTestSettings           
        {
           Configuration = configuration,
        });
    }
});

Task("Pack")
	.IsDependentOn("Run-Unit-Tests")
    .Does(() => 
    {
		DotNetCorePack(srcNetStandardDir + File("AnyRoot"), defaultNugetPackSettings);
    });

Task("Push")
    .IsDependentOn("Clean")
    .IsDependentOn("Pack")
	.Does(() =>
	{
	    var projectFiles = GetFiles("./nuget/*.nupkg");
		foreach(var file in projectFiles)
		{
			DotNetCoreNuGetPush(file.FullPath, new DotNetCoreNuGetPushSettings
			{
				Source = "https://api.nuget.org/v3/index.json",
			});
		}
	});



//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task("Default")
    .IsDependentOn("Build")
    .IsDependentOn("Run-Unit-Tests")
    .IsDependentOn("Pack");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);


namespace BuildScript
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("Hello World!");
    }
  }
}
