using Autofac;
using PremiumAccess.Domain.Features.AccessStore;

namespace PremiumAccess.Web.Areas.Admin.Models
{
    public class ContentCreateModel 
    {
        private ILifetimeScope _scope;
        private IContentManagementService _contentManagementService;
        public string Title { get; set; }
        public string Category { get; set; }
        public uint Duration { get; set; }


        public ContentCreateModel() { }


        public ContentCreateModel(IContentManagementService courseManagementService)
        {
            _contentManagementService = courseManagementService;
        }

        internal void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _contentManagementService = _scope.Resolve<IContentManagementService>();
        }

        internal async Task CreateContentAsync()
        {
            await _contentManagementService.CreateContentAsync(Title, Category, Duration);
        }
    }
}
