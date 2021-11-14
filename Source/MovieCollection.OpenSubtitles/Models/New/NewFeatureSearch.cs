namespace MovieCollection.OpenSubtitles.Models
{
    public class NewFeatureSearch
    {
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string? Query { get; set; }

        /// <summary>
        /// Gets or sets the feature id.
        /// </summary>
        public int? FeatureId { get; set; }

        /// <summary>
        /// Gets or sets the imdb id.
        /// </summary>
        public string? ImdbId { get; set; }

        /// <summary>
        /// Gets or sets the tmdb id.
        /// </summary>
        /// <remarks>
        /// Should be set with the <see cref="Type"/> parameter to avoid conflicts.
        /// </remarks>
        public int? TmdbId { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <remarks>
        /// Set empty to list all.
        /// Should be: "movie", "tvshow" or "episode".
        /// </remarks>
        public string? Type { get; set; }

        /// <summary>
        /// Gets or sets the year.
        /// </summary>
        /// <remarks>
        /// Can only be used in combination with a <see cref="Query"/>.
        /// </remarks>
        public int? Year { get; set; }
    }
}
