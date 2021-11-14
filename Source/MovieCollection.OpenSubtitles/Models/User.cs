namespace MovieCollection.OpenSubtitles.Models
{
    using Newtonsoft.Json;

    public class User
    {
        [JsonProperty("allowed_downloads")]
        public int AllowedDownloads { get; set; }

        [JsonProperty("level")]
        public string? Level { get; set; }

        [JsonProperty("user_id")]
        public int UserId { get; set; }

        [JsonProperty("ext_installed")]
        public bool ExtInstalled { get; set; }

        [JsonProperty("vip")]
        public bool Vip { get; set; }
    }
}
