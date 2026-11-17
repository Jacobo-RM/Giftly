
using Giftly.Api.Interfaces;
using Giftly.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace GiftFinder.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GiftsController : ControllerBase
{
    private readonly IGiftIdeaGenerator _generator;
    private readonly IAffiliateLinkProvider _links;

    public GiftsController(IGiftIdeaGenerator generator, IAffiliateLinkProvider links)
    {
        _generator = generator;
        _links = links;
    }

    [HttpPost]
    public async Task<ActionResult<IEnumerable<GiftSuggestion>>> Suggest([FromBody] GiftRequest request, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(request.Interests))
            return BadRequest("Indica al menos algún gusto.");

        var ideas = await _generator.GenerateAsync(request, ct);

        return Ok(ideas.Select(i => new GiftSuggestion(i.Title, i.Reason, i.EstimatedPrice, _links.BuildLink(i))));
    }
}