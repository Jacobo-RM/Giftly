namespace Giftly.Api.Models;

public record GiftSuggestion(
    string Title,
    string Reason,
    decimal? EstimatedPrice,
    string Url
);