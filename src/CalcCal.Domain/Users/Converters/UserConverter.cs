using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CalcCal.Domain.Users.Converters
{
    public sealed class UserConverter : JsonConverter<User>
    {
        public override User Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return JsonSerializer.Deserialize<User>(ref reader, options) ??
                   throw new JsonException($"Problem on deserialization of type {typeof(User)}");
        }

        public override void Write(Utf8JsonWriter writer, User user, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WritePropertyName(nameof(User.PhoneNumber));
            JsonSerializer.Serialize(writer, user.PhoneNumber, options);

            writer.WritePropertyName(nameof(User.FirstName));
            JsonSerializer.Serialize(writer, user.FirstName, options);

            writer.WritePropertyName(nameof(User.LastName));
            JsonSerializer.Serialize(writer, user.LastName, options);

            writer.WritePropertyName(nameof(User.Username));
            JsonSerializer.Serialize(writer, user.Username, options);

            writer.WritePropertyName(nameof(User.EatenFood));
            JsonSerializer.Serialize(writer, user.EatenFood, options); // Serialize the read-only collection

            writer.WritePropertyName(nameof(User.PasswordHash));
            JsonSerializer.Serialize(writer, user.PasswordHash, options);

            writer.WritePropertyName(nameof(User.IsPhoneNumberVerified));
            JsonSerializer.Serialize(writer, user.IsPhoneNumberVerified, options);

            writer.WritePropertyName(nameof(User.CreatedAt));
            JsonSerializer.Serialize(writer, user.CreatedAt, options);

            writer.WritePropertyName(nameof(User.LastLoggedInAt));
            JsonSerializer.Serialize(writer, user.LastLoggedInAt, options);

            writer.WriteEndObject();
        }
    }
}
