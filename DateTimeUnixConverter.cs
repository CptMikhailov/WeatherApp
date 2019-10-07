using System;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WeatherApp
{
    // Custom JSON Converter that handles Unix timestamps
    public class DateTimeUnixConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            Debug.Assert(typeToConvert == typeof(DateTime));
            return DateTime.SpecifyKind(DateTime.UnixEpoch.AddSeconds(reader.GetInt64()), DateTimeKind.Utc);
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteNumberValue((value.ToFileTime() - DateTime.UnixEpoch.ToFileTime()) / (Math.Pow(10, 9)));
        }
    }
}