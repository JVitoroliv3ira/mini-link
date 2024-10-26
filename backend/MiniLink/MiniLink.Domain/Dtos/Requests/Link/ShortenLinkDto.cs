using System.ComponentModel.DataAnnotations;

namespace MiniLink.Domain.Dtos.Requests.Link;

public class ShortenLinkDto
{
    [Url(ErrorMessage = "A URL deve ser um endereço válido.")]
    public string OriginalUrl { get; set; }
}