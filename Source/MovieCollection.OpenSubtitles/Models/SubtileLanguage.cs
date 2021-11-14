namespace MovieCollection.OpenSubtitles.Models
{
    using Newtonsoft.Json;

    public class SubtileLanguage
    {
        [JsonProperty("language_code")]
        public string? LanguageCode { get; set; }

        [JsonProperty("language_name")]
        public string? LanguageName { get; set; }
    }
}
