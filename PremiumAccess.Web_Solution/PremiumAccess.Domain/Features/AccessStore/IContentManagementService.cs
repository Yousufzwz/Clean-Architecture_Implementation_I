using PremiumAccess.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PremiumAccess.Domain.Features.AccessStore;

public interface IContentManagementService
{
    Task CreateContentAsync(string title, string category, uint duration);
    Task<(IList<Content> records, int total, int totalDisplay)> GetPagedContentsAsync(int pageIndex, int pageSize, string searchText, string sortBy);
}
