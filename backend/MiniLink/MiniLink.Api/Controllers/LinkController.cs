using Microsoft.AspNetCore.Mvc;
using MiniLink.Application.IUseCases;
using MiniLink.Domain.Dtos;
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
    public async Task<IActionResult> ShortenLink([FromBody] ShortenLinkDto request)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();

            return BadRequest(new ApiResponse<string>(false, "Erro de validação", null, errors));
        }

        try
        {
            var link = await _shortenLinkUseCase.Execute(request);
            return Ok(new ApiResponse<object>(
                true,
                "Link encurtado com sucesso",
                new { link.Slug }
            ));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao encurtar o link.");
            return StatusCode(500, new ApiResponse<string>(
                false,
                "Ocorreu um erro inesperado ao encurtar o link."
            ));
        }
    }
}