using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PremiumAccess.Domain.Features.AccessStore;

public interface IContentManagementService
{
    Task CreateContentAsync(string title, string category, uint duration);
}
