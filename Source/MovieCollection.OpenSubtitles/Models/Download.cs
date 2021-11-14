namespace MovieCollection.OpenSubtitles.Models
{
    using System;
    using Newtonsoft.Json;

    public class Download
    {
        [JsonProperty("link")]
        public Uri? Link { get; set; }

        [JsonProperty("file_name")]
        public string? FileName { get; set; }

        [JsonProperty("requests")]
        public int Requests { get; set; }

        [JsonProperty("remaining")]
        public int Remaining { get; set; }

        [JsonProperty("message")]
        public string? Message { get; set; }
    }
}
