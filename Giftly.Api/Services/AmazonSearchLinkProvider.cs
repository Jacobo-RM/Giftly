using Giftly.Api.Interfaces;
using Giftly.Api.Models;

namespace Giftly.Api.Services;

public class AmazonSearchLinkProvider : IAffiliateLinkProvider
{
    private readonly string _domain;
    private readonly string _tag;

    public AmazonSearchLinkProvider(IConfiguration config)
    {
        _domain = config["Amazon:Domain"] ?? "www.amazon.es";
        _tag = config["Amazon:PartnerTag"] ?? throw new InvalidOperationException("Falta Amazon:PartnerTag");
    }

    public string BuildLink(GiftIdea idea) =>
        $"https://{_domain}/s?k={Uri.EscapeDataString(idea.SearchQuery)}&tag={_tag}";

        //TENEMOS QUE GENERAR EL TAG DE AFILIADOS. HAY QUE COMPRAR TRES COSAS CON EL PARA HABILITARLO ENTERO.
}