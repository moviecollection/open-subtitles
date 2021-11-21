using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using MovieCollection.OpenSubtitles;
using MovieCollection.OpenSubtitles.Models;

namespace Demo
{
    class Program
    {
        // HttpClient is intended to be instantiated once per application, rather than per-use.
        // See https://docs.microsoft.com/en-us/dotnet/api/system.net.http.httpclient
        private static readonly HttpClient _httpClient = new HttpClient();

        private static OpenSubtitlesOptions _options;
        private static OpenSubtitlesService _service;

        private static string _username;
        private static string _password;

        private static string _token;

        private static async Task Main(string[] args)
        {
            _options = new OpenSubtitlesOptions
            {
                ApiKey = "your-api-key",
                ProductInformation = new ProductHeaderValue("your-app-name", "your-app-version"),
            };

            _service = new OpenSubtitlesService(_httpClient, _options);

            _username = "your-username";
            _password = "your-password";

            await InitializeMenu();
        }

        private static async Task InitializeMenu()
        {
Start:
            Console.Clear();
            Console.WriteLine("Welcome to the 'Open Subtitles' demo.\n");

            if (!string.IsNullOrEmpty(_token))
            {
                Console.WriteLine($"Token: {_token}\n");
            }

            Console.WriteLine("1. Get Subtitle Formats");
            Console.WriteLine("2. Get User Info");
            Console.WriteLine("3. Get Subtitle Languages");
            Console.WriteLine("4. Login");
            Console.WriteLine("5. Logout");
            Console.WriteLine("6. Get Popular Featuers");
            Console.WriteLine("7. Get Latest Subtitles");
            Console.WriteLine("8. Get Most Downloaded Subtitles");
            Console.WriteLine("9. Get Subtitles By ImdbId");
            Console.WriteLine("10. Get Subtitles By Hash");
            Console.WriteLine("11. Get Subtitles By Name");
            Console.WriteLine("12. Get Subtitles By Name And Episode");
            Console.WriteLine("13. Search Features");
            Console.WriteLine("14. Guessit");
            Console.WriteLine("15. Download Subtitle");

            Console.Write("\nPlease select an option: ");
            int input = Convert.ToInt32(Console.ReadLine());

            Console.Clear();

            var task = input switch
            {
                1 => GetSubtitleFormats(),
                2 => GetUserInfo(),
                3 => GetSubtitleLanguages(),
                4 => Login(),
                5 => Logout(),
                6 => GetPopularFeatuers(),
                7 => GetLatestSubtitles(),
                8 => GetMostDownloadedSubtitles(),
                9 => GetSubtitlesByImdbId(),
                10 => GetSubtitlesByHash(),
                11 => GetSubtitlesByName(),
                12 => GetSubtitlesByNameAndEpisode(),
                13 => SearchFeatures(),
                14 => Guessit(),
                15 => DownloadSubtitle(),
                _ => null,
            };

            if (task != null)
            {
                await task;
            }

            Console.WriteLine("\nPress any key to go back to the menu...");
            Console.ReadKey();

            goto Start;
        }

        private static async Task GetSubtitleFormats()
        {
            var result = await _service.GetSubtitleFormatsAsync();

            foreach (var item in result.Data.OutputFormats)
            {
                Console.WriteLine(item);
            }
        }

        private static async Task GetUserInfo()
        {
            if (string.IsNullOrEmpty(_token))
            {
                Console.WriteLine("Please login first.");
                return;
            }

            var result = await _service.GetUserInformationAsync(_token);

            Console.WriteLine($"AllowedDownloads: {result.Data.AllowedDownloads}");
            Console.WriteLine($"Level: {result.Data.Level}");
            Console.WriteLine($"UserId: {result.Data.UserId}");
            Console.WriteLine($"ExtInstalled: {result.Data.ExtInstalled}");
            Console.WriteLine($"Vip: {result.Data.Vip}");
            Console.WriteLine($"ResetTimeUtc: {result.Data.ResetTimeUtc}");
            Console.WriteLine($"ResetTime: {result.Data.ResetTime}");
            Console.WriteLine($"DownloadsCount: {result.Data.DownloadsCount}");
            Console.WriteLine($"RemainingDownloads: {result.Data.RemainingDownloads}");
            Console.WriteLine();
        }

