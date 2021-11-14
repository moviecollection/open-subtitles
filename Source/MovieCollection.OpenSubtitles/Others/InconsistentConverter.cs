namespace MovieCollection.OpenSubtitles
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Converts the inconsistent result sets.
    /// </summary>
    /// <typeparam name="T">The type of the object to deserialize.</typeparam>
    internal class InconsistentConverter<T> : JsonConverter
        where T : class
    {
        /// <inheritdoc/>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.StartObject)
            {
                var obj = serializer.Deserialize<T>(reader);
                return new List<T>() { obj };
            }
            else if (reader.TokenType == JsonToken.StartArray)
            {
                return serializer.Deserialize<List<T>>(reader);
            }

            return new List<T>();
        }

        /// <inheritdoc/>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public override bool CanConvert(Type objectType)
        {
            return true;
        }
    }
}
