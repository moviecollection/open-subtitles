namespace MovieCollection.OpenSubtitles.Models
{
    using Newtonsoft.Json;

    public class Login : Response
    {
        [JsonProperty("user")]
        public User? User { get; set; }

        [JsonProperty("token")]
        public string? Token { get; set; }
    }
}
