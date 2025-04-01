using System.Text.Json.Serialization;

namespace JacquesMurray.ProwlarrApiClient.Models
{
    /// <summary>
    /// Represents the system status information returned by the Prowlarr API.
    /// Corresponds to the response from GET /api/v1/system/status.
    /// </summary>
    public class SystemStatus
    {
        /// <summary>Gets or sets the application name (e.g., "Prowlarr").</summary>
        [JsonPropertyName("appName")]
        public string? AppName { get; set; }

        /// <summary>Gets or sets the instance name configured by the user.</summary>
        [JsonPropertyName("instanceName")]
        public string? InstanceName { get; set; }

        /// <summary>Gets or sets the application version (e.g., "1.0.0.1234").</summary>
        [JsonPropertyName("version")]
        public string? Version { get; set; }

        /// <summary>Gets or sets the date and time the application was built.</summary>
        [JsonPropertyName("buildTime")]
        public DateTimeOffset BuildTime { get; set; }

        /// <summary>Gets or sets a value indicating whether the application is running in debug mode.</summary>
        [JsonPropertyName("isDebug")]
        public bool IsDebug { get; set; }

        /// <summary>Gets or sets a value indicating whether the application is running in production mode.</summary>
        [JsonPropertyName("isProduction")]
        public bool IsProduction { get; set; }

        /// <summary>Gets or sets a value indicating whether the current API key grants administrator privileges.</summary>
        [JsonPropertyName("isAdmin")]
        public bool IsAdmin { get; set; }

        /// <summary>Gets or sets a value indicating whether the application requires user interaction (relevant for certain environments).</summary>
        [JsonPropertyName("isUserInteractive")]
        public bool IsUserInteractive { get; set; }

        /// <summary>Gets or sets the directory from which the application was started.</summary>
        [JsonPropertyName("startupPath")]
        public string? StartupPath { get; set; }

        /// <summary>Gets or sets the directory where application data (database, logs) is stored.</summary>
        [JsonPropertyName("appData")]
        public string? AppData { get; set; }

        /// <summary>Gets or sets the name of the operating system (e.g., "Windows", "Linux").</summary>
        [JsonPropertyName("osName")]
        public string? OsName { get; set; }

        /// <summary>Gets or sets the version of the operating system.</summary>
        [JsonPropertyName("osVersion")]
        public string? OsVersion { get; set; }

        /// <summary>Gets or sets a value indicating whether the runtime is .NET Core / .NET 5+.</summary>
        /// <remarks>The API property name might be `isNetCore` but reflects the modern .NET runtime.</remarks>
        [JsonPropertyName("isNetCore")]
        public bool IsNetCore { get; set; }

        /// <summary>Gets or sets a value indicating whether the operating system is Linux.</summary>
        [JsonPropertyName("isLinux")]
        public bool IsLinux { get; set; }

        /// <summary>Gets or sets a value indicating whether the operating system is macOS.</summary>
        [JsonPropertyName("isOsx")]
        public bool IsOsx { get; set; }

        /// <summary>Gets or sets a value indicating whether the operating system is Windows.</summary>
        [JsonPropertyName("isWindows")]
        public bool IsWindows { get; set; }

        /// <summary>Gets or sets a value indicating whether the application is running inside a Docker container.</summary>
        [JsonPropertyName("isDocker")]
        public bool IsDocker { get; set; }

        /// <summary>Gets or sets the runtime mode (e.g., "Console", "Service").</summary>
        [JsonPropertyName("mode")]
        public string? Mode { get; set; }

        /// <summary>Gets or sets the Git branch the application was built from (e.g., "main", "develop").</summary>
        [JsonPropertyName("branch")]
        public string? Branch { get; set; }

        /// <summary>Gets or sets the configured authentication method (e.g., "None", "Forms", "Basic").</summary>
        [JsonPropertyName("authentication")]
        public string? Authentication { get; set; }

        /// <summary>Gets or sets the version of the SQLite library being used.</summary>
        [JsonPropertyName("sqliteVersion")]
        public string? SqliteVersion { get; set; }

        /// <summary>Gets or sets the current database schema migration version.</summary>
        [JsonPropertyName("migrationVersion")]
        public int MigrationVersion { get; set; }

        /// <summary>Gets or sets the configured URL base for the application.</summary>
        [JsonPropertyName("urlBase")]
        public string? UrlBase { get; set; }

        /// <summary>Gets or sets the version of the .NET runtime executing the application (e.g., "6.0.1").</summary>
        [JsonPropertyName("runtimeVersion")]
        public string? RuntimeVersion { get; set; }

        /// <summary>Gets or sets the name of the .NET runtime (e.g., ".NET Core").</summary>
        [JsonPropertyName("runtimeName")]
        public string? RuntimeName { get; set; }

        /// <summary>Gets or sets the time the application process started.</summary>
        [JsonPropertyName("startTime")]
        public DateTimeOffset StartTime { get; set; }

        /// <summary>Gets or sets the version of the installation package, if applicable.</summary>
        [JsonPropertyName("packageVersion")]
        public string? PackageVersion { get; set; }

        /// <summary>Gets or sets the author of the installation package, if applicable.</summary>
        [JsonPropertyName("packageAuthor")]
        public string? PackageAuthor { get; set; }

        /// <summary>Gets or sets the mechanism used for package updates (e.g., "BuiltIn", "Docker").</summary>
        [JsonPropertyName("packageUpdateMechanism")]
        public string? PackageUpdateMechanism { get; set; }

        /// <summary>Gets or sets the date of the last successful update check.</summary>
        [JsonPropertyName("updateDate")]
        public DateTimeOffset? UpdateDate { get; set; } // Can be null if no update checked/found

        /// <summary>Gets or sets the version of the available update package.</summary>
        [JsonPropertyName("updatePackageVersion")]
        public string? UpdatePackageVersion { get; set; } // Can be null

        /// <summary>Gets or sets the author of the available update package.</summary>
        [JsonPropertyName("updatePackageAuthor")]
        public string? UpdatePackageAuthor { get; set; } // Can be null

        /// <summary>Gets or sets a value indicating whether an update is available.</summary>
        [JsonPropertyName("updateAvailable")]
        public bool UpdateAvailable { get; set; }
    }
}