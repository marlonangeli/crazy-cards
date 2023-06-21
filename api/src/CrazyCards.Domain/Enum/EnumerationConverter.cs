using CrazyCards.Domain.Enum.Shared;
using Newtonsoft.Json;

namespace CrazyCards.Domain.Enum;

public sealed class EnumerationConverter<T> : JsonConverter<T> where T : Enumeration<T>
{
    /// <inheritdoc />
    public override void WriteJson(JsonWriter writer, T? value, JsonSerializer serializer)
    {
        writer.WriteStartObject();
        writer.WritePropertyName(nameof(value.Value).ToLower());
        writer.WriteValue(value!.Value);
        writer.WritePropertyName(nameof(value.Name).ToLower());
        writer.WriteValue(value.Name);
        writer.WriteEndObject();
    }

    /// <inheritdoc />
    public override T? ReadJson(JsonReader reader, Type objectType, T? existingValue, bool hasExistingValue,
        JsonSerializer serializer)
    {
        while (reader.TokenType != JsonToken.Integer) reader.Read();

        int value = Convert.ToInt32(reader.Value);

        while (reader.TokenType != JsonToken.EndObject) reader.Read();

        return Enumeration<T>.FromValue(value) ??
               throw new JsonSerializationException($"Invalid {typeof(T).Name} value: {value}");
    }
}