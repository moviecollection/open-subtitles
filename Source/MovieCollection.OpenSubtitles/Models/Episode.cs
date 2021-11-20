namespace MovieCollection.OpenSubtitles.Models
{
    using Newtonsoft.Json;

    public class Episode
    {
        [JsonProperty("episode_number")]
        public int EpisodeNumber { get; set; }

        [JsonProperty("title")]
        public string? Title { get; set; }

        [JsonProperty("feature_id")]
        public int FeatureId { get; set; }

        [JsonProperty("feature_imdb_id")]
        public int? FeatureImdbId { get; set; }
    }
}
