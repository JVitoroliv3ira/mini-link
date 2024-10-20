namespace MiniLink.Domain.Models;

public class Link
{
    public int Id { get; set; }
    public string OriginalUrl { get; set; }
    public string Slug { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateOnly? ExpiresAt { get; set; }
}