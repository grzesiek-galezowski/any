#tool "nuget:?package=NUnit.ConsoleRunner"
#tool "nuget:?package=ILRepack"
#addin nuget:?package=Cake.SemVer
#addin nuget:?package=semver&version=2.0.4
 
///////////////////////////////////////////////////////////////////////
// ARGUMENTS
///////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
var toolpath = Argument("toolpath", @"");

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
var version="4.1.0";
Func<ProcessArgumentBuilder, ProcessArgumentBuilder> versionCustomization = args => args.Append("-p:VersionPrefix=" + version); 

var defaultNugetPackSettings = new DotNetCorePackSettings 
{
	IncludeSymbols = true,
	Configuration = "Release",
	OutputDirectory = "./nuget",
	ArgumentCustomization = args => args.Append("--include-symbols -p:SymbolPackageFormat=snupkg -p:VersionPrefix=" + version)
};

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

Task("Build")
    .Does(() =>
{
        DotNetCoreBuild(srcNetStandardDir + Directory("AnyRoot"), new DotNetCoreBuildSettings
    {
        Configuration = configuration,
        OutputDirectory = buildDir,
	    ArgumentCustomization = versionCustomization
    });

});

Task("Run-Unit-Tests")
	.IsDependentOn("Build")
    .Does(() =>
{
	var testAssemblies = GetFiles(specificationNetStandardDir.ToString() + "/*Specification.dll");
	NUnit3(testAssemblies); 

    var projectFiles = GetFiles(srcNetStandardDir.ToString() + "/**/*Specification.csproj");
    foreach(var file in projectFiles)
    {
        DotNetCoreTest(file.FullPath, new DotNetCoreTestSettings           
        {
           Configuration = configuration,
           Logger = "trx"
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