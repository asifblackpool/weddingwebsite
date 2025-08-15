using Blackpool.Zengenti.CMS.Models.Interfaces;
using Blackpool.Zengenti.CMS.Models.Weddings;
using RazorPageWeddingWebsite.Core.Interfaces;

namespace RazorPageWeddingWebsite.Core.Services
{
    

    // Core/Services/ContentService.cs
    public class ContentService : IContentService
    {
        private readonly IContentRepository _repository;

        public ContentService(IContentRepository repository)
        {
            _repository = repository;
        }

        public List<IGettingMarried> GetChildPages(string parentUri)
        {
            return _repository.GetChildEntries<GettingMarried>(parentUri)
                       .Cast<IGettingMarried>()
                       .ToList();
        }
    }
}
