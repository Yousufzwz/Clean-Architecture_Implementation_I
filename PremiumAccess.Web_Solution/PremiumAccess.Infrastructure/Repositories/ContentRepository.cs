using Microsoft.EntityFrameworkCore;
using PremiumAccess.Domain.Entities;
using PremiumAccess.Domain.Entities.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PremiumAccess.Infrastructure.Repositories;

public class ContentRepository : Repository<Content, Guid>, IContentRepository
{
    public ContentRepository(IApplicationDbContext context) : base((DbContext)context)
    {
    }
}
