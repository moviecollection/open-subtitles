namespace MovieCollection.OpenSubtitles.Models
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class Guessit
    {
        [JsonProperty("title")]
        public string? Title { get; set; }

        [JsonProperty("year")]
        public int? Year { get; set; }

        [JsonProperty("season")]
        public int? Season { get; set; }

        [JsonProperty("episode")]
        public int? Episode { get; set; }

        [JsonProperty("episode_title")]
        public string? EpisodeTitle { get; set; }

        [JsonProperty("episode_details")]
        public List<string>? EpisodeDetails { get; set; }

        [JsonProperty("language")]
        public string? Language { get; set; }

        [JsonProperty("subtitle_language")]
        public string? SubtitleLanguage { get; set; }

        [JsonProperty("screen_size")]
        public string? ScreenSize { get; set; }

        [JsonProperty("date")]
        public string? Date { get; set; }

        [JsonProperty("size")]
        public string? Size { get; set; }

        [JsonProperty("streaming_service")]
        public string? StreamingService { get; set; }

        [JsonProperty("source")]
        public string? Source { get; set; }

        [JsonProperty("other")]
        public string? Other { get; set; }

        [JsonProperty("audio_codec")]
        public string? AudioCodec { get; set; }

        [JsonProperty("audio_channels")]
        public string? AudioChannels { get; set; }

        [JsonProperty("video_codec")]
        public string? VideoCodec { get; set; }

        [JsonProperty("video_profile")]
        public string? VideoProfile { get; set; }

        [JsonProperty("color_depth")]
        public string? ColorDepth { get; set; }

        [JsonProperty("release_group")]
        public string? ReleaseGroup { get; set; }

        [JsonProperty("website")]
        public string? Website { get; set; }

        [JsonProperty("container")]
        public string? Container { get; set; }

        [JsonProperty("mimetype")]
        public string? Mimetype { get; set; }

        [JsonProperty("type")]
        public string? Type { get; set; }
    }
}
