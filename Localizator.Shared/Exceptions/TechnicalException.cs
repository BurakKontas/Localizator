namespace Localizator.Shared.Exceptions;

public sealed class TechnicalException : BaseException
{
    protected override string Title => "Technical Error";

    public TechnicalException(
        string message,
        string code = "TechnicalError",
        Exception? innerException = null)
        : base(code, message, 500, innerException)
    {
    }
}
