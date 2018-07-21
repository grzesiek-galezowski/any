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

public void RestorePackages(string path)
{
	DotNetCoreRestore(path);

    NuGetRestore(path, new NuGetRestoreSettings 
	{ 
		NoCache = true,
		Verbosity = NuGetVerbosity.Detailed,
		ToolPath = FilePath.FromString("./tools/nuget.exe")
	});
}

public void Build(string path)
{
    if(IsRunningOnWindows())
    {
      // Use MSBuild
      MSBuild(path, settings => {
		settings.ToolPath = String.IsNullOrEmpty(toolpath) ? settings.ToolPath : toolpath;
		settings.ToolVersion = MSBuildToolVersion.VS2017;
        settings.PlatformTarget = PlatformTarget.MSIL;
		settings.SetConfiguration(configuration);
	  });
    }
    else
    {
      // Use XBuild
      XBuild(path, settings =>
        settings.SetConfiguration(configuration));
    }
}

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
	RestorePackages("./src/Any.sln");
	RestorePackages("./src.net.framework/Any/Any.sln");
});

Task("Build")
    .IsDependentOn("Restore-NuGet-Packages")
    .Does(() =>
{
    Build("./src/Any.sln");
	Build("./src.net.framework/Any/Any.sln");
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