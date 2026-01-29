using Localizator.Shared.Providers;

namespace Localizator.Shared.Result;

public class Result()
{
    public bool IsSuccess { get; protected set; }
    public string Message { get; protected set; } = string.Empty;
    public object? Data { get; protected set; }
    public Meta? Meta { get; set; }

    public static Result Success(object? data = null, string message = "", Meta? meta = null)
        => new()
        {
            IsSuccess = true,
            Message = message,
            Data = data,
            Meta = meta ?? MetaProvider.Get()
        };

    public static Result Failure(string message = "", object? data = null, Meta? meta = null)
        => new()
        {
            IsSuccess = false,
            Message = message,
            Data = data,
            Meta = meta ?? MetaProvider.Get()
        };
}