        private static async Task GetSubtitleLanguages()
        {
            var result = await _service.GetSubtitleLanguagesAsync();

            foreach (var item in result.Data)
            {
                Console.WriteLine($"LanguageCode: {item.LanguageCode}");
                Console.WriteLine($"LanguageName: {item.LanguageName}");
                Console.WriteLine();
            }
        }

        private static async Task Login()
        {
            if (!string.IsNullOrEmpty(_token))
            {
                Console.WriteLine("Already logged in.");
                return;
            }

            var login = new NewLogin
            {
                Username = _username,
                Password = _password,
            };

            var result = await _service.LoginAsync(login);

            Console.WriteLine($"Status: {result.Status}");

            if (result.Status == 200)
            {
                // Login was successful, save the token.
                _token = result.Token;

                Console.WriteLine($"Token: {result.Token}");
                Console.WriteLine($"AllowedDownloads: {result.User.AllowedDownloads}");
                Console.WriteLine($"Level: {result.User.Level}");
                Console.WriteLine($"UserId: {result.User.UserId}");
                Console.WriteLine($"ExtInstalled: {result.User.ExtInstalled}");
                Console.WriteLine($"Vip: {result.User.Vip}");
            }
            else
            {
                // Login failed, show the error message.
                Console.WriteLine($"Message: {result.Message}");
            }
        }

        private static async Task Logout()
        {
            if (string.IsNullOrEmpty(_token))
            {
                Console.WriteLine("Please login first.");
                return;
            }

            var logout = await _service.LogoutAsync(_token);

            Console.WriteLine($"Status: {logout.Status}");
            Console.WriteLine($"Message: {logout.Message}");

            _token = string.Empty;
        }

        private static async Task GetPopularFeatuers()
        {
            var result = await _service.GetPopularFeaturesAsync();

            foreach (var item in result.Data)
            {
                Console.WriteLine($"Id: {item.Id}");
                Console.WriteLine($"Type: {item.Type}");

                Console.WriteLine($"Title: {item.Attributes.Title}");
                Console.WriteLine($"OriginalTitle: {item.Attributes.OriginalTitle}");
                Console.WriteLine($"ImdbId: {item.Attributes.ImdbId}");
                Console.WriteLine($"TmdbId: {item.Attributes.TmdbId}");
                Console.WriteLine($"FeatureId: {item.Attributes.FeatureId}");
                Console.WriteLine($"Year: {item.Attributes.Year}");
                Console.WriteLine($"Url: {item.Attributes.Url}");
                Console.WriteLine($"ImageUrl: {item.Attributes.ImageUrl}");

                Console.WriteLine();
            }
        }

        private static async Task GetLatestSubtitles()
        {
            var result = await _service.GetLatestSubtitlesAsync();

            foreach (var item in result.Data)
            {
                Print(item);
            }
        }

        private static async Task GetMostDownloadedSubtitles()
        {
            var result = await _service.GetMostDownloadedSubtitlesAsync();

            foreach (var item in result.Data)
            {
                Print(item);
            }
        }

        private static async Task GetSubtitlesByImdbId()
        {
            var search = new NewSubtitleSearch
            {
                ImdbId = 11204094,
                Languages = new[] { "en", "fa" },
            };

            var result = await _service.SearchSubtitlesAsync(search);

            foreach (var item in result.Data)
            {
                Print(item);
            }
        }

        private static async Task GetSubtitlesByHash()
        {
            string filePath = "D:\\Iron.Man.2008.1080p.BluRay.6CH.x264.mkv";

            var search = new NewSubtitleSearch
            {
                Query = Path.GetFileName(filePath),
                MovieHash = "7f81042ca8139fbc",
                // MovieHash = OpenSubtitlesHasher.GetFileHash(filePath),
                Languages = new[] { "en", "fa" },
            };

            var result = await _service.SearchSubtitlesAsync(search);

            foreach (var item in result.Data)
            {
                Print(item);
            }
        }

        private static async Task GetSubtitlesByName()
        {
            var search = new NewSubtitleSearch
            {
                Query = "Iron Man",
                Year = 2008,
                Languages = new[] { "en", "fa" },
            };

            var result = await _service.SearchSubtitlesAsync(search);

            foreach (var item in result.Data)
            {
                Print(item);
            }
        }

