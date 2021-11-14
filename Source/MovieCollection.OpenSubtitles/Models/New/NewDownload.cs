namespace MovieCollection.OpenSubtitles.Models
{
    using Newtonsoft.Json;

    public class NewDownload
    {
        /// <summary>
        /// Gets or sets the file id.
        /// </summary>
        [JsonProperty("file_id")]
        public int FileId { get; set; }

        /// <summary>
        /// Gets or sets the subtitle format.
        /// </summary>
        /// <remarks>
        /// From <see cref="OpenSubtitlesService.GetSubtitleFormatsAsync"/>.
        /// </remarks>
        [JsonProperty("sub_format")]
        public string? SubFormat { get; set; }

        /// <summary>
        /// Gets or sets the desired file name.
        /// </summary>
        [JsonProperty("file_name")]
        public string? FileName { get; set; }

        /// <summary>
        /// Gets or sets in-fps for conversions.
        /// </summary>
        /// <remarks>
        /// The <see cref="InFps"/> and <see cref="OutFps"/> must then be indicated.
        /// </remarks>
        [JsonProperty("in_fps")]
        public int? InFps { get; set; }

        /// <summary>
        /// Gets or sets out-fps for conversions.
        /// </summary>
        /// <remarks>
        /// The <see cref="InFps"/> and <see cref="OutFps"/> must then be indicated.
        /// </remarks>
        [JsonProperty("out_fps")]
        public int? OutFps { get; set; }

        /// <summary>
        /// Gets or sets the time shift.
        /// </summary>
        [JsonProperty("timeshift")]
        public int? TimeShift { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether subtitle file headers
        /// should be set to "application/force-download".
        /// </summary>
        [JsonProperty("force_download")]
        public bool? ForceDownload { get; set; }
    }
}
