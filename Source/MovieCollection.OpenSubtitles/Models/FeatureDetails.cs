namespace MovieCollection.OpenSubtitles.Models
{
    using Newtonsoft.Json;

    public class FeatureDetails
    {
        [JsonProperty("feature_id")]
        public int FeatureId { get; set; }

        [JsonProperty("feature_type")]
        public string? FeatureType { get; set; }

        [JsonProperty("year")]
        public int? Year { get; set; }

        [JsonProperty("title")]
        public string? Title { get; set; }

        [JsonProperty("movie_name")]
        public string? MovieName { get; set; }

        [JsonProperty("imdb_id")]
        public string? ImdbId { get; set; }

        [JsonProperty("tmdb_id")]
        public int? TmdbId { get; set; }

        [JsonProperty("season_number")]
        public int? SeasonNumber { get; set; }

        [JsonProperty("episode_number")]
        public int? EpisodeNumber { get; set; }

        [JsonProperty("parent_imdb_id")]
        public string? ParentImdbId { get; set; }

        [JsonProperty("parent_title")]
        public string? ParentTitle { get; set; }

        [JsonProperty("parent_tmdb_id")]
        public int? ParentTmdbId { get; set; }

        [JsonProperty("parent_feature_id")]
        public int? ParentFeatureId { get; set; }
    }
}
