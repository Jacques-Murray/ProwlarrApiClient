using System.Text.Json;
using System.Text.Json.Serialization;

namespace JacquesMurray.ProwlarrApiClient.Models
{
    /// <summary>
    /// Represents an indexer configuration in Prowlarr.
    /// Corresponds to items in the response from /api/v1/indexer and the request/response for POST/PUT/GET by ID.
    /// </summary>
    public class Indexer
    {
        /// <summary>Gets or sets the unique ID of the indexer (0 for new indexers).</summary>
        [JsonPropertyName("id")]
        public int Id { get; set; }

        /// <summary>Gets or sets the name of the indexer.</summary>
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        /// <summary>Gets or sets the implementation type (e.g., "TorrentRssParser").</summary>
        [JsonPropertyName("implementation")]
        public string? Implementation { get; set; }

        /// <summary>Gets or sets the configuration contract (e.g., "UsenetIndexerSettings").</summary>
        [JsonPropertyName("configContract")]
        public string? ConfigContract { get; set; }

        /// <summary>Gets or sets a value indicating whether this indexer is enabled.</summary>
        [JsonPropertyName("enable")]
        public bool Enable { get; set; } = true; // Default to enabled

        /// <summary>Gets or sets the protocol (e.g., "usenet", "torrent").</summary>
        [JsonPropertyName("protocol")]
        public string? Protocol { get; set; } // Consider an enum: "unknown", "usenet", "torrent"

        /// <summary>Gets or sets the priority level.</summary>
        [JsonPropertyName("priority")]
        public int Priority { get; set; } = 25; // Default priority

        /// <summary>Gets or sets the list of assigned tags.</summary>
        [JsonPropertyName("tags")]
        public List<int>? Tags { get; set; } = new List<int>();

        /// <summary>Gets or sets the indexer-specific settings fields.</summary>
        /// <remarks>
        /// This is dynamic based on the indexer type. Using JsonElement allows flexibility,
        /// but requires careful handling or defining specific settings classes per indexer type.
        /// For simplicity here, we use JsonElement.
        /// </remarks>
        [JsonPropertyName("fields")]
        public List<IndexerField>? Fields { get; set; } = new List<IndexerField>();

        // --- Add other common properties as needed based on API docs ---
        // Example:
        // [JsonPropertyName("supportsRss")]
        // public bool? SupportsRss { get; set; }

        // [JsonPropertyName("supportsSearch")]
        // public bool? SupportsSearch { get; set; }

        /// <summary>Gets or sets the definition name (preset name).</summary>
        [JsonPropertyName("definitionName")]
        public string? DefinitionName { get; set; }
    }

    /// <summary>
    /// Represents a single configuration field for an indexer.
    /// </summary>
    public class IndexerField
    {
        /// <summary>Gets or sets the name of the field.</summary>
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        /// <summary>Gets or sets the value of the field.</summary>
        /// <remarks>Can be string, number, boolean, or null.</remarks>
        [JsonPropertyName("value")]
        public JsonElement Value { get; set; } // Use JsonElement to handle various types
    }
}