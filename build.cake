var target = Argument("target", "Test");
var configuration = Argument("configuration", "Release");

var version = Argument("package-version", "");

var source = "./Source";
var artifacts = "./.artifacts";

Task("Clean")
    .Does(() =>
{
    CleanDirectory(artifacts);
    CleanDirectory($"./Source/MovieCollection.OpenSubtitles/bin/{configuration}");
});

Task("Build")
    .IsDependentOn("Clean")
    .Does(() =>
{
    DotNetBuild("MovieCollection.OpenSubtitles.slnx", new DotNetBuildSettings
    {
        NoIncremental = true,
        WorkingDirectory = source,
        Configuration = configuration,
    });
});

Task("Test")
    .IsDependentOn("Build")
    .Does(() =>
{
    var projects = GetFiles("./Source/**/*{Tests}.csproj");

    foreach (var project in projects)
    {
        DotNetTest(project.FullPath, new DotNetTestSettings
        {
            NoBuild = true,
            WorkingDirectory = source,
            Configuration = configuration,
        });
    }
});

Task("Pack")
    .IsDependentOn("Test")
    .Does(context =>
{
    var apiKey = context.EnvironmentVariable("NUGET_API_KEY");

    if (string.IsNullOrWhiteSpace(apiKey))
    {
        throw new CakeException("No NuGet API key specified.");
    }

    if (string.IsNullOrWhiteSpace(version))
    {
        throw new CakeException("No package version specified.");
    }

    string actualVersion = version;

    if (version.StartsWith("v"))
    {
        actualVersion = version.Substring(1);
    }

    DotNetPack("MovieCollection.OpenSubtitles.slnx", new DotNetPackSettings
    {
        NoRestore = true,
        WorkingDirectory = source,
        OutputDirectory = artifacts,
        Configuration = configuration,
        MSBuildSettings = new DotNetMSBuildSettings()
            .WithProperty("Version", actualVersion)
    });

    var pushSettings = new DotNetNuGetPushSettings
    {
        ApiKey = apiKey,
        Source = "https://api.nuget.org/v3/index.json",
    };

    var files = GetFiles($"{artifacts}/*.nupkg");

    foreach (var file in files)
    {
        context.DotNetNuGetPush(file, pushSettings);
    }
});

RunTarget(target);
