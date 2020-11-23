#tool "nuget:?package=NUnit.ConsoleRunner"
 
///////////////////////////////////////////////////////////////////////
// ARGUMENTS
///////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
var toolpath = Argument("toolpath", @"");

//////////////////////////////////////////////////////////////////////
// DEPENDENCIES
//////////////////////////////////////////////////////////////////////

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
Func<ProcessArgumentBuilder, ProcessArgumentBuilder> versionCustomization = args => args.Append("-p:VersionPrefix=" + version); 
var version="4.4.1";

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