using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SimpleExample.Infrastructure.Converters
{
    public class SafeEnumConverter<TEnum> : JsonConverter<TEnum> where TEnum : struct, Enum
    {
        public override TEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();

            if (Enum.TryParse<TEnum>(value, true, out var result))
                return result;

            var allowed = string.Join(", ", Enum.GetNames(typeof(TEnum)));
            throw new JsonException($"Invalid value '{value}'. Allowed values: {allowed}.");
        }
        public override void Write(Utf8JsonWriter writer, TEnum value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