        private static async Task GetSubtitlesByNameAndEpisode()
        {
            var search = new NewSubtitleSearch
            {
                Query = "Rick and Morty",
                SeasonNumber = 5,
                EpisodeNumber = 10,
                Languages = new[] { "en", "fa" },
            };

            var result = await _service.SearchSubtitlesAsync(search);

            foreach (var item in result.Data)
            {
                Print(item);
            }
        }

        public static async Task SearchFeatures()
        {
            var search = new NewFeatureSearch
            {
                Query = "Iron Man",
                Year = 2008,
            };

            var result = await _service.SearchFeaturesAsync(search);

            foreach (var item in result.Data)
            {
                Console.WriteLine($"Id: {item.Id}");
                Console.WriteLine($"Type: {item.Type}");

                Console.WriteLine($"Title: {item.Attributes.Title}");
                Console.WriteLine($"OriginalTitle: {item.Attributes.OriginalTitle}");
                Console.WriteLine($"ImdbId: {item.Attributes.ImdbId}");
                Console.WriteLine($"TmdbId: {item.Attributes.TmdbId}");
                Console.WriteLine($"FeatureId: {item.Attributes.FeatureId}");
                Console.WriteLine($"Year: {item.Attributes.Year}");
                Console.WriteLine($"Url: {item.Attributes.Url}");

                Console.WriteLine();
            }
        }

        public static async Task Guessit()
        {
            string filePath = "D:\\Clarksons.Farm.S01E01.720p.WEB-DL.2CH.x265.HEVC.mkv";
            string fileName = Path.GetFileName(filePath);

            var result = await _service.GuessitAsync(fileName);

            Console.WriteLine($"Title: {result.Title}");
            Console.WriteLine($"Year: {result.Year}");
            Console.WriteLine($"Season: {result.Season}");
            Console.WriteLine($"Episode: {result.Episode}");
            Console.WriteLine($"Language: {result.Language}");
            Console.WriteLine($"SubtitleLanguage: {result.SubtitleLanguage}");
            Console.WriteLine($"ScreenSize: {result.ScreenSize}");
            Console.WriteLine($"StreamingService: {result.StreamingService}");
            Console.WriteLine($"Source: {result.Source}");
            Console.WriteLine($"Other: {result.Other}");
            Console.WriteLine($"AudioCodec: {result.AudioCodec}");
            Console.WriteLine($"AudioChannels: {result.AudioChannels}");
            Console.WriteLine($"VideoCodec: {result.VideoCodec}");
            Console.WriteLine($"ReleaseGroup: {result.ReleaseGroup}");
            Console.WriteLine($"Type: {result.Type}");

            Console.WriteLine();
        }

        private static async Task DownloadSubtitle()
        {
            var download = new NewDownload
            {
                FileId = 3867953,
            };

            var result = await _service.GetSubtitleForDownloadAsync(download, _token);

            Console.WriteLine($"FileName: {result.FileName}");
            Console.WriteLine($"Requests: {result.Requests}");
            Console.WriteLine($"Remaining: {result.Remaining}");
            Console.WriteLine($"Message: {result.Message}");
            Console.WriteLine($"Link: {result.Link}");

            var desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var path = Path.Combine(desktop, result.FileName);

            try
            {
                Console.WriteLine();
                Console.WriteLine($"Downloading to: {path}");

                var webClient = new WebClient();
                await webClient.DownloadFileTaskAsync(result.Link, path);

                Console.WriteLine("Download was successful.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Download file error: {ex.Message}");
            }
        }

        private static void Print(AttributeResult<Subtitle> item)
        {
            Console.WriteLine($"SubtitleId: {item.Attributes.SubtitleId}");
            Console.WriteLine($"Language: {item.Attributes.Language}");
            Console.WriteLine($"DownloadCount: {item.Attributes.DownloadCount}");
            Console.WriteLine($"NewDownloadCount: {item.Attributes.NewDownloadCount}");
            Console.WriteLine($"Release: {item.Attributes.Release}");
            Console.WriteLine($"HearingImpaired: {item.Attributes.HearingImpaired}");

            Console.WriteLine();

            foreach (var file in item.Attributes.Files)
            {
                Console.WriteLine($"FileId: {file.FileId}");
                Console.WriteLine($"FileName: {file.FileName}");
                Console.WriteLine($"CD Number: {file.CdNumber}");
                Console.WriteLine();
            }

            Console.WriteLine("********************\n");
        }
    }
}
