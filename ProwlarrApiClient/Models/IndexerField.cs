using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace JacquesMurray.ProwlarrApiClient.Models
{
    /// <summary>
    /// Represents a single configuration field for an indexer or application.
    /// Contains a name and a value whose type can vary.
    /// </summary>
    public class FieldBase
    {
        /// <summary>Gets or sets the name of the configuration field (e.g., "ApiKey", "BaseUrl").</summary>
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        /// <summary>Gets or sets the raw JSON value of the configuration field.</summary>
        /// <remarks>
        /// Use the helper methods (e.g., GetStringValue, GetIntValue) for type-safe access.
        /// Can represent JSON strings, numbers, booleans, arrays, or objects.
        /// </remarks>
        [JsonPropertyName("value")]
        public JsonElement Value { get; set; }

        /// <summary>
        /// Attempts to get the field value as a string.
        /// </summary>
        /// <returns>The string value, or null if the value is not a string or is null.</returns>
        public string? GetStringValue() => Value.ValueKind == JsonValueKind.String ? Value.GetString() : null;

        /// <summary>
        /// Attempts to get the field value as an integer.
        /// </summary>
        /// <returns>The integer value, or null if the value cannot be represented as an int.</returns>
        public int? GetIntValue() => Value.TryGetInt32(out int intValue) ? intValue : null;

        /// <summary>
        /// Attempts to get the field value as a boolean.
        /// </summary>
        /// <returns>The boolean value, or null if the value is not a boolean.</returns>
        public bool? GetBooleanValue() => Value.ValueKind switch
        {
            JsonValueKind.True => true,
            JsonValueKind.False => false,
            _ => null
        };
    }

    /// <summary>
    /// Represents a configuration field specifically for an Indexer. Inherits from FieldBase.
    /// </summary>
    public class IndexerField : FieldBase { }

    /// <summary>
    /// Represents a configuration field specifically for an Application. Inherits from FieldBase.
    /// </summary>
    public class ApplicationField : FieldBase { }
}
