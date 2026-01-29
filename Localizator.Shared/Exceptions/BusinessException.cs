namespace Localizator.Shared.Exceptions;

public sealed class BusinessException : BaseException
{
    protected override string Title => "Business Error";

    public BusinessException(string message)
        : this("NoErrorCode", message)
    {
    }

    public BusinessException(
        string code,
        string message,
        int statusCode = 400,
        Exception? innerException = null)
        : base(code, message, statusCode, innerException)
    {
    }
}