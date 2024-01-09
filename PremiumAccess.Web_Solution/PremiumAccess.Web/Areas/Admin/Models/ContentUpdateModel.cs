using Autofac;
using PremiumAccess.Domain.Entities;
using PremiumAccess.Domain.Features.AccessStore;
using System.ComponentModel.DataAnnotations;

namespace PremiumAccess.Web.Areas.Admin.Models;

public class ContentUpdateModel
{

    [Required]
    public Guid Id { get; set; }
    [Required]
    public string Title { get; set; }
    [Required, Range(0, 50000, ErrorMessage = "Duration should be between 0 & 50000")]
    public uint Duration { get; set; }
    public string Category { get; set; }

    private IContentManagementService _contentService;

    public ContentUpdateModel()
    {

    }

    public ContentUpdateModel(IContentManagementService contentService)
    {
        _contentService = contentService;
    }

    internal void Resolve(ILifetimeScope scope)
    {
        _contentService = scope.Resolve<IContentManagementService>();
    }

    internal async Task LoadAsync(Guid id)
    {
        Content content = await _contentService.GetContentAsync(id);
        if (content != null)
        {
            Id = content.Id;
            Title = content.Title;
            Duration = content.Duration;
            Category = content.Category;
        }
    }

    internal async Task UpdateContentAsync()
    {
        if (!string.IsNullOrWhiteSpace(Title)
            && Duration >= 0)
        {
            await _contentService.UpdateContentAsync(Id, Title, Category, Duration);
        }
    }
}
