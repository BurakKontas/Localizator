using Localizator.Shared.Extensions;
using Localizator.Shared.Providers;
using Localizator.Shared.Result;
using System.Diagnostics;
using System.Text.Json;

namespace Localizator.API.Middlewares;

public class ResultWrapperMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        var originalBodyStream = context.Response.Body;
        using var responseBody = new MemoryStream();
        context.Response.Body = responseBody;

        try
        {
            await _next(context);

            var meta = MetaProvider.GetFromContext(context);

            // Response'u oku
            responseBody.Seek(0, SeekOrigin.Begin);
            var responseText = await new StreamReader(responseBody).ReadToEndAsync();

            // JSON mı kontrol et
            if (IsJsonResponse(context) && !responseText.IsNullOrWhitespace())
            {
                try
                {
                    var responseObject = JsonSerializer.Deserialize<object>(responseText, GetJsonOptions());

                    Result finalResult;

                    if (IsResultObject(responseText))
                    {
                        var existingResult = JsonSerializer.Deserialize<Result>(responseText, GetJsonOptions());
                        if (existingResult != null)
                        {
                            existingResult.Meta = meta;
                            finalResult = existingResult;
                        }
                        else
                        {
                            finalResult = Result.Success(responseObject, meta: meta);
                        }
                    }
                    else if (context.Response.StatusCode >= 200 && context.Response.StatusCode < 300)
                    {
                        finalResult = Result.Success(responseObject, meta: meta);
                    }
                    else
                    {
                        finalResult = Result.Failure($"Error: {context.Response.StatusCode}", responseObject, meta);
                        context.Response.ContentType = "application/json";
                    }

                    var finalResponse = JsonSerializer.Serialize(finalResult, GetJsonOptions());
                    await WriteResponse(context, originalBodyStream, finalResponse);
                }
                catch (JsonException)
                {
                    await CopyOriginalResponse(responseBody, originalBodyStream);
                }
            }
            else
            {
                await CopyOriginalResponse(responseBody, originalBodyStream);
            }
        }
        catch (Exception ex)
        {
            // Exception durumunda Result ile wrap et
            await HandleException(context, originalBodyStream, ex);
        }
        finally
        {
            context.Response.Body = originalBodyStream;
        }
    }

    private async Task HandleException(HttpContext context, Stream originalStream, Exception exception)
    {
        var meta = MetaProvider.GetFromContext(context);

        if (!context.Response.HasStarted)
        {
            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";

            var isDevelopment = context.RequestServices
                .GetService<IWebHostEnvironment>()?
                .IsDevelopment() ?? false;

            var errorResult = Result.Failure(
                message: exception.Message,
                data: isDevelopment ? new
                {
                    exceptionType = exception.GetType().Name,
                    stackTrace = exception.StackTrace,
                    innerException = exception.InnerException?.Message
                } : null,
                meta: meta
            );

            var errorResponse = JsonSerializer.Serialize(errorResult, GetJsonOptions());
            await WriteResponse(context, originalStream, errorResponse);
        } else {

            throw exception;
        }
    }

    private bool IsJsonResponse(HttpContext context)
    {
        return context.Response.ContentType?.Contains("application/json") == true;
    }

    private bool IsResultObject(string json)
    {
        try
        {
            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;

            return root.TryGetProperty("isSuccess", out _) ||
                   root.TryGetProperty("IsSuccess", out _);
        }
        catch
        {
            return false;
        }
    }

    private JsonSerializerOptions GetJsonOptions()
    {
        return new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = false,
            PropertyNameCaseInsensitive = true
        };
    }

    private async Task WriteResponse(HttpContext context, Stream originalStream, string content)
    {
        var bytes = System.Text.Encoding.UTF8.GetBytes(content);
        context.Response.ContentLength = bytes.Length;
        context.Response.Body = originalStream;
        await context.Response.WriteAsync(content);
    }

    private async Task CopyOriginalResponse(MemoryStream responseBody, Stream originalStream)
    {
        responseBody.Seek(0, SeekOrigin.Begin);
        await responseBody.CopyToAsync(originalStream);
    }
}