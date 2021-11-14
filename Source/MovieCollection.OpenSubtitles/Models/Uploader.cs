namespace MovieCollection.OpenSubtitles.Models
{
    using Newtonsoft.Json;

    public class Uploader
    {
        [JsonProperty("uploader_id")]
        public int? UploaderId { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("rank")]
        public string? Rank { get; set; }
    }
}
