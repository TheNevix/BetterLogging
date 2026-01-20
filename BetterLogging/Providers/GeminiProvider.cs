using BetterLogging.Abstractions;
using BetterLogging.Configuration;
using System.Net.Http.Json;
using System.Text.Json;

namespace BetterLogging.Providers;

public sealed class GeminiProvider : IAiProvider
{
    public string ProviderName => "Gemini";

    private string Endpoint =
        $"https://generativelanguage.googleapis.com/v1beta/models/MODELNAMETOREPLACE:generateContent";

    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public GeminiProvider(string apiKey)
    {
        if (string.IsNullOrWhiteSpace(apiKey))
            throw new ArgumentException("Gemini API key must be provided", nameof(apiKey));

        _apiKey = apiKey;
        _httpClient = new HttpClient();
    }

    public async Task<string> GenerateAsync(
        string prompt,
        AiModel model,
        CancellationToken cancellationToken = default)
    {
        Endpoint = Endpoint.Replace("MODELNAMETOREPLACE", model.Name);

        var request = new
        {
            contents = new[]
            {
                new
                {
                    parts = new[]
                    {
                        new { text = prompt }
                    }
                }
            },
            generationConfig = new
            {
                temperature = 0.2,
                maxOutputTokens = 512
            }
        };

        var response = await _httpClient.PostAsJsonAsync(
            $"{Endpoint}?key={_apiKey}",
            request,
            cancellationToken);

        // IMPORTANT: log body on failure
        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync(cancellationToken);
            throw new HttpRequestException(
                $"Gemini call failed ({(int)response.StatusCode}): {error}");
        }

        using var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
        using var json = await JsonDocument.ParseAsync(stream, cancellationToken: cancellationToken);

        return ExtractText(json);
    }

    private static string ExtractText(JsonDocument json)
    {
        return json
            .RootElement
            .GetProperty("candidates")[0]
            .GetProperty("content")
            .GetProperty("parts")[0]
            .GetProperty("text")
            .GetString()
            ?? throw new InvalidOperationException("Empty Gemini response");
    }
}
