namespace MovieCollection.OpenSubtitles
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;
    using MovieCollection.OpenSubtitles.Models;
    using Newtonsoft.Json;

    /// <summary>
    /// The <see cref="OpenSubtitlesService"/> class.
    /// </summary>
    public class OpenSubtitlesService
    {
        private readonly HttpClient _httpClient;
        private readonly OpenSubtitlesOptions _options;

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenSubtitlesService"/> class.
        /// </summary>
        /// <param name="httpClient">An instance of <see cref="HttpClient"/>.</param>
        /// <param name="options">An instance of <see cref="OpenSubtitlesOptions"/>.</param>
        public OpenSubtitlesService(HttpClient httpClient, OpenSubtitlesOptions options)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _options = options ?? throw new ArgumentNullException(nameof(options));

            if (string.IsNullOrWhiteSpace(_options.ApiAddress))
            {
                throw new ArgumentException($"'{nameof(_options.ApiAddress)}' cannot be null or whitespace.", nameof(options));
            }

            if (string.IsNullOrWhiteSpace(_options.ApiKey))
            {
                throw new ArgumentException($"'{nameof(_options.ApiKey)}' cannot be null or whitespace.", nameof(options));
            }
        }

        /// <summary>
        /// List subtitle formats recognized by the api.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public Task<Result<SubtitleFormats>> GetSubtitleFormatsAsync()
        {
            return GetJsonAsync<Result<SubtitleFormats>>("/api/v1/infos/formats");
        }

        /// <summary>
        /// Gather informations about the user authenticated by a bearer token.
        /// User information are already sent when user is authenticated, and the remaining downloads is
        /// returned with each download, but you can also get these information here.
        /// </summary>
        /// <param name="token">User token created in the login endpoint.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public Task<Result<UserDetails>> GetUserInformationAsync(string token)
        {
            return GetJsonAsync<Result<UserDetails>>("/api/v1/infos/user", token: token);
        }

        /// <summary>
        /// Get the languages table containing the codes and names used through the api.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public Task<Result<List<SubtileLanguage>>> GetSubtitleLanguagesAsync()
        {
            return GetJsonAsync<Result<List<SubtileLanguage>>>("/api/v1/infos/languages");
        }

        /// <summary>
        /// Create a token to authenticate a user.
        /// </summary>
        /// <param name="login">An instance of <see cref="NewLogin"/>.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public Task<Login> LoginAsync(NewLogin login)
        {
            return PostJsonAsync<Login>("/api/v1/login", login);
        }

        /// <summary>
        /// Destroy a user token to end a session.
        /// Bearer token is required for this endpoint.
        /// </summary>
        /// <param name="token">The user token created in the login endpoint.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public Task<Response> LogoutAsync(string token)
        {
            return DeleteAsync<Response>("/api/v1/logout", token);
        }

        /// <summary>
        /// Discover popular features on opensubtitles.com, according to last 30 days downloads.
        /// </summary>
        /// <param name="languages">The language codes (en, fr).</param>
        /// <param name="type">The type (movie or tvshow).</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public Task<Result<List<AttributeResult<FeaturePopular>>>> GetPopularFeaturesAsync(IList<string>? languages = null, string? type = null)
        {
            var parameters = new Dictionary<string, object>();

            if (languages?.Any() == true)
            {
                parameters.Add("languages", string.Join(",", languages));
            }

            if (!string.IsNullOrEmpty(type))
            {
                parameters.Add("type", type);
            }

            return GetJsonAsync<Result<List<AttributeResult<FeaturePopular>>>>("/api/v1/discover/popular", parameters);
        }

        /// <summary>
        /// Lists 60 latest uploaded subtitles.
        /// </summary>
        /// <param name="languages">The language codes (en, fr).</param>
        /// <param name="type">The type (movie or tvshow).</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public Task<PagedResult<AttributeResult<Subtitle>>> GetLatestSubtitlesAsync(IList<string>? languages = null, string? type = null)
        {
            var parameters = new Dictionary<string, object>();

            if (languages?.Any() == true)
            {
                parameters.Add("languages", string.Join(",", languages));
            }

            if (!string.IsNullOrEmpty(type))
            {
                parameters.Add("type", type);
            }

            return GetJsonAsync<PagedResult<AttributeResult<Subtitle>>>("/api/v1/discover/latest", parameters);
        }

        /// <summary>
        /// Discover popular subtitles, according to last 30 days downloads on opensubtitles.com.
        /// This list can be filtered by language code or feature type (movie, episode).
        /// </summary>
        /// <param name="languages">The language codes (en, fr).</param>
        /// <param name="type">The type (movie or tvshow).</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public Task<PagedResult<AttributeResult<Subtitle>>> GetMostDownloadedSubtitlesAsync(IList<string>? languages = null, string? type = null)
        {
            var parameters = new Dictionary<string, object>();

            if (languages?.Any() == true)
            {
                parameters.Add("languages", string.Join(",", languages));
            }

            if (!string.IsNullOrEmpty(type))
            {
                parameters.Add("type", type);
            }

            return GetJsonAsync<PagedResult<AttributeResult<Subtitle>>>("/api/v1/discover/most_downloaded", parameters);
        }

        /// <summary>
        /// Request a download url for a subtitle.
        /// The download count is calculated on this action, not the file download itself.
        /// </summary>
        /// <remarks>
        /// <para>The download URL is temporary, and cannot be used more than 3 hours,
        /// so do not cache it, but you can download the file more than once if needed.</para>
        /// The <see cref="NewDownload.InFps"/> and <see cref="NewDownload.InFps"/> must be indicated for
        /// subtitle conversions, we want to make sure you know what you are doing, and therefore
        /// collected the current FPS from the subtitle search result, or calculated it somehow.
        /// </remarks>
        /// <param name="download">An instance of the <see cref="NewDownload"/> class.</param>
        /// <param name="token">The user token created in the login endpoint.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public Task<Download> GetSubtitleForDownloadAsync(NewDownload download, string token)
        {
            if (download is null)
            {
                throw new ArgumentNullException(nameof(download));
            }

            return PostJsonAsync<Download>("/api/v1/download", download, token);
        }

        /// <summary>
        /// Find subtitle for a video file.
        /// All parameters can be combined following various logics: searching by a specific external id (imdb, tmdb), a file moviehash, or a simple text query.
        /// </summary>
        /// <remarks>
        /// For best results with automatic searching based on file analysis,
        /// send the file name as a query together with the moviehash.
        /// </remarks>
        /// <param name="search">An instance of <see cref="NewSubtitleSearch"/>.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public Task<PagedResult<AttributeResult<Subtitle>>> SearchSubtitlesAsync(NewSubtitleSearch search)
        {
            if (search is null)
            {
                throw new ArgumentNullException(nameof(search));
            }

            var parameters = new Dictionary<string, object>();

            if (!string.IsNullOrEmpty(search.AiTranslated))
            {
                parameters.Add("ai_translated", search.AiTranslated);
            }

            if (search.EpisodeNumber.HasValue)
            {
                parameters.Add("episode_number", search.EpisodeNumber);
            }

            if (!string.IsNullOrEmpty(search.ForeignPartsOnly))
            {
                parameters.Add("foreign_parts_only", search.ForeignPartsOnly);
            }

            if (!string.IsNullOrEmpty(search.HearingImpaired))
            {
                parameters.Add("hearing_impaired", search.HearingImpaired);
            }

            if (search.Id.HasValue)
            {
                parameters.Add("id", search.Id);
            }

            if (search.ImdbId.HasValue)
            {
                parameters.Add("imdb_id", search.ImdbId);
            }

            if (search.Languages?.Any() == true)
            {
                parameters.Add("languages", string.Join(",", search.Languages));
            }

            if (!string.IsNullOrEmpty(search.MachineTranslated))
            {
                parameters.Add("machine_translated", search.MachineTranslated);
            }

            if (!string.IsNullOrEmpty(search.MovieHash))
            {
                parameters.Add("moviehash", search.MovieHash);
            }

            if (!string.IsNullOrEmpty(search.MovieHashMatch))
            {
                parameters.Add("moviehash_match", search.MovieHashMatch);
            }

            if (!string.IsNullOrEmpty(search.OrderBy))
            {
                parameters.Add("order_by", search.OrderBy);
            }

            if (!string.IsNullOrEmpty(search.OrderDirection))
            {
                parameters.Add("order_direction", search.OrderDirection);
            }

            if (search.Page.HasValue)
            {
                parameters.Add("page", search.Page);
            }

            if (search.ParentFeatureId.HasValue)
            {
                parameters.Add("parent_feature_id", search.ParentFeatureId);
            }

            if (search.ParentImdbId.HasValue)
            {
                parameters.Add("parent_imdb_id", search.ParentImdbId);
            }

            if (search.ParentTmdbId.HasValue)
            {
                parameters.Add("parent_tmdb_id", search.ParentTmdbId);
            }

            if (!string.IsNullOrWhiteSpace(search.Query))
            {
                parameters.Add("query", search.Query);
            }

            if (search.SeasonNumber.HasValue)
            {
                parameters.Add("season_number", search.SeasonNumber);
            }

            if (search.TmdbId.HasValue)
            {
                parameters.Add("tmdb_id", search.TmdbId);
            }

            if (!string.IsNullOrEmpty(search.TrustedSources))
            {
                parameters.Add("trusted_sources", search.TrustedSources);
            }

            if (!string.IsNullOrEmpty(search.Type))
            {
                parameters.Add("type", search.Type);
            }

            if (search.UserId.HasValue)
            {
                parameters.Add("user_id", search.UserId);
            }

            if (search.Year.HasValue)
            {
                parameters.Add("year", search.Year);
            }

            return GetJsonAsync<PagedResult<AttributeResult<Subtitle>>>("/api/v1/subtitles", parameters);
        }

        /// <summary>
        /// With the "query" parameter, search for a Feature from a simple text input. Typically used for a text search or autocomplete.
        /// With an ID, get basic information and subtitles count for a specific title.
        /// </summary>
        /// <remarks>
        /// If you create an autocomplete, don't set a too small refresh limit,
        /// remember you must not go over 40 requests per 10 seconds.
        /// </remarks>
        /// <param name="search">An instance of <see cref="NewFeatureSearch"/>.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public Task<Result<List<AttributeResult<FeatureSearch>>>> SearchFeaturesAsync(NewFeatureSearch search)
        {
            if (search is null)
            {
                throw new ArgumentNullException(nameof(search));
            }

            var parameters = new Dictionary<string, object>();

            if (!string.IsNullOrWhiteSpace(search.Query))
            {
                parameters.Add("query", search.Query);
            }

            if (search.FeatureId.HasValue)
            {
                parameters.Add("feature_id", search.FeatureId);
            }

            if (search.ImdbId.HasValue)
            {
                parameters.Add("imdb_id", search.ImdbId);
            }

            if (search.TmdbId.HasValue)
            {
                parameters.Add("tmdb_id", search.TmdbId);
            }

            if (!string.IsNullOrEmpty(search.Type))
            {
                parameters.Add("type", search.Type);
            }

            if (search.Year.HasValue)
            {
                parameters.Add("year", search.Year);
            }

            return GetJsonAsync<Result<List<AttributeResult<FeatureSearch>>>>("/api/v1/features", parameters);
        }

        /// <summary>
        /// Extracts as much information as possible from a video filename.
        /// It has a very powerful matcher that allows to guess properties from a video using its filename only.
        /// This matcher works with both movies and tv shows episodes.
        /// </summary>
        /// <param name="fileName">The file name to match.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public Task<Guessit> GuessitAsync(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentException($"'{nameof(fileName)}' cannot be null or whitespace.", nameof(fileName));
            }

            var parameters = new Dictionary<string, object>
            {
                ["filename"] = fileName,
            };

            return GetJsonAsync<Guessit>("/api/v1/utilities/guessit", parameters);
        }

        private static string GetParametersString(Dictionary<string, object> parameters)
        {
            var builder = new StringBuilder();

            // Sort the parameters alphabetically to avoid http redirection.
            foreach (var item in parameters.OrderBy(x => x.Key))
            {
                builder.Append(builder.Length == 0 ? "?" : "&");
                builder.Append($"{item.Key.ToLowerInvariant()}={item.Value.ToString()?.ToLowerInvariant()}");
            }

            return builder.ToString();
        }

        private async Task<T> DeleteAsync<T>(string requestUrl, string token)
        {
            string url = _options.ApiAddress + requestUrl;

            using var request = new HttpRequestMessage(HttpMethod.Delete, url);

            request.Headers.Add("Api-Key", _options.ApiKey);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            using var response = await _httpClient.SendAsync(request)
                .ConfigureAwait(false);

            string json = await response.Content.ReadAsStringAsync()
                .ConfigureAwait(false);

            return JsonConvert.DeserializeObject<T>(json);
        }

        private async Task<T> PostJsonAsync<T>(string requestUrl, object obj, string? token = null)
        {
            string url = _options.ApiAddress + requestUrl;

            using var request = new HttpRequestMessage(HttpMethod.Post, url);

            request.Headers.Add("Api-Key", _options.ApiKey);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            string requestJson = JsonConvert.SerializeObject(obj);
            request.Content = new StringContent(requestJson, Encoding.UTF8, "application/json");

            using var response = await _httpClient.SendAsync(request)
                .ConfigureAwait(false);

            string json = await response.Content.ReadAsStringAsync()
                .ConfigureAwait(false);

            return JsonConvert.DeserializeObject<T>(json);
        }

        private async Task<T> GetJsonAsync<T>(string requestUrl, Dictionary<string, object>? parameters = null, string? token = null)
        {
            string url = _options.ApiAddress + requestUrl;

            if (parameters?.Any() == true)
            {
                url += GetParametersString(parameters);
            }

            using var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("Api-Key", _options.ApiKey);

            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            using var response = await _httpClient.SendAsync(request)
                .ConfigureAwait(false);

            string json = await response.Content.ReadAsStringAsync()
                .ConfigureAwait(false);

            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
