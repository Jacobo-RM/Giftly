
using Giftly.Api.Models;

namespace Giftly.Api.Interfaces;

public interface IGiftIdeaGenerator
{
    Task<IReadOnlyList<GiftIdea>> GenerateAsync(GiftRequest request, CancellationToken ct);
}