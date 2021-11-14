namespace MovieCollection.OpenSubtitles.Models
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class Subtitle
    {
        [JsonProperty("subtitle_id")]
        public int SubtitleId { get; set; }

        [JsonProperty("language")]
        public string? Language { get; set; }

        [JsonProperty("download_count")]
        public long DownloadCount { get; set; }

        [JsonProperty("new_download_count")]
        public long NewDownloadCount { get; set; }

        [JsonProperty("hearing_impaired")]
        public bool HearingImpaired { get; set; }

        [JsonProperty("hd")]
        public bool Hd { get; set; }

        [JsonProperty("format")]
        public string? Format { get; set; }

        [JsonProperty("fps")]
        public string? Fps { get; set; }

        [JsonProperty("votes")]
        public string? Votes { get; set; }

        [JsonProperty("points")]
        public string? Points { get; set; }

        [JsonProperty("ratings")]
        public string? Ratings { get; set; }

        [JsonProperty("from_trusted")]
        public bool? FromTrusted { get; set; }

        [JsonProperty("foreign_parts_only")]
        public bool ForeignPartsOnly { get; set; }

        [JsonProperty("ai_translated")]
        public bool AiTranslated { get; set; }

        [JsonProperty("machine_translated")]
        public bool? MachineTranslated { get; set; }

        [JsonProperty("upload_date")]
        public DateTimeOffset UploadDate { get; set; }

        [JsonProperty("release")]
        public string? Release { get; set; }

        [JsonProperty("comments")]
        public string? Comments { get; set; }

        [JsonProperty("legacy_subtitle_id")]
        public int? LegacySubtitleId { get; set; }

        [JsonProperty("uploader")]
        public Uploader? Uploader { get; set; }

        [JsonProperty("feature_details")]
        public FeatureDetails? FeatureDetails { get; set; }

        [JsonProperty("url")]
        public Uri? Url { get; set; }

        [JsonProperty("related_links")]
        [JsonConverter(typeof(InconsistentConverter<RelatedLink>))]
        public List<RelatedLink>? RelatedLinks { get; set; }

        [JsonProperty("files")]
        public List<SubtitleFile>? Files { get; set; }

        [JsonProperty("moviehash_match")]
        public bool? MovieHashMatch { get; set; }
    }
}
