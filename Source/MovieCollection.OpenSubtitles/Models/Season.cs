namespace MovieCollection.OpenSubtitles.Models
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class Season
    {
        [JsonProperty("season_number")]
        public int SeasonNumber { get; set; }

        [JsonProperty("episodes")]
        public List<Episode>? Episodes { get; set; }
    }
}
