using System.Text.Json.Serialization;

namespace JacquesMurray.ProwlarrApiClient.Models
{
    /// <summary>
    /// Defines the protocol used by an indexer.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum IndexerProtocol
    {
        /// <summary>Protocol is unknown or not applicable.</summary>
        Unknown,
        /// <summary>Indexer uses the Usenet protocol.</summary>
        Usenet,
        /// <summary>Indexer uses the BitTorrent protocol.</summary>
        Torrent
    }

    /// <summary>
    /// Represents an indexer configuration in Prowlarr.
    /// Corresponds to items in the response from /api/v1/indexer and the request/response for POST/PUT/GET by ID.
    /// </summary>
    public class Indexer
    {
        /// <summary>Gets or sets the unique ID of the indexer. Set to 0 when adding a new indexer.</summary>
        [JsonPropertyName("id")]
        public int Id { get; set; }

        /// <summary>Gets or sets the user-defined name of the indexer.</summary>
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        /// <summary>Gets or sets the Prowlarr implementation type used for this indexer (e.g., "TorrentRssParser", "Newznab").</summary>
        [JsonPropertyName("configContract")]
        public string? ConfigContract { get; set; }

        /// <summary>Gets or sets a value indicating whether this indexer is enabled and active.</summary>
        [JsonPropertyName("enable")]
        public bool Enable { get; set; } = true;

        /// <summary>Gets or sets the communication protocol used by the indexer.</summary>
        [JsonPropertyName("protocol")]
        public IndexerProtocol Protocol { get; set; } = IndexerProtocol.Unknown;

        /// <summary>Gets or sets the priority level of the indexer (lower value means higher priority).</summary>
        [JsonPropertyName("priority")]
        public int Priority { get; set; } = 25;

        /// <summary>Gets or sets the list of tag IDs assigned to this indexer.</summary>
        [JsonPropertyName("tags")]
        public List<int> Tags { get; set; } = new List<int>();

        /// <summary>Gets or sets the indexer-specific configuration fields.</summary>
        /// <remarks>Use helper methods on individual <see cref="IndexerField"/> objects to access typed values.</remarks>
        [JsonPropertyName("fields")]
        public List<IndexerField> Fields { get; set; } = new List<IndexerField>();
    }
}