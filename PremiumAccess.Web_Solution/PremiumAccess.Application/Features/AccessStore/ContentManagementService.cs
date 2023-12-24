using PremiumAccess.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PremiumAccess.Application.Features.AccessStore;

public class ContentManagementService
{
    private readonly IApplicationUnitOfWork _unitOfWork;
    public ContentManagementService(IApplicationUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task CreateContentAsync(string title, uint duration, string category)
    {
        Content content = new Content
        {
            Title = title,
            Duration = duration,
            Category = category
        };

        _unitOfWork.ContentRepository.Add(content);
        await _unitOfWork.SaveAsync();
    }
}
