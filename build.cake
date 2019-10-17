#tool "nuget:?package=NUnit.ConsoleRunner"
#tool "nuget:?package=ILRepack"
#addin nuget:?package=Cake.SemVer
#addin nuget:?package=semver&version=2.0.4
#tool "nuget:?package=GitVersion.CommandLine"
 
///////////////////////////////////////////////////////////////////////
// ARGUMENTS
///////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
var toolpath = Argument("toolpath", @"");
var netstandard20 = new Framework("netstandard2.0");

//////////////////////////////////////////////////////////////////////
// DEPENDENCIES
//////////////////////////////////////////////////////////////////////

var castleCore = new[] {"Castle.Core", "4.4.0"};
var autoFixtureSeed = new[] {"AutoFixture.SeedExtensions", "4.11.0"};
var autoFixture = new[] {"AutoFixture", "4.11.0"};
var fluentAssertions = new[] {"FluentAssertions", "5.9.0"};

var taskExtensions = new[] {"System.Threading.Tasks.Extensions", "4.5.3"};
var valueTuple = new[] {"System.ValueTuple", "4.5.0"};


//////////////////////////////////////////////////////////////////////
// PREPARATION
//////////////////////////////////////////////////////////////////////

// Define directories.
var buildDir = Directory("./build") + Directory(configuration);
var publishDir = Directory("./publish");
var srcDir = Directory("./src");
var specificationDir = Directory("./specification") + Directory(configuration);
var buildNetStandardDir = buildDir + Directory("netstandard2.0");
var publishNetStandardDir = publishDir + Directory("netstandard2.0");
var srcNetStandardDir = srcDir + Directory("netstandard2.0");
var slnNetStandard = srcNetStandardDir + File("Any.sln");
var specificationNetStandardDir = specificationDir + Directory("netstandard2.0");

GitVersion nugetVersion = null; 

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
		settings.ToolVersion = MSBuildToolVersion.VS2019;
        settings.PlatformTarget = PlatformTarget.MSIL;
		settings.SetConfiguration(configuration);
		settings.SetMaxCpuCount(0);
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
	CleanDirectory("./nuget");
});

Task("Restore-NuGet-Packages")
    .IsDependentOn("Clean")
    .Does(() =>
{
	RestorePackages(slnNetStandard);
});

Task("Build")
    .IsDependentOn("Restore-NuGet-Packages")
    .IsDependentOn("GitVersion")
    .Does(() =>
{
    Build(slnNetStandard);
});

Task("GitVersion")
    .Does(() =>
{
    nugetVersion = GitVersion(new GitVersionSettings {
        UpdateAssemblyInfo = true,
    });
    Console.WriteLine(nugetVersion.NuGetVersionV2);
});


Task("Run-Unit-Tests")
	.IsDependentOn("Build")
    .Does(() =>
{
	var testAssemblies = GetFiles(specificationNetStandardDir.ToString() + "/*Specification.dll");
	NUnit3(testAssemblies); 
});

public void BundleDependencies(DirectoryPath specificVersionPublishDir, string rootDllName)
{
	var fullRootDllFilePath = specificVersionPublishDir + "/" + rootDllName;
	var assemblyPaths = GetFiles(specificVersionPublishDir + "/TddXt*.dll");
	var mainAssemblyPath = new FilePath(fullRootDllFilePath).MakeAbsolute(Context.Environment);
	assemblyPaths.Remove(mainAssemblyPath);
	ILRepack(fullRootDllFilePath, fullRootDllFilePath, assemblyPaths, 
		new ILRepackSettings 
		{ 
			Parallel = true,
			Keyfile = "./src/netstandard2.0/AnyRoot/TddToolkit.snk",
			DelaySign = false,
      CopyAttrs = true,
			NDebug = false
		});
	DeleteFiles(assemblyPaths);
}


Task("Pack")
	.IsDependentOn("Build")
    .Does(() => 
    {
		CopyDirectory(buildDir, publishDir);
		BundleDependencies(publishNetStandardDir, "TddXt.AnyRoot.dll");
		NuGetPack("./Any.nuspec", new NuGetPackSettings()
		{
			Id = "Any",
			Title = "Any",
			Owners = new [] { "Grzegorz Galezowski" },
			Authors = new [] { "Grzegorz Galezowski" },
			Summary = "Anonymous value generator, supporting the 'Any.Whatever()' syntax proposed on the www.sustainabletdd.com blog.",
			Description = "Anonymous value generator, supporting the 'Any.Whatever()' syntax proposed on the www.sustainabletdd.com blog. It makes use of the static usings and extension methods to achieve flexibility and extensibility.",
			Language = "en-US",
			ReleaseNotes = new[] {"Allowed customizations to be used for concrete classes and values"},
			ProjectUrl = new Uri("https://github.com/grzesiek-galezowski/any"),
			OutputDirectory = "./nuget",
      LicenseUrl = new Uri("https://raw.githubusercontent.com/grzesiek-galezowski/any/master/LICENSE"),
			Version = nugetVersion.NuGetVersionV2, //"2.1.3",
      Symbols = false,
			Files = new [] 
			{
				new NuSpecContent {Source = @".\publish\netstandard2.0\TddXt*.*", Exclude=@"**\*.json", Target = @"lib\netstandard2.0"},
			},

			Dependencies = new [] 
			{
				netstandard20.Dependency(castleCore),
				netstandard20.Dependency(autoFixtureSeed),
				netstandard20.Dependency(autoFixture),
				netstandard20.Dependency(fluentAssertions),
				netstandard20.Dependency(taskExtensions),
				netstandard20.Dependency(valueTuple),
			}
		});  
    });

	public class Framework
	{
		string _name;

		public Framework(string name)
		{
			_name = name;
		}

		public NuSpecDependency Dependency(params string[] idAndVersion)
		{
			return new NuSpecDependency { Id = idAndVersion[0], Version = idAndVersion[1], TargetFramework = _name };
		}
	}


//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task("Default")
    .IsDependentOn("GitVersion")
    .IsDependentOn("Build")
    .IsDependentOn("Run-Unit-Tests")
    .IsDependentOn("Pack");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);