namespace Giftly.Api.Models;
public record GiftIdea(
    string Title,
    string Reason,
    string SearchQuery,
    decimal? EstimatedPrice
);
