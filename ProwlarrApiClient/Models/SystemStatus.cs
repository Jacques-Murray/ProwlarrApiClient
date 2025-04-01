using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace JacquesMurray.ProwlarrApiClient.Models
{
    /// <summary>
    /// Represents the system status information returned by the Prowlarr API.
    /// Corresponds to the response from /api/v1/system/status.
    /// </summary>
    public class SystemStatus
    {
        /// <summary>Gets or sets the application name.</summary>
        [JsonPropertyName("appName")]
        public string? AppName { get; set; }

        /// <summary>Gets or sets the instance name.</summary>
        [JsonPropertyName("instanceName")]
        public string? InstanceName { get; set; }

        /// <summary>Gets or sets the application version.</summary>
        [JsonPropertyName("version")]
        public string? Version { get; set; }

        /// <summary>Gets or sets the build time.</summary>
        [JsonPropertyName("buildTime")]
        public DateTimeOffset BuildTime { get; set; }

        /// <summary>Gets or sets a value indicating whether debugging is enabled.</summary>
        [JsonPropertyName("isDebug")]
        public bool IsDebug { get; set; }

        /// <summary>Gets or sets a value indicating whether production mode is enabled.</summary>
        [JsonPropertyName("isProduction")]
        public bool IsProduction { get; set; }

        /// <summary>Gets or sets a value indicating whether the user is an administrator.</summary>
        [JsonPropertyName("isAdmin")]
        public bool IsAdmin { get; set; }

        /// <summary>Gets or sets a value indicating whether authentication is enabled.</summary>
        [JsonPropertyName("isUserInteractive")]
        public bool IsUserInteractive { get; set; }

        /// <summary>Gets or sets the startup path.</summary>
        [JsonPropertyName("startupPath")]
        public string? StartupPath { get; set; }

        /// <summary>Gets or sets the application data path.</summary>
        [JsonPropertyName("appData")]
        public string? AppData { get; set; }

        /// <summary>Gets or sets the OS name.</summary>
        [JsonPropertyName("osName")]
        public string? OsName { get; set; }

        /// <summary>Gets or sets the OS version.</summary>
        [JsonPropertyName("osVersion")]
        public string? OsVersion { get; set; }

        /// <summary>Gets or sets a value indicating whether the OS is 64-bit.</summary>
        [JsonPropertyName("isNetCore")] // Note: API might label this based on runtime
        public bool IsNetCore { get; set; }

        /// <summary>Gets or sets a value indicating whether the OS is Linux.</summary>
        [JsonPropertyName("isLinux")]
        public bool IsLinux { get; set; }

        /// <summary>Gets or sets a value indicating whether the OS is macOS.</summary>
        [JsonPropertyName("isOsx")]
        public bool IsOsx { get; set; }

        /// <summary>Gets or sets a value indicating whether the OS is Windows.</summary>
        [JsonPropertyName("isWindows")]
        public bool IsWindows { get; set; }

        /// <summary>Gets or sets a value indicating whether the OS is Docker.</summary>
        [JsonPropertyName("isDocker")]
        public bool IsDocker { get; set; }

        /// <summary>Gets or sets the runtime mode.</summary>
        [JsonPropertyName("mode")]
        public string? Mode { get; set; }

        /// <summary>Gets or sets the current branch.</summary>
        [JsonPropertyName("branch")]
        public string? Branch { get; set; }

        /// <summary>Gets or sets the authentication method.</summary>
        [JsonPropertyName("authentication")]
        public string? Authentication { get; set; }

        /// <summary>Gets or sets the database version.</summary>
        [JsonPropertyName("sqliteVersion")]
        public string? SqliteVersion { get; set; }

        /// <summary>Gets or sets the migration version.</summary>
        [JsonPropertyName("migrationVersion")]
        public int MigrationVersion { get; set; }

        /// <summary>Gets or sets the URL base.</summary>
        [JsonPropertyName("urlBase")]
        public string? UrlBase { get; set; }

        /// <summary>Gets or sets the runtime version.</summary>
        [JsonPropertyName("runtimeVersion")]
        public string? RuntimeVersion { get; set; }

        /// <summary>Gets or sets the runtime name.</summary>
        [JsonPropertyName("runtimeName")]
        public string? RuntimeName { get; set; }

        /// <summary>Gets or sets the start time.</summary>
        [JsonPropertyName("startTime")]
        public DateTimeOffset StartTime { get; set; }

        /// <summary>Gets or sets the package version.</summary>
        [JsonPropertyName("packageVersion")]
        public string? PackageVersion { get; set; }

        /// <summary>Gets or sets the package author.</summary>
        [JsonPropertyName("packageAuthor")]
        public string? PackageAuthor { get; set; }

        /// <summary>Gets or sets the package update mechanism.</summary>
        [JsonPropertyName("packageUpdateMechanism")]
        public string? PackageUpdateMechanism { get; set; }

        /// <summary>Gets or sets update information.</summary>
        [JsonPropertyName("updateDate")]
        public DateTimeOffset? UpdateDate { get; set; } // Can be null

        /// <summary>Gets or sets update package information.</summary>
        [JsonPropertyName("updatePackageVersion")]
        public string? UpdatePackageVersion { get; set; } // Can be null

        /// <summary>Gets or sets update package information.</summary>
        [JsonPropertyName("updatePackageAuthor")]
        public string? UpdatePackageAuthor { get; set; } // Can be null

        /// <summary>Gets or sets update information.</summary>
        [JsonPropertyName("updateAvailable")]
        public bool UpdateAvailable { get; set; }
    }
}