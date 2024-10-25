using MiniLink.Domain.Dtos.Link;
using MiniLink.Domain.Models;

namespace MiniLink.Application.IUseCases;

public interface ICreateLinkUseCase
{
    Task<Link> Execute(CreateLinkDto dto);
}