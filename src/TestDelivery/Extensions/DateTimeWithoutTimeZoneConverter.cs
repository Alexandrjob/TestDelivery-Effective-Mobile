using System.Text.Json;
using System.Text.Json.Serialization;

namespace TestDelivery.Extensions;

public class DateTimeWithoutTimeZoneConverter : JsonConverter<DateTime>
{
    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString("yyyy-MM-dd HH:mm:ss"));
    }

    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            var dateTimeString = reader.GetString();
            return DateTime.Parse(dateTimeString);
        }
        throw new JsonException();
    }
}