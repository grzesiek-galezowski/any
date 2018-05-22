#tool "nuget:?package=NUnit.ConsoleRunner"
#tool "nuget:?package=ILRepack"
#addin nuget:?package=Cake.SemVer
#addin nuget:?package=semver&version=2.0.4

//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
var toolpath = Argument("toolpath", @"");

//////////////////////////////////////////////////////////////////////
// PREPARATION
//////////////////////////////////////////////////////////////////////

// Define directories.
var buildDir = Directory("./build") + Directory(configuration);
var publishDir = "./publish";
GitVersion gitVersion = null; 

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Clean")
    .Does(() =>
{
    CleanDirectory(buildDir);
    CleanDirectory(publishDir);
});

Task("Restore-NuGet-Packages")
    .IsDependentOn("Clean")
    .Does(() =>
{
	DotNetCoreRestore("./src/Any.sln");

    NuGetRestore("./src/Any.sln", new NuGetRestoreSettings 
	{ 
		NoCache = true,
		Verbosity = NuGetVerbosity.Detailed,
		ToolPath = FilePath.FromString("./tools/nuget.exe")
	});
});

Task("Build")
    .IsDependentOn("Restore-NuGet-Packages")
    .Does(() =>
{
    if(IsRunningOnWindows())
    {
      // Use MSBuild
      MSBuild("./src/Any.sln", settings => {
		settings.ToolPath = String.IsNullOrEmpty(toolpath) ? settings.ToolPath : toolpath;
		settings.ToolVersion = MSBuildToolVersion.VS2017;
        settings.PlatformTarget = PlatformTarget.MSIL;
		settings.SetConfiguration(configuration);
	  });
    }
    else
    {
      // Use XBuild
      XBuild("./src/Any.sln", settings =>
        settings.SetConfiguration(configuration));
    }
});

Task("Run-Unit-Tests")
	.IsDependentOn("Build")
    .Does(() =>
{
	var testAssemblies = GetFiles("./specification/*Specification.dll");
	NUnit3(testAssemblies);
});

Task("Pack")
	.IsDependentOn("Build")
    .Does(() => 
    {
		CopyDirectory("./build/Release/", publishDir);
		var assemblyPaths = GetFiles(publishDir + "/netstandard2.0/*.dll");
		var mainAssemblyPath = new FilePath(publishDir + "/netstandard2.0/TddXt.AnyRoot.dll").MakeAbsolute(Context.Environment);
		assemblyPaths.Remove(mainAssemblyPath);
		ILRepack(publishDir + "/netstandard2.0/TddXt.AnyRoot.dll", publishDir + "/netstandard2.0/TddXt.AnyRoot.dll", assemblyPaths);
		DeleteFiles(assemblyPaths);
		NuGetPack("./Any.nuspec", new NuGetPackSettings()
		{
			OutputDirectory = "./nuget",
			Version = "1.0.0",
		});  
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