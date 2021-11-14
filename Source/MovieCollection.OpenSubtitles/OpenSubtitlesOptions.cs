namespace MovieCollection.OpenSubtitles
{
    /// <summary>
    /// The <see cref="OpenSubtitlesOptions"/> class.
    /// </summary>
    public class OpenSubtitlesOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OpenSubtitlesOptions"/> class.
        /// </summary>
        public OpenSubtitlesOptions()
        {
            ApiAddress = "https://api.opensubtitles.com";
            ApiKey = string.Empty;
        }

        /// <summary>
        /// Gets or sets the api address.
        /// </summary>
        public string ApiAddress { get; set; }

        /// <summary>
        /// Gets or sets the api key.
        /// </summary>
        public string ApiKey { get; set; }
    }
}
