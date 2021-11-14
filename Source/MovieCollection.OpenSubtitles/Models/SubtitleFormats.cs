namespace MovieCollection.OpenSubtitles.Models
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class SubtitleFormats
    {
        [JsonProperty("output_formats")]
        public List<string>? OutputFormats { get; set; }
    }
}
