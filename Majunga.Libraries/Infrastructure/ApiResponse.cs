using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Majunga.Libraries.Infrastructure
{
    public class ApiResponse
    {
        [JsonConstructor]
        public ApiResponse()
        {
        }
        private ApiResponse(HttpStatusCode statusCode, string? message = null, object? data = null, JsonSerializerOptions? jsonSerializerOptions = null)
        {
            _options = jsonSerializerOptions ?? new JsonSerializerOptions();

            StatusCode = statusCode;
            Message = message;
            Data = Serialise(data, _options);
        }

        private JsonSerializerOptions? _options;

        [JsonPropertyName("statusCode")]
        public HttpStatusCode StatusCode { get; set; }

        [JsonPropertyName("message")]
        public string? Message { get; set; }

        [JsonPropertyName("data")]
        public string? Data { get; set; }

        [JsonIgnore]
        public bool IsSuccessStatusCode
        {
            get { return (int)StatusCode >= 200 && (int)StatusCode <= 299; }
        }


        public static ApiResponse Create(HttpStatusCode statusCode) => new ApiResponse(statusCode);

        public static ApiResponse Success(HttpStatusCode statusCode, object? data = null, JsonSerializerOptions? jsonSerializerOptions = null)
        {
            return new ApiResponse(statusCode, data: data, jsonSerializerOptions: jsonSerializerOptions);
        }

        public static ApiResponse Failure(HttpStatusCode statusCode, string? message = null, object? data = null, JsonSerializerOptions? jsonSerializerOptions = null)
        {
            return new ApiResponse(statusCode, message, data, jsonSerializerOptions: jsonSerializerOptions);
        }

        public T? GetData<T>()
            where T : class
        {
            if (Data == null) return null;
            return JsonSerializer.Deserialize<T>(Data, _options);
        }

        private static string Serialise(object? data, JsonSerializerOptions? options) => JsonSerializer.Serialize(data, options);
    }
}
