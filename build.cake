var target = Argument("Target", "Default");

var publishDir = Directory("./publish");

var buildSettings = new DotNetCoreBuildSettings
     {
         Framework = "netcoreapp2.1",
         Configuration = "Release",
     };


	Setup(ctx =>
	{
		Information("First Cake Build Running tasks...");
	});

	Teardown(ctx =>
	{
		Information("Finished running tasks.");
	});



	Task("Default")
	.Does(() => {
		Information("You Are Awesome Cake");
	});

	Task("Clean")
    .Does(() =>
    {
        CleanDirectory(publishDir);
    });

	Task("Restore")
	.IsDependentOn("Clean")
    .Does(() => {
		
		Information("Restore Task Started!!!!");
		
		DotNetCoreRestore("./WebApplicationCake.sln");

		Information("Restore Task Ended!!!!");
    });

	Task("Build")
	.IsDependentOn("Restore")
	.Does(()=>
	{
		Information("Build Task Started!!!!");

		DotNetCoreBuild("./WebApplicationCake.sln", buildSettings);

		Information("Build Task Ended!!!!");
	});

	Task("Publish")
    .IsDependentOn("Build")
    .Does(() =>
    {
        var settings = new DotNetCorePublishSettings
        {
            Framework = "netcoreapp2.1",
            Configuration = "Release",
			OutputDirectory = "./publish/",
            Runtime = "win-x64"
        };
 
        DotNetCorePublish("./WebApplicationCake.sln", settings);
    });

	Task("Zip")
    .IsDependentOn("Publish")
    .Does(() =>
    {
	   Information("Zip Task Started!!!!");

       Zip("./publish", "publish.zip");

	   Information("Zip Task Completed!!!!");
    });

	RunTarget(target);