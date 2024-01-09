using PremiumAccess.Domain.Features.AccessStore;
using PremiumAccess.Infrastructure;
using System.Web;

namespace PremiumAccess.Web.Areas.Admin.Models;

public class ContentListModel
{
    private IContentManagementService _contentManagementService;

    public ContentListModel()
    {
    }

    public ContentListModel(IContentManagementService contentManagementService)
    {
        _contentManagementService = contentManagementService;
    }

    public async Task<object> GetPagedContentsAsync(DataTablesAjaxRequestUtility dataTablesUtility)
    {
        var data = await _contentManagementService.GetPagedContentsAsync(
            dataTablesUtility.PageIndex,
            dataTablesUtility.PageSize,
            dataTablesUtility.SearchText,
            dataTablesUtility.GetSortText(new string[] { "Title", "Category" }));

        return new
        {
            recordsTotal = data.total,
            recordsFiltered = data.totalDisplay,
            data = (from record in data.records
                    select new string[]
                    {
                                HttpUtility.HtmlEncode(record.Title),
                                HttpUtility.HtmlEncode(record.Category),
                                record.Duration.ToString(),
                                record.Id.ToString()
                    }
                ).ToArray()
        };
    }

    internal async Task DeleteContentAsync(Guid id)
    {
        await _contentManagementService.DeleteContentAsync(id);
    }
}
