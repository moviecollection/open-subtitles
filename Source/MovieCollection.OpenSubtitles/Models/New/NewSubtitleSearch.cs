namespace MovieCollection.OpenSubtitles.Models
{
    using System.Collections.Generic;

    public class NewSubtitleSearch
    {
        /// <summary>
        /// Gets or sets the ai translated value.
        /// </summary>
        /// <remarks>
        /// exclude, include (default: exclude).
        /// </remarks>
        public string? AiTranslated { get; set; }

        /// <summary>
        /// Gets or sets the tv show episode number.
        /// </summary>
        /// <remarks>
        /// For TV Shows.
        /// </remarks>
        public int? EpisodeNumber { get; set; }

        /// <summary>
        /// Gets or sets the foreign parts only value.
        /// </summary>
        /// <remarks>
        /// include, only (default: include).
        /// </remarks>
        public string? ForeignPartsOnly { get; set; }

        /// <summary>
        /// Gets or sets the hearing impaired value.
        /// </summary>
        /// <remarks>
        /// include, exclude, only. (default: include).
        /// </remarks>
        public string? HearingImpaired { get; set; }

        /// <summary>
        /// Gets or sets the id of the movie or episode.
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// Gets or sets the imdb id of the movie or episode.
        /// </summary>
        public string? ImdbId { get; set; }

        /// <summary>
        /// Gets or sets the language codes (e.g. en, fr).
        /// </summary>
        public IList<string>? Languages { get; set; }

        /// <summary>
        /// Gets or sets the machine translated value.
        /// </summary>
        /// <remarks>
        /// exclude, include (default: exclude).
        /// </remarks>
        public string? MachineTranslated { get; set; }

        /// <summary>
        /// Gets or sets the hash of the movie.
        /// </summary>
        public string? MovieHash { get; set; }

        /// <summary>
        /// Gets or sets the movie hash match value.
        /// </summary>
        /// <remarks>
        /// include, only (default: include).
        /// </remarks>
        public string? MovieHashMatch { get; set; }

        /// <summary>
        /// Gets or sets thr order of the returned results, accepts any of above fields.
        /// </summary>
        public string? OrderBy { get; set; }

        /// <summary>
        /// Gets or sets the order direction of the returned results (asc, desc).
        /// </summary>
        public string? OrderDirection { get; set; }

        /// <summary>
        /// Gets or sets the results page to display.
        /// </summary>
        public int? Page { get; set; }

        /// <summary>
        /// Gets or sets the parent feature id.
        /// </summary>
        /// <remarks>
        /// For Tvshows.
        /// </remarks>
        public int? ParentFeatureId { get; set; }

        /// <summary>
        /// Gets or sets the parent imdb id.
        /// </summary>
        /// <remarks>
        /// For Tvshows.
        /// </remarks>
        public string? ParentImdbId { get; set; }

        /// <summary>
        /// Gets or sets the parent tmdb id.
        /// </summary>
        /// <remarks>
        /// For Tvshows.
        /// </remarks>
        public string? ParentTmdbId { get; set; }

        /// <summary>
        /// Gets or sets the filename or search text.
        /// </summary>
        public string? Query { get; set; }

        /// <summary>
        /// Gets or sets the tv show season number.
        /// </summary>
        /// <remarks>
        /// For TV Shows.
        /// </remarks>
        public int? SeasonNumber { get; set; }

        /// <summary>
        /// Gets or sets the tmdb id of the movie or episode.
        /// </summary>
        public int? TmdbId { get; set; }

        /// <summary>
        /// Gets or sets the trusted sources value.
        /// </summary>
        /// <remarks>
        /// include, only (default: include).
        /// </remarks>
        public string? TrustedSources { get; set; }

        /// <summary>
        /// Gets or sets the type value.
        /// </summary>
        /// <remarks>
        /// movie, episode or all, (default: all).
        /// </remarks>
        public string? Type { get; set; }

        /// <summary>
        /// Gets or sets the user id for user uploads listing (To be used alone).
        /// </summary>
        public int? UserId { get; set; }

        /// <summary>
        /// Gets or sets the movie or episode year filter.
        /// </summary>
        public int? Year { get; set; }
    }
}
