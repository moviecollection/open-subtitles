namespace MovieCollection.OpenSubtitles.Models
{
    using System;
    using Newtonsoft.Json;

    public class UserDetails : User
    {
        [JsonProperty("reset_time_utc")]
        public DateTimeOffset ResetTimeUtc { get; set; }

        [JsonProperty("reset_time")]
        public string? ResetTime { get; set; }

        [JsonProperty("downloads_count")]
        public int DownloadsCount { get; set; }

        [JsonProperty("remaining_downloads")]
        public int RemainingDownloads { get; set; }
    }
}
