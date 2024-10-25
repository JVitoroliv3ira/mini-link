using Microsoft.EntityFrameworkCore;
using MiniLink.Domain.Models;
using MiniLink.Domain.Repositories;

namespace MiniLink.Infrastructure.Repositories;

public class LinkRepository : RepositoryBase<Link>, ILinkRepository
{
    public LinkRepository(DbContext context) : base(context)
    {
    }
}