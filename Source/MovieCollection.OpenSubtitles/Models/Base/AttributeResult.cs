namespace MovieCollection.OpenSubtitles.Models
{
    using Newtonsoft.Json;

    public class AttributeResult<T>
        where T : class
    {
        [JsonProperty("id")]
        public string? Id { get; set; }

        [JsonProperty("type")]
        public string? Type { get; set; }

        [JsonProperty("attributes")]
        public T? Attributes { get; set; }
    }
}
