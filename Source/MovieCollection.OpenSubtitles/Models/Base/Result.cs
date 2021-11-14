namespace MovieCollection.OpenSubtitles.Models
{
    using Newtonsoft.Json;

    public class Result<T>
        where T : class
    {
        [JsonProperty("data")]
        public T? Data { get; set; }
    }
}
