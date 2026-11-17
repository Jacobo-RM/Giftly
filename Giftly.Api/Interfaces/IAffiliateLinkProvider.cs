using Giftly.Api.Models;

namespace Giftly.Api.Interfaces;

public interface IAffiliateLinkProvider
{
    string BuildLink(GiftIdea idea);
}