namespace MovieCollection.OpenSubtitles
{
    using System.Net.Http.Headers;

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

        /// <summary>
        /// Gets or sets the name (and version) of the product using this library.
        /// This will be sent to the server as part of user agent for diagnostic purposes.
        /// </summary>
        /// <remarks>
        /// Not required if you set the user agent via the <see cref="System.Net.Http.HttpClient.DefaultRequestHeaders"/>.
        /// </remarks>
        public ProductHeaderValue? ProductInformation { get; set; }
    }
}
