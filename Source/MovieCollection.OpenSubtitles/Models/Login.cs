namespace MovieCollection.OpenSubtitles.Models
{
    using Newtonsoft.Json;

    public class Login
    {
        [JsonProperty("user")]
        public User? User { get; set; }

        [JsonProperty("token")]
        public string? Token { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }
    }
}
