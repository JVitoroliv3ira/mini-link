using System.ComponentModel.DataAnnotations;

namespace MiniLink.Domain.Dtos.Link;

public class ShortenLinkDto
{
    [Required(ErrorMessage = "Informe a URL.")]
    [Url(ErrorMessage = "A URL deve ser um endereço válido.")]
    public string OriginalUrl { get; set; }
}