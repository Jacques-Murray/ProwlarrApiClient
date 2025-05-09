﻿using System.Text.Json;
using System.Text.Json.Serialization;

namespace JacquesMurray.ProwlarrApiClient.Models
{
    /// <summary>
    /// Represents an application Prowlarr can sync indexers to.
    /// Corresponds to items in the response from /api/v1/application.
    /// </summary>
    public class Application
    {
        /// <summary>Gets or sets the unique ID of the application.</summary>
        [JsonPropertyName("id")]
        public int Id { get; set; }

        /// <summary>Gets or sets the name of the application.</summary>
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        /// <summary>Gets or sets the implementation type (e.g., "Radarr").</summary>
        [JsonPropertyName("implementation")]
        public string? Implementation { get; set; }

        /// <summary>Gets or sets the configuration contract (e.g., "RadarrSettings").</summary>
        [JsonPropertyName("configContract")]
        public string? ConfigContract { get; set; }

        /// <summary>Gets or sets a value indicating whether indexer sync is enabled.</summary>
        [JsonPropertyName("syncLevel")]
        public string? SyncLevel { get; set; } // e.g., "addOnly", "fullSync"

        /// <summary>Gets or sets the list of assigned tags.</summary>
        [JsonPropertyName("tags")]
        public List<int>? Tags { get; set; } = new List<int>();

        /// <summary>Gets or sets the application-specific settings fields.</summary>
        [JsonPropertyName("fields")]
        public List<ApplicationField>? Fields { get; set; } = new List<ApplicationField>();

        // --- Add other common properties as needed based on API docs ---
    }

}