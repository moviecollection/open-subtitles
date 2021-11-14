namespace MovieCollection.OpenSubtitles.Models
{
    using System;
    using Newtonsoft.Json;

    public class RelatedLink
    {
        [JsonProperty("label")]
        public string? Label { get; set; }

        [JsonProperty("url")]
        public Uri? Url { get; set; }

        [JsonProperty("img_url")]
        public Uri? ImgUrl { get; set; }
    }
}
