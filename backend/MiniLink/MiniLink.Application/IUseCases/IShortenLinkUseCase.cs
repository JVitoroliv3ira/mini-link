using MiniLink.Domain.Dtos.Requests.Link;
using MiniLink.Domain.Models;

namespace MiniLink.Application.IUseCases;

public interface IShortenLinkUseCase
{
    Task<Link> Execute(ShortenLinkDto dto);
}