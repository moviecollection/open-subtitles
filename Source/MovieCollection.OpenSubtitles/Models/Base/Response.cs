namespace MovieCollection.OpenSubtitles.Models
{
    using Newtonsoft.Json;

    public class Response
    {
        [JsonProperty("message")]
        public string? Message { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }
    }
}
