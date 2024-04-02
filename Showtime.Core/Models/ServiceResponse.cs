namespace Showtime.Core.Models
{
    public class ServiceResponse<T>(T? data, bool success, string? message)
    {
        public T? Data { get; set; } = data;
        public bool Success { get; set; } = success;
        public string? Message { get; set; } = message;

        public static ServiceResponse<T> CreateSuccessResponse(T? data, string? message) => new(data, true, message);

        public static ServiceResponse<T> CreateErrorResponse(T? data, string? message) => new(data, false, message);
    }
}
