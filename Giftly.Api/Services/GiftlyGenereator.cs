using System.Text;
using System.Text.Json;
using Giftly.Api.Interfaces;
using Giftly.Api.Models;

namespace Giftly.Api.Services;

public class GiftlyGenereator : IGiftIdeaGenerator
{
    private const string promp = """
        Eres un experto en regalos. Devuelve SOLO un array JSON (sin texto extra, sin markdown) con 6 ideas.
        Cada elemento: {"title": string, "reason": string, "searchQuery": string, "estimatedPrice": number|null}.
        - title: nombre corto de la idea (genérica, no una marca concreta salvo que sea muy obvia).
        - reason: 1-2 frases explicando por qué encaja con esa persona.
        - searchQuery: búsqueda en español para encontrarlo en una tienda online.
        - estimatedPrice: precio aproximado en euros.
        Respeta el presupuesto máximo si se indica. Varía las ideas (no todas de la misma categoría).
        """;

    private static readonly JsonSerializerOptions JsonOpts = new() { PropertyNameCaseInsensitive = true };

    private readonly HttpClient _http;
    private readonly string _model;

    public GiftlyGenereator(HttpClient http, IConfiguration config)
    {
        _http = http;
        _model = config["Anthropic:Model"] ?? "claude-sonnet-5-5";
    }

    public async Task<IReadOnlyList<GiftIdea>> GenerateAsync(GiftRequest request, CancellationToken ct)
    {
        var userMessage = $"""
            Relación: {request.Relationship}
            Edad: {request.Age?.ToString() ?? "no indicada"}
            Ocasión: {request.Occasion}
            Gustos: {request.Interests}
            Presupuesto máximo: {(request.BudgetMax.HasValue ? request.BudgetMax + " €" : "sin límite")}
            Notas: {request.Notes ?? "ninguna"}
            """;

        var body = new
        {
            model = _model,
            max_tokens = 1500,
            system = promp,
            messages = new[] { new { role = "user", content = userMessage } }
        };

        using var content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");
        using var response = await _http.PostAsync("v1/messages", content, ct);
        response.EnsureSuccessStatusCode();
        using var doc = await JsonDocument.ParseAsync(await response.Content.ReadAsStreamAsync(ct), cancellationToken: ct);
        var text = doc.RootElement.GetProperty("content")[0].GetProperty("text").GetString() ?? "[]";
        text = text.Trim().Trim('`');
        if (text.StartsWith("json", StringComparison.OrdinalIgnoreCase)) text = text[4..];
        return JsonSerializer.Deserialize<List<GiftIdea>>(text, JsonOpts) ?? [];
    }
}