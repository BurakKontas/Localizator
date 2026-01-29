using Localizator.Shared.Helpers;
using Soenneker.Dtos.ProblemDetails;

namespace Localizator.Shared.Exceptions;

public abstract class BaseException : Exception
{
    public string Code { get; }
    public int StatusCode { get; }

    protected BaseException(
        string code,
        string message,
        int statusCode,
        Exception? innerException = null)
        : base(message, innerException)
    {
        Code = code;
        StatusCode = statusCode;
    }

    protected virtual string Title => "Error";

    protected virtual IDictionary<string, object> Extensions => new Dictionary<string, object>
    {
        ["code"] = Code
    };

    public virtual ProblemDetailsDto GetProblemDetails()
    {
        ProblemDetailsDto details = new()
        {
            Title = Title,
            Detail = Message,
            Status = StatusCode,
            Type = HttpProblemTypeMapper.FromStatusCode(StatusCode),
            Extensions = { }
        };

        details.Extensions!.Concat(Extensions);

        return details;
    }
}
