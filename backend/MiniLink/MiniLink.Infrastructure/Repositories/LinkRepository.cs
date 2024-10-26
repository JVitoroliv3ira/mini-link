using Microsoft.EntityFrameworkCore;
using MiniLink.Domain.Models;
using MiniLink.Domain.Repositories;
using MiniLink.Infrastructure.Context;

namespace MiniLink.Infrastructure.Repositories;

public class LinkRepository : RepositoryBase<Link>, ILinkRepository
{
    public LinkRepository(ApplicationDbContext context) : base(context)
    {
    }
}