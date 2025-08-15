using Blackpool.Zengenti.CMS.Models.Interfaces;

namespace RazorPageWeddingWebsite.Core.Interfaces
{
    public interface IContentService
    {
        List<IGettingMarried> GetChildPages(string parentUri);
    }
}
