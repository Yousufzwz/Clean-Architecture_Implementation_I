using PremiumAccess.Domain.Entities;
using PremiumAccess.Domain.Exceptions;
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
            throw new DuplicateTitleException();

        Content content = new Content
        {
            Title = title,
            Duration = duration,
            Category = category
        };

        _unitOfWork.ContentRepository.Add(content);
        await _unitOfWork.SaveAsync();
    }


    public async Task DeleteContentAsync(Guid id)
    {
        await _unitOfWork.ContentRepository.RemoveAsync(id);
        await _unitOfWork.SaveAsync();
    }

    public async Task<Content> GetContentAsync(Guid id)
    {
        return await _unitOfWork.ContentRepository.GetByIdAsync(id);
    }

    public async Task UpdateContentAsync(Guid id, string title, string category,
           uint duration)
    {
        bool isDuplicatTitle = await _unitOfWork.ContentRepository.
                IsTitleDuplicateAsync(title, id);

        if (isDuplicatTitle)
            throw new DuplicateTitleException();

        var course = await GetContentAsync(id);
        if (course is not null)
        {
            course.Title = title;
            course.Category = category;
            course.Duration = duration;
        }
        await _unitOfWork.SaveAsync();
    }


    public async Task<(IList<Content> records, int total, int totalDisplay)> GetPagedContentsAsync(int pageIndex, int pageSize, string searchText, string sortBy)
    {
        return await _unitOfWork.ContentRepository.GetTableDataAsync(searchText, sortBy,
            pageIndex, pageSize);
    }
}
