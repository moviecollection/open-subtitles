namespace MovieCollection.OpenSubtitles.Models
{
    using Newtonsoft.Json;

    public class SubtitleFile
    {
        [JsonProperty("file_id")]
        public int FileId { get; set; }

        [JsonProperty("cd_number")]
        public int CdNumber { get; set; }

        [JsonProperty("file_name")]
        public string? FileName { get; set; }
    }
}
