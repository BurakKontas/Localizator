using Localizator.Shared.Providers;
using Soenneker.Dtos.ProblemDetails;

namespace Localizator.Shared.Result;

public class Result()
{
    public bool IsSuccess { get; protected set; }
    public string Message { get; protected set; } = string.Empty;
    public object? Data { get; protected set; }
    public Meta? Meta { get; set; } // middleware sets meta
    public ProblemDetailsDto? ProblemDetails { get; protected set; }


    public static Result Success(object? data = null, string message = "", Meta? meta = null)
        => new()
        {
            IsSuccess = true,
            Message = message,
            Data = data,
            Meta = meta ?? MetaProvider.Get()
        };

    public static Result Failure(string message = "", object? data = null, Meta? meta = null, ProblemDetailsDto? problemDetails = null)
        => new()
        {
            IsSuccess = false,
            Message = message,
            Data = data,
            Meta = meta ?? MetaProvider.Get(),
            ProblemDetails = problemDetails
        };
}