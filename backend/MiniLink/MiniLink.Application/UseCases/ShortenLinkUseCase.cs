using System.Security.Cryptography;
using MiniLink.Application.IUseCases;
using MiniLink.Domain.Dtos.Requests.Link;
using MiniLink.Domain.Models;
using MiniLink.Domain.Repositories;

namespace MiniLink.Application.UseCases;

public class ShortenLinkUseCase : IShortenLinkUseCase
{
    private readonly ILinkRepository _linkRepository;
    private const int BytesToGenerate = 4;
    private const int DaysUntilExpiration = 7;

    public ShortenLinkUseCase(ILinkRepository linkRepository)
    {
        _linkRepository = linkRepository;
    }

    public async Task<Link> Execute(ShortenLinkDto dto)
    {
        var link = new Link
        {
            OriginalUrl = dto.OriginalUrl,
            Slug = await GenerateUniqueSlug(),
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = DateOnly.FromDateTime(DateTime.UtcNow).AddDays(DaysUntilExpiration)
        };

        await _linkRepository.Insert(link);
        return link;
    }

    private async Task<string> GenerateUniqueSlug()
    {
        string slug;
        bool exists;

        do
        {
            slug = GenerateSlug();
            exists = await _linkRepository.Exists(l => l.Slug == slug);
        } while (exists);

        return slug;
    }

    private static string GenerateSlug(int slugLength = 6)
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(BytesToGenerate)).Substring(0, slugLength);
    }
}