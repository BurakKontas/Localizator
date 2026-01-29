namespace Localizator.Shared.Helpers;

public static class HttpProblemTypeMapper
{
    public static string FromStatusCode(int statusCode)
        => statusCode switch
        {
            100 => "https://datatracker.ietf.org/doc/html/rfc9110#section-15.3.1",
            101 => "https://datatracker.ietf.org/doc/html/rfc9110#section-15.3.2",

            200 => "about:blank",
            201 => "about:blank",
            202 => "about:blank",

            300 => "about:blank",
            301 => "about:blank",
            302 => "about:blank",

            // --- 4xx Client Errors (RFC7231) ---
            400 => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
            401 => "https://datatracker.ietf.org/doc/html/rfc7235#section-3.1",
            402 => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.2",
            403 => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.3",
            404 => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4",
            405 => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.5",
            406 => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.6",
            408 => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.7",
            409 => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.8",
            410 => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.9",
            411 => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.10",
            413 => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.11",
            414 => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.12",
            415 => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.13",
            417 => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.14",
            426 => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.15",

            // --- 5xx Server Errors (RFC7231 §6.6) ---
            500 => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1",
            501 => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.2",
            502 => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.3",
            503 => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.4",
            504 => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.5",
            505 => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.6",

            // --- Defaults / extensions ---
            _ => "about:blank"
        };
}
