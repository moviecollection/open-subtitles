namespace MovieCollection.OpenSubtitles.Models
{
    using Newtonsoft.Json;

    public class NewLogin
    {
        [JsonProperty("username")]
        public string? Username { get; set; }

        [JsonProperty("password")]
        public string? Password { get; set; }
    }
}
