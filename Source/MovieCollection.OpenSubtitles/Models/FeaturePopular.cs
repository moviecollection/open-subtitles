namespace MovieCollection.OpenSubtitles.Models
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class FeaturePopular
    {
        [JsonProperty("title")]
        public string? Title { get; set; }

        [JsonProperty("original_title")]
        public string? OriginalTitle { get; set; }

        [JsonProperty("imdb_id")]
        public string? ImdbId { get; set; }

        [JsonProperty("tmdb_id")]
        public int? TmdbId { get; set; }

        [JsonProperty("feature_id")]
        public int FeatureId { get; set; }

        [JsonProperty("year")]
        public int? Year { get; set; }

        [JsonProperty("title_aka")]
        public List<string>? TitleAka { get; set; }

        [JsonProperty("subtitles_counts")]
        public Dictionary<string, int>? SubtitlesCounts { get; set; }

        [JsonProperty("url")]
        public Uri? Url { get; set; }

        [JsonProperty("img_url")]
        public Uri? ImageUrl { get; set; }

        [JsonProperty("seasons")]
        public List<Season>? Seasons { get; set; }
    }
}
