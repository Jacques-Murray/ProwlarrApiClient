using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace JacquesMurray.ProwlarrApiClient
{
    /// <summary>
    /// Client for interacting with the Prowlarr API (v1).
    /// </summary>
    public class ProwlarrClient : IDisposable
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly JsonSerializerOptions _jsonSerializerOptions;
        private bool _disposed = false; // To detect redundant calls.

        /// <summary>
        /// Initializes a new instance of the <see cref="ProwlarrClient"/> class.
        /// </summary>
        /// <param name="prowlarrUrl">The base URL of your Prowlarr instance (e.g., "http://localhost:9696").</param>
        /// <param name="apiKey">Your Prowlarr API key.</param>
        /// <param name="httpClient">Optional: An existing HttpClient instance to use.</param>
        /// <exception cref="ArgumentNullException">Thrown if prowlarrUrl or apiKey is null or whitespace.</exception>
        /// <exception cref="ArgumentException">Thrown if prowlarrUrl is not a valid absolute URL.</exception>
        public ProwlarrClient(string prowlarrUrl, string apiKey, HttpClient? httpClient = null)
        {
            if (string.IsNullOrWhiteSpace(prowlarrUrl))
                throw new ArgumentNullException(nameof(prowlarrUrl));
            if (string.IsNullOrWhiteSpace(apiKey))
                throw new ArgumentNullException(nameof(apiKey));
            if (!Uri.TryCreate(prowlarrUrl, UriKind.Absolute, out var baseUri))
                throw new ArgumentException("The Prowlarr URL must be a valid absolute URL.", nameof(prowlarrUrl));

            _apiKey = apiKey;
            _httpClient = httpClient ?? new HttpClient();

            // Ensure base URL ends with a slash for correct relative URI combination.
            _httpClient.BaseAddress = baseUri.AbsoluteUri.EndsWith("/") ? baseUri : new Uri(baseUri.AbsoluteUri + "/");

            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
            // Add the API key header to all requests.
            _httpClient.DefaultRequestHeaders.Add("X-Api-Key", _apiKey);

            // Configure JSON serializer options for consistency.
            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true // Handle case differences between JSON and C# properties
            };
        }

        // --- Core Request Methods ---

        /// <summary>
        /// Sends a GET request to the specified relative API path and deserializes the response.
        /// </summary>
        /// <typeparam name="TResponse">The type to deserialize the JSON response into.</typeparam>
        /// <param name="relativePath">The relative path to the API endpoint (e.g., "api/v1/system/status").</param>
        /// <param name="cancellationToken">A token to cancel the request.</param>
        /// <returns>The deserialized response object.</returns>
        /// <exception cref="ProwlarrApiException">Thrown if the API request fails.</exception>
        private async Task<TResponse> GetAsync<TResponse>(string relativePath, CancellationToken cancellationToken = default)
        {
            return await SendRequestAsync<TResponse>(HttpMethod.Get, relativePath, null, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Sends a POST request to the specified relative API path deserializes the response.
        /// </summary>
        /// <typeparam name="TResponse">The type to deserialize the JSON response into.</typeparam>
        /// <typeparam name="TRequest">The type of the request body object.</typeparam>
        /// <param name="relativePath">The relative path of the API endpoint.</param>
        /// <param name="requestBody">The object to serialize as the JSON request body.</param>
        /// <param name="cancellationToken">A token to cancel the request.</param>
        /// <returns>The deserialized response object.</returns>
        /// <exception cref="ProwlarrApiException">Thrown if the API request fails.</exception>
        private async Task<TResponse> PostAsync<TResponse, TRequest>(string relativePath, TRequest requestBody, CancellationToken cancellationToken = default)
        {
            return await SendRequestAsync<TResponse>(HttpMethod.Post, relativePath, requestBody, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Sends a POST request with a JSON body to the specified relative API path without expecting a specific response body structure.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request body object.</typeparam>
        /// <param name="relativePath">The relative path of the API endpoint.</param>
        /// <param name="requestBody">The object to serialize as the JSON request body.</param>
        /// <param name="cancellationToken">A token to cancel the request.</param>
        /// <exception cref="ProwlarrApiException">Thrown if the API request fails.</exception>
        private async Task PostAsync<TRequest>(string relativePath, TRequest requestBody, CancellationToken cancellationToken = default)
        {
            await SendRequestAsync<object>(HttpMethod.Post, relativePath, requestBody, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Sends a PUT request with a JSON body to the specified relative API path and deserializes the response.
        /// </summary>
        /// <typeparam name="TResponse">The type to deserialize the JSON response into.</typeparam>
        /// <typeparam name="TRequest">The type of the request body object.</typeparam>
        /// <param name="relativePath">The relative path of the API endpoint.</param>
        /// <param name="requestBody">The object to serialize as the JSON request body.</param>
        /// <param name="cancellationToken">A token to cancel the request.</param>
        /// <returns>The deserialized response object.</returns>
        /// <exception cref="ProwlarrApiException">Thrown if the API request fails.</exception>
        private async Task<TResponse> PutAsync<TResponse, TRequest>(string relativePath, TRequest requestBody, CancellationToken cancellationToken = default)
        {
            return await SendRequestAsync<TResponse>(HttpMethod.Put, relativePath, requestBody, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Sends a DELETE request to the specified relative API path.
        /// </summary>
        /// <param name="relativePath">The relative path of the API endpoint.</param>
        /// <param name="cancellationToken">A token to cancel the request.</param>
        /// <exception cref="ProwlarrApiException">Thrown if the API request fails.</exception>
        private async Task DeleteAsync(string relativePath, CancellationToken cancellationToken = default)
        {
            await SendRequestAsync<object>(HttpMethod.Delete, relativePath, null, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Generic method to send an HTTP request and handle the response.
        /// </summary>
        private async Task<TResponse> SendRequestAsync<TResponse>(HttpMethod method, string relativePath, object? requestBody = null, CancellationToken cancellationToken = default)
        {
            using var request = new HttpRequestMessage(method, relativePath);

            if (requestBody != null)
            {
                try
                {
                    string jsonPayload = JsonSerializer.Serialize(requestBody, _jsonSerializerOptions);
                    request.Content = new StringContent(jsonPayload, Encoding.UTF8, MediaTypeNames.Application.Json);
                }
                catch (JsonException ex)
                {
                    throw new ProwlarrApiException($"Failed to serialize request body for {method} {relativePath}.", ex);
                }
                catch (NotSupportedException ex)
                {
                    throw new ProwlarrApiException($"Failed to serialize request body (unsupported type?) for {method} {relativePath}.", ex);
                }
            }

            HttpResponseMessage response;

            try
            {
                response = await _httpClient.SendAsync(request, cancellationToken);
            }
            catch (HttpRequestException ex)
            {
                // Network errors, DNS issues, etc.
                throw new ProwlarrApiException($"Network error occurred while sending request to {relativePath}.", ex);
            }
            catch (TaskCanceledException ex) when (cancellationToken.IsCancellationRequested)
            {
                // Handle cancellation specifically if needed, otherwise rethrow or wrap.
                throw new OperationCanceledException($"Request to {relativePath} was cancelled.", ex, cancellationToken);
            }
            catch (TaskCanceledException ex) // Handles timeout
            {
                throw new ProwlarrApiException($"Request to {relativePath} timed out.", ex);
            }

            using (response) // Ensure response is disposed
            {
                string responseContent = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

                if (!response.IsSuccessStatusCode)
                {
                    // Throw a specific exception for API errors.
                    throw new ProwlarrApiException($"API request failed.", response.StatusCode, responseContent);
                }

                // Handle cases where no response body is expected or needed (e.g., DELETE, or successful POST/PUT with 2xx No Content)
                // Check if TResponse is object (used as placeholder) or if status code indicates no content.
                if (typeof(TResponse) == typeof(object) || response.StatusCode == System.Net.HttpStatusCode.NoContent || string.IsNullOrWhiteSpace(responseContent))
                {
                    // Check if TResponse is nullable or a reference type before returning default.
                    if (default(TResponse) == null || Nullable.GetUnderlyingType(typeof(TResponse)) != null)
                    {
                        return default!; // Return default (null for reference types/nullable value types)
                    }
                    else
                    {
                        // This case is unlikely if TResponse isn't an object, but handles non-nullable value types if used incorrectly.
                        throw new ProwlarrApiException($"Received success status code {response.StatusCode} but expected a non-nullable response body of type {typeof(TResponse).Name}, and response was empty.", response.StatusCode);
                    }
                }

                try
                {
                    var result = JsonSerializer.Deserialize<TResponse>(responseContent, _jsonSerializerOptions);
                    if (result == null && Nullable.GetUnderlyingType(typeof(TResponse)) == null && typeof(TResponse).IsValueType == false)
                    {
                        // If deserialization results in null for a non-nullable reference type (could happen with "null" JSON).
                        throw new ProwlarrApiException($"API returned null but expected a non-null response body of type {typeof(TResponse).Name}.", response.StatusCode, responseContent);
                    }
                    return result!; // Nullable reference types handled by the null check above.
                }
                catch (JsonException ex)
                {
                    // Handle deserialization errors
                    throw new ProwlarrApiException($"Failed to deserialize JSON response from {relativePath}. Content: {responseContent}", response.StatusCode, ex, responseContent);
                }
            }
        }

        // --- API Endpoint Implementations ---

        /// <summary>
        /// Gets the system status from the Prowlarr API. Corresponds to /api/v1/system/status.
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the request.</param>
        /// <returns>A <see cref="SystemStatus"/> object containing system information.</returns>
        /// <exception cref="ProwlarrApiException">Thrown if the API request fails.</exception>
        public async Task<SystemStatus> GetSystemStatusAsync(CancellationToken cancellationToken = default)
        {
            return await GetAsync<SystemStatus>("api/v1/system/status", cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the list of configured indexers. Corresponds to /api/v1/indexer.
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the request.</param>
        /// <returns>A list of <see cref="Indexer"/> objects.</returns>
        /// <exception cref="ProwlarrApiException">Thrown if the API request fails.</exception>
        public async Task<List<Indexer>> GetIndexersAsync(CancellationToken cancellationToken = default)
        {
            // Prowlarr might return null instead of an empty array if no indexers exist.
            var result = await GetAsync<List<Indexer>?>("api/v1/indexer", cancellationToken).ConfigureAwait(false);
            return result ?? new List<Indexer>(); // Return empty list if API returns null.
        }

        /// <summary>
        /// Gets a specific indexer by its ID. Corresponds to /api/v1/indexer/{id}.
        /// </summary>
        /// <param name="indexerId">The ID of the indexer to retrieve.</param>
        /// <param name="cancellationToken">A token to cancel the request.</param>
        /// <returns>The requested, <see cref="Indexer"/> object.</returns>
        /// <exception cref="ProwlarrApiException">Thrown if the API request fails (e.g., indexer not found - 404).</exception>
        public async Task<Indexer> GetIndexerByIdAsync(int indexerId, CancellationToken cancellationToken = default)
        {
            if (indexerId <= 0) throw new ArgumentOutOfRangeException(nameof(indexerId), "Indexer ID must be positive.");
            return await GetAsync<Indexer>($"api/v1/indexer/{indexerId}", cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Adds a new Indexer. Corresponds to POST /api/v1/indexer.
        /// </summary>
        /// <param name="newIndexer">The indexer configuration to add. The ID property should typicall be 0 or omitted.</param>
        /// <param name="cancellationToken">A token to cancel the request.</param>
        /// <returns>The newly created <see cref="Indexer"/> object, including its assigned ID.</returns>
        /// <exception cref="ArgumentNullException">Thrown if newIndexer is null.</exception>
        /// <exception cref="ProwlarrApiException">Thrown if the API request fails.</exception>
        public async Task<Indexer> AddIndexerAsync(Indexer newIndexer, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(newIndexer);
            // Optionally add validation for required fields in newIndexer here.
            return await PostAsync<Indexer, Indexer>("api/v1/indexer", newIndexer, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Updates an existing indexer. Corresponds to PUT /api/v1/indexer/{id}.
        /// </summary>
        /// <param name="indexerToUpdate">The indexer configuration with updated values. The ID must match the indexer to update.</param>
        /// <param name="cancellationToken">A token to cancel the request.</param>
        /// <returns>The updated <see cref="Indexer"/> object.</returns>
        /// <exception cref="ArgumentNullException">Thrown if indexerToUpdate is null.</exception>
        /// <exception cref="ArgumentException">Thrown if indexerToUpdate has an invalid ID.</exception>
        /// <exception cref="ProwlarrApiException">Thrown if the API request fails.</exception>
        public async Task<Indexer> UpdateIndexerAsync(Indexer indexerToUpdate, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(indexerToUpdate);
            if (indexerToUpdate.Id <= 0) throw new ArgumentException("Indexer must have a valid positive ID for update.", nameof(indexerToUpdate));

            // Optionally add more validation here

            return await PutAsync<Indexer, Indexer>($"api/v1/indexer/{indexerToUpdate.Id}", indexerToUpdate, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Deletes an indexer by its ID. Corresponds to DELETE /api/v1/indexer/{id}.
        /// </summary>
        /// <param name="indexerId">The ID of the indexer to delete.</param>
        /// <param name="cancellationToken">A token to cancel the request.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if indexerId is not positive.</exception>
        /// <exception cref="ProwlarrApiException">Thrown if the API request fails.</exception>
        public async Task DeleteIndexerAsync(int indexerId, CancellationToken cancellationToken = default)
        {
            if (indexerId <= 0) throw new ArgumentOutOfRangeException(nameof(indexerId), "Indexer ID must be positive.");
            await DeleteAsync($"api/v1/indexer/{indexerId}", cancellationToken).ConfigureAwait(false);
        }

        // -- IDisposable Implementation

        /// <summary>
        /// Releases the resources used by the <see cref="ProwlarrClient"/>.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this); // Prevent finalizer from running if Dispose was called.
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Dispose managed state (managed objects).
                    _httpClient?.Dispose();
                }

                // Free unmanaged resources (unmanaged objects) and override finalizer.
                // Set large fields to null
                _disposed = true;
            }
        }
    }
}
