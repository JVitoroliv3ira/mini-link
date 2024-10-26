using Microsoft.AspNetCore.Mvc;
using MiniLink.Application.IUseCases;
using MiniLink.Domain.Dtos.Link;

namespace MiniLink.Api.Controllers;


[ApiController]
[Route("[controller]")]
public class LinkController : ControllerBase
{
    private readonly ILogger<LinkController> _logger;
    private readonly IShortenLinkUseCase _shortenLinkUseCase;

    public LinkController(
        ILogger<LinkController> logger,
        IShortenLinkUseCase shortenLinkUseCase
    )
    {
        _logger = logger;
        _shortenLinkUseCase = shortenLinkUseCase;
    }

    [HttpPost("shorten")]
    public async Task<IActionResult> ShortenLink([FromBody] CreateLinkDto request)
    {
        try
        {
            var link = await _shortenLinkUseCase.Execute(request);
            return Ok(new
            {
                link.Slug
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(
                500,
                "Ocorreu um erro inesperado ao encurtar o link."
            );
        }
    }
}