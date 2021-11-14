namespace MovieCollection.OpenSubtitles.Models
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class FeatureSearch
    {
        [JsonProperty("title")]
        public string? Title { get; set; }

        [JsonProperty("original_title")]
        public string? OriginalTitle { get; set; }

        [JsonProperty("year")]
        public int Year { get; set; }

        [JsonProperty("subtitles_counts")]
        public Dictionary<string, int>? SubtitlesCounts { get; set; }

        [JsonProperty("subtitles_count")]
        public int SubtitlesCount { get; set; }

        [JsonProperty("seasons_count")]
        public int SeasonsCount { get; set; }

        [JsonProperty("parent_title")]
        public string? ParentTitle { get; set; }

        [JsonProperty("season_number")]
        public int SeasonNumber { get; set; }

        [JsonProperty("episode_number")]
        public int? EpisodeNumber { get; set; }

        [JsonProperty("imdb_id")]
        public string? ImdbId { get; set; }

        [JsonProperty("tmdb_id")]
        public int? TmdbId { get; set; }

        [JsonProperty("parent_imdb_id")]
        public string? ParentImdbId { get; set; }

        [JsonProperty("feature_id")]
        public int FeatureId { get; set; }

        [JsonProperty("title_aka")]
        public List<string>? TitleAka { get; set; }

        [JsonProperty("feature_type")]
        public string? FeatureType { get; set; }

        [JsonProperty("url")]
        public Uri? Url { get; set; }

        [JsonProperty("img_url")]
        public Uri? ImgUrl { get; set; }

        [JsonProperty("seasons")]
        public List<Season>? Seasons { get; set; }
    }
}
