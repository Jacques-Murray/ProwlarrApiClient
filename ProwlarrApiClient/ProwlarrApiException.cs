using System.Net;

namespace JacquesMurray.ProwlarrApiClient
{
    /// <summary>
    /// Represents errors that occur during Prowlarr API interactions.
    /// </summary>
    public class ProwlarrApiException : Exception
    {
        /// <summary>
        /// Gets the HTTP status code associated with the error, if available.
        /// </summary>
        public HttpStatusCode? StatusCode { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProwlarrApiException"/> class.
        /// </summary>
        public ProwlarrApiException()
            : base() { }

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
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public ProwlarrApiException(string message, Exception innerException) :
            base(message, innerException)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProwlarrApiException"/> class
        /// with a specified error message, HTTP status code, and optionally the response content.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="statusCode">The HTTP status code returned by the API.</param>
        /// <param name="responseContent">The content of the HTTP response, if available.</param>
        public ProwlarrApiException(string message, HttpStatusCode statusCode, string? responseContent = null)
            : base(BuildMessage(message, statusCode, responseContent))
        {
            StatusCode = statusCode;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProwlarrApiException"/> class
        /// with a specified error message, HTTP status code, an inner exception, and optionally the response conent.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="statusCode">The HTTP status code returned by the API.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        /// <param name="responseContent">The content of the HTTP response, if available.</param>
        public ProwlarrApiException(string message, HttpStatusCode statusCode, Exception innerException, string? responseContent = null)
            : base(BuildMessage(message, statusCode, responseContent), innerException)
        {
            StatusCode = statusCode;
        }

        private static string BuildMessage(string message, HttpStatusCode statusCode, string? responseContent)
        {
            var fullMessage = $"Prowlarr API request failed with status code {(int)statusCode} ({statusCode}).";

            if (!string.IsNullOrWhiteSpace(message))
            {
                fullMessage += $" Message: {message}";
            }
            if (!string.IsNullOrWhiteSpace(responseContent))
            {
                fullMessage += $"\nResponse: {responseContent}";
            }

            return fullMessage;
        }
    }
}
