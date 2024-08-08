using Newtonsoft.Json;
using System;

public class NullCharacterConverter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(string);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.Null)
        {
            return null;
        }
        else if (reader.TokenType == JsonToken.String)
        {
            var value = (string)reader.Value;
            value = value.Replace("\0", string.Empty); // Remove null characters
            return value;
        }

        throw new JsonSerializationException("Unexpected token type: " + reader.TokenType);
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }
}
