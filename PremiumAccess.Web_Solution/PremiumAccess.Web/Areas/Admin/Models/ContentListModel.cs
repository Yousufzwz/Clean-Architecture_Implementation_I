using PremiumAccess.Domain.Features.AccessStore;
using PremiumAccess.Infrastructure;
using System.Web;

namespace PremiumAccess.Web.Areas.Admin.Models;

public class ContentListModel
{
    private readonly IContentManagementService _contentService;

    public ContentListModel()
    {
    }

    public ContentListModel(IContentManagementService contentService)
    {
        _contentService = contentService;
    }

    public async Task<object> GetPagedContentsAsync(DataTablesAjaxRequestUtility dataTablesUtility)
    {
        var data = await _contentService.GetPagedContentsAsync(
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

}
