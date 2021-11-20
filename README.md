# Open Subtitles API
Unofficial implementation of the Open Subtitles API.

[![Nuget Version][nuget-shield]][nuget]
[![Nuget Downloads][nuget-shield-dl]][nuget]

> Note: This package is based on beta release of the [OpenSubtitles REST API][opensub-docs].

## Installing
You can install this package by entering the following command into your `Package Manager Console`:
```powershell
Install-Package MovieCollection.OpenSubtitles -PreRelease
```

## Configuration
First, define an instance of the `HttpClient` class if you haven't already.
```csharp
// HttpClient is intended to be instantiated once per application, rather than per-use.
// See https://docs.microsoft.com/en-us/dotnet/api/system.net.http.httpclient
private static readonly HttpClient httpClient = new HttpClient();
```

Then, you need to set your api key.
```csharp
// using MovieCollection.OpenSubtitles;

var options = new OpenSubtitlesOptions
{
    ApiKey = "your-api-key",
};

var service = new OpenSubtitlesService(httpClient, _options);
```

## Searching for subtitles
You can search for subtitles via `SearchSubtitles` method.
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

Please check out the demo project for more examples.

## Notes
- Thanks to [Open Subtitles][opensub] for providing free API services. 
- Please read Open Subtitles [terms of use][opensub-terms] before using their API.

## License
This project is licensed under the [MIT License](LICENSE).

[nuget]: https://www.nuget.org/packages/MovieCollection.OpenSubtitles
[nuget-shield]: https://img.shields.io/nuget/v/MovieCollection.OpenSubtitles.svg?label=Release
[nuget-shield-dl]: https://img.shields.io/nuget/dt/MovieCollection.OpenSubtitles?label=Downloads&color=red

[opensub]: https://www.opensubtitles.com
[opensub-docs]: https://opensubtitles.stoplight.io
[opensub-terms]: https://www.opensubtitles.com/en/tos