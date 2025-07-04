namespace NetMovilAPI.Domain.Entities.BaseEntities;
public class ApiResponse<T>
{
    public T Data { get; set; }
    public bool Success { get; set; }
    public string Message { get; set; }
    public List<string> Errors { get; set; }
    public ApiResponse()
    {
        Success = false;
        Message = string.Empty;
        Data = default;
    }
    public ApiResponse(T data, string message)
    {
        Success = true;
        Message = message;
        Data = data;
    }
    public ApiResponse(string message)
    {
        Success = false;
        Message = message;
        Data = default;
    }

    public ApiResponse(List<string> errors)
    {
        Success = false;
        Message = "Ocurrio un error";
        Data = default;
        Errors = errors;
    }
    public ApiResponse(T data, string message, List<string> errors)
    {
        Success = true;
        Message = message;
        Data = data;
        Errors = errors;
    }
}
