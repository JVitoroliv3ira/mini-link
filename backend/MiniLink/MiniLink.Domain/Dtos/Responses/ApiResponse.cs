namespace MiniLink.Domain.Dtos.Responses;

public class ApiResponse<T>
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public T? Data { get; set; }
    public List<string>? Errors { get; set; }

    public ApiResponse(bool success, string? message = null, T? data = default, List<string>? errors = null)
    {
        Success = success;
        Message = message;
        Data = data;
        Errors = errors ?? new();
    }
}