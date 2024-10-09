# Open Subtitles API
Unofficial implementation of the Open Subtitles API for .NET

[![NuGet Version][nuget-shield]][nuget]
[![NuGet Downloads][nuget-shield-dl]][nuget]

## Installation
You can install this package via the `Package Manager Console` in Visual Studio.

```powershell
Install-Package MovieCollection.OpenSubtitles -PreRelease
```

## Configuration
Get or create a new static `HttpClient` instance if you don't have one already.

```csharp
// HttpClient lifecycle management best practices:
// https://learn.microsoft.com/dotnet/fundamentals/networking/http/httpclient-guidelines#recommended-use
private static readonly HttpClient httpClient = new HttpClient();
```

You need to set your api key and user-agent then pass it to the service's constructor.

```csharp
// using System.Net.Http.Headers;
// using MovieCollection.OpenSubtitles;

var options = new OpenSubtitlesOptions
{
    ApiKey = "your-api-key",
    ProductInformation = new ProductHeaderValue("your-app-name", "your-app-version"),
};

var service = new OpenSubtitlesService(httpClient, options);
```

**Alternatively,** you can set the user-agent via `HttpClient`'s default request headers.
```csharp
// using System.Net.Http.Headers;

var product = new ProductInfoHeaderValue("your-app-name", "your-app-version");
httpClient.DefaultRequestHeaders.UserAgent.Add(product);
```

## Search for Subtitles
You can search for subtitles via the `SearchSubtitles` method.
```csharp
var search = new NewSubtitleSearch
{
    ImdbId = 11204094,
};

var result = await service.SearchSubtitlesAsync(search);
```

You can also search by the movie name and year.
```csharp
var search = new NewSubtitleSearch
{
    Query = "Iron Man",
    Year = 2008,
};
```

You can also specify season and episode number.
```csharp
var search = new NewSubtitleSearch
{
    Query = "Rick and Morty",
    SeasonNumber = 5,
    EpisodeNumber = 10,
};
```

You can also search by movie file hash.
```csharp
string filePath = "D:\\path-to-file\\";

var search = new NewSubtitleSearch
{
    // For best results with automatic searching based on file analysis,
    // send the file name as a query together with the moviehash.
    Query = Path.GetFileName(filePath),

    // Open Subtitles is using a special hash function to match subtitle files against movie files.
    // Hash is not dependent on file name of movie file.
    MovieHash = OpenSubtitlesHasher.GetFileHash(filePath),
};
```

Please see the demo project for more examples.

## Notes
- Please read [Open Subtitles][opensub]'s [terms of use][opensub-terms] before using their their services.

## License
This project is licensed under the [MIT License](LICENSE).

[nuget]: https://www.nuget.org/packages/MovieCollection.OpenSubtitles
[nuget-shield]: https://img.shields.io/nuget/v/MovieCollection.OpenSubtitles.svg?label=NuGet
[nuget-shield-dl]: https://img.shields.io/nuget/dt/MovieCollection.OpenSubtitles?label=Downloads&color=red

[opensub]: https://www.opensubtitles.com
[opensub-docs]: https://opensubtitles.stoplight.io
[opensub-terms]: https://www.opensubtitles.com/en/tos