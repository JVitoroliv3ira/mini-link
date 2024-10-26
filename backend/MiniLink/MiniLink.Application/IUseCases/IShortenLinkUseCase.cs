using MiniLink.Domain.Dtos.Link;
using MiniLink.Domain.Models;

namespace MiniLink.Application.IUseCases;

public interface IShortenLinkUseCase
{
    Task<Link> Execute(CreateLinkDto dto);
}