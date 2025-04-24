using System.Net;
using System.Text;

namespace JacquesMurray.ProwlarrApiClient
{
    /// <summary>
    /// Represents errors that occur during Prowlarr API interactions.
    /// This exception provides context about the HTTP request failure, including status code and response body.
    /// </summary>
    public class ProwlarrApiException : Exception
    {
        /// <summary>
        /// Gets the HTTP status code associated with the error, if available.
        /// </summary>
        public HttpStatusCode? StatusCode { get; private set; }

        /// <summary>
        /// Gets the raw response content associated with the error, if available.
        /// Useful for debugging unexpected API responses.
        /// </summary>
        public string? ResponseContent { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProwlarrApiException"/> class.
        /// </summary>
        public ProwlarrApiException()
            : base("An unspecified Prowlarr API error occurred.") { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProwlarrApiException"/> class
        /// with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public ProwlarrApiException(string message)
            : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProwlarrApiException"/> class
        /// with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public ProwlarrApiException(string message, Exception innerException) :
            base(message, innerException)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProwlarrApiException"/> class
        /// with a specified error message, HTTP status code, and optionally the response content.
        /// </summary>
        /// <param name="message">The base error message that explains the reason for the exception.</param>
        /// <param name="statusCode">The HTTP status code returned by the API.</param>
        /// <param name="responseContent">The content of the HTTP response, if available.</param>
        public ProwlarrApiException(string message, HttpStatusCode statusCode, string? responseContent = null)
            : base(BuildMessage(message, statusCode, responseContent))
        {
            StatusCode = statusCode;
            ResponseContent = responseContent;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProwlarrApiException"/> class
        /// with a specified error message, HTTP status code, an inner exception, and optionally the response content.
        /// </summary>
        /// <param name="message">The base error message that explains the reason for the exception.</param>
        /// <param name="statusCode">The HTTP status code returned by the API.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        /// <param name="responseContent">The content of the HTTP response, if available.</param>
        public ProwlarrApiException(string message, HttpStatusCode statusCode, Exception innerException, string? responseContent = null)
            : base(BuildMessage(message, statusCode, responseContent), innerException)
        {
            StatusCode = statusCode;
            ResponseContent = responseContent;
        }

        /// <summary>
        /// Builds a detailed error message including the base message, status code, and response content.
        /// </summary>
        /// <param name="baseMessage">The core error message.</param>
        /// <param name="statusCode">The HTTP status code.</param>
        /// <param name="responseContent">The API response content (optional).</param>
        /// <returns>A formatted error message string.</returns>
        private static string BuildMessage(string baseMessage, HttpStatusCode statusCode, string? responseContent)
        {
            // Use StringBuilder for efficient string concatenation.
            var messageBuilder = new StringBuilder();
            messageBuilder.Append($"Prowlarr API request failed with status code {(int)statusCode} ({statusCode}).");

            // Append the specific message if provided.
            if (!string.IsNullOrWhiteSpace(baseMessage))
            {
                messageBuilder.Append($" Message: {baseMessage}");
            }

            // Append the response content if available and potentially useful.
            if (!string.IsNullOrWhiteSpace(responseContent))
            {
                // Consider trimming long responses or adding only a preview.
                const int maxResponseLength = 500; // Limit displayed response length
                string responseSnippet = responseContent.Length > maxResponseLength
                    ? responseContent.Substring(0, maxResponseLength) + "..."
                    : responseContent;
                messageBuilder.Append($"\nResponse Preview: {responseSnippet}");
            }

            return messageBuilder.ToString();
        }
    }
}