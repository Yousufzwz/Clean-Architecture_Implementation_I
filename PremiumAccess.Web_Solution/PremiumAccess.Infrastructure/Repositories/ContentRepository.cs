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

    public async Task<bool> IsTitleDuplicateAsync(string title, Guid? id = null)
    {
        if (id.HasValue)
        {
            return (await GetCountAsync(x => x.Id != id.Value && x.Title == title)) > 0;
        }
        else
        {
            return (await GetCountAsync(x => x.Title == title)) > 0;
        }
    }

    public async Task<(IList<Content> records, int total, int totalDisplay)>
        GetTableDataAsync(string searchText, string orderBy,
            int pageIndex, int pageSize)
    {
        return await GetDynamicAsync(x => x.Title.Contains(searchText),
            orderBy, null, pageIndex, pageSize, true);
    }
}
