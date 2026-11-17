namespace Giftly.Api.Models;

public record GiftRequest(
    string Relationship,
    int? Age,
    string Occasion,
    string Interests,
    decimal? BudgetMax,
    string? Notes
);

