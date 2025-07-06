namespace NetMovilAPI.Domain.Entities.BaseEntities;
public class ApiResponse<T>
{
    public T? Data { get; set; }
    public bool Success { get; set; }
    public string Message { get; set; }
    public List<string>? Errors { get; set; }
    /// <summary>
    /// Initializes a new instance of the <see cref="ApiResponse{T}"/> class with default values.
    /// </summary>
    /// <remarks>By default, the <see cref="Success"/> property is set to <see langword="false"/>,  the <see
    /// cref="Message"/> property is initialized to an empty string,  and the <see cref="Data"/> property is set to its
    /// default value.</remarks>
    public ApiResponse()
    {
        Success = true;
        Message = string.Empty;
        Data = default;
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="ApiResponse{T}"/> class with the specified data and message for a successful response.
    /// </summary>
    /// <param name="data">The data associated with the response. A list or a single registry</param>
    /// <param name="message">A message providing additional information about the response.</param>
    public ApiResponse(T data, string message)
    {
        Success = true;
        Message = message;
        Data = data;
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="ApiResponse{T}"/> class with the specified message for a failed response.
    /// </summary>
    /// <param name="message">The message describing the response or providing additional context.</param>
    public ApiResponse(string message)
    {
        Success = false;
        Message = message;
        Data = default;
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="ApiResponse{T}"/> class with the specified error messages for a failed operation.
    /// </summary>
    /// <remarks>This constructor sets the <see cref="Success"/> property to <see langword="false"/> and
    /// initializes the  <see cref="Message"/> property with a default error message. The <see cref="Data"/> property is
    /// set to its default value.</remarks>
    /// <param name="errors">A list of error messages describing the issues that occurred. Cannot be null.</param>
    public ApiResponse(List<string> errors)
    {
        Success = false;
        Message = "Ocurrio un error";
        Data = default;
        Errors = errors;
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="ApiResponse{T}"/> class with a failed status, a message, and a list
    /// of errors for a failed operation.
    /// </summary>
    /// <param name="message">The message describing the result of the operation.</param>
    /// <param name="errors">A list of error messages associated with the response. Can be empty if no errors occurred.</param>
    public ApiResponse(string message, List<string> errors)
    {
        Success = false;
        Message = message;
        Data = default;
        Errors = errors;
    }
}
