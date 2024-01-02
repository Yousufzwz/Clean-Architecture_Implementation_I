using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PremiumAccess.Domain.Entities.Repositories;

public interface IContentRepository : IRepositoryBase<Content, Guid>
{
    Task<bool> IsTitleDuplicateAsync(string title, Guid? id = null);

    Task<(IList<Content> records, int total, int totalDisplay)>
            GetTableDataAsync(string searchText, string orderBy,
                int pageIndex, int pageSize);
}
