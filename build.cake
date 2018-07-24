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
var buildFrameworkDir = Directory("./build.net.framework") + Directory(configuration);
var publishFrameworkDir = "./publish.net.framework";

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
    CleanDirectory(buildFrameworkDir);
    CleanDirectory(publishFrameworkDir);
});

Task("Restore-NuGet-Packages")
    .IsDependentOn("Clean")
    .Does(() =>
{
	RestorePackages("./src/Any.sln");
	RestorePackages("./src.net.framework/Any.sln");
});

Task("Build")
    .IsDependentOn("Restore-NuGet-Packages")
    .Does(() =>
{
    Build("./src/Any.sln");
	Build("./src.net.framework/Any.sln");
});

Task("Run-Unit-Tests")
	.IsDependentOn("Build")
    .Does(() =>
{
	var testAssemblies = GetFiles("./specification/*Specification.dll");
	NUnit3(testAssemblies); 
	var frameworkTestAssemblies = GetFiles("./specification.net.framework/*Specification.dll");
	NUnit3(frameworkTestAssemblies); 

});

public void BundleDependencies(DirectoryPath specificVersionPublishDir, string rootDllName)
{
	var fullRootDllFilePath = specificVersionPublishDir + "/" + rootDllName;
	var assemblyPaths = GetFiles(specificVersionPublishDir + "/*.dll");
	var mainAssemblyPath = new FilePath(fullRootDllFilePath).MakeAbsolute(Context.Environment);
	assemblyPaths.Remove(mainAssemblyPath);
	ILRepack(fullRootDllFilePath, fullRootDllFilePath, assemblyPaths);
	DeleteFiles(assemblyPaths);
}

public void BuildNuGetPackage()
{
	var specificVersionPublishDir = publishDir + "/netstandard2.0/";

	CopyDirectory("./build/Release/", publishDir);
	BundleDependencies(specificVersionPublishDir, "TddXt.AnyRoot.dll");
	NuGetPack("./Any.nuspec", new NuGetPackSettings()
	{
		Id = "Any",
		Title = "Any",
		Owners = new [] { "Grzegorz Galezowski" },
		Authors = new [] { "Grzegorz Galezowski" },
		Summary = "Anonymous value generator, supporting the &quot;Any.Whatever()&quot; syntax proposed on the www.sustainabletdd.com blog.",
		Description = "Anonymous value generator, supporting the &quot;Any.Whatever()&quot; syntax proposed on the www.sustainabletdd.com blog. It makes use of the static usings and extension methods to achieve flexibility and extensibility.",
		Language = "en-US",
		ProjectUrl = new Uri("https://github.com/grzesiek-galezowski/any"),
		OutputDirectory = "./nuget",
		Version = "1.0.0",
		Files = new [] 
		{
			new NuSpecContent {Source = @".\publish\netstandard2.0\*.*", Exclude=@"**\*.json", Target = @"lib\netstandard2.0"},
		}
	});  
}

Task("Pack")
	.IsDependentOn("Build")
    .Does(() => 
    {
		BuildNuGetPackage();
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