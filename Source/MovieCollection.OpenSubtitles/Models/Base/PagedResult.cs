namespace MovieCollection.OpenSubtitles.Models
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class PagedResult<T>
        where T : class
    {
        [JsonProperty("total_pages")]
        public int TotalPages { get; set; }

        [JsonProperty("total_count")]
        public int TotalCount { get; set; }

        [JsonProperty("page")]
        public int Page { get; set; }

        [JsonProperty("data")]
        public List<T>? Data { get; set; }
    }
}
