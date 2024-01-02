using PremiumAccess.Domain.Entities;
using PremiumAccess.Domain.Features.AccessStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PremiumAccess.Application.Features.AccessStore;

public class ContentManagementService : IContentManagementService
{
    private readonly IApplicationUnitOfWork _unitOfWork;
    public ContentManagementService(IApplicationUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task CreateContentAsync(string title, string category, uint duration)
    {
        bool isDuplicateTitle = await _unitOfWork.ContentRepository.
           IsTitleDuplicateAsync(title);

        if (isDuplicateTitle)
            throw new InvalidOperationException();

        Content content = new Content
        {
            Title = title,
            Duration = duration,
            Category = category
        };

        _unitOfWork.ContentRepository.Add(content);
        await _unitOfWork.SaveAsync();
    }

    public Task CreateContentAsync(string title, uint duration, string category)
    {
        throw new NotImplementedException();
    }

    public async Task<(IList<Content> records, int total, int totalDisplay)> GetPagedContentsAsync(int pageIndex, int pageSize, string searchText, string sortBy)
    {
        return await _unitOfWork.ContentRepository.GetTableDataAsync(searchText, sortBy,
            pageIndex, pageSize);
    }
}
