
using Blackpool.Zengenti.CMS.Models.Canvas.Lists;
using Microsoft.AspNetCore.Html;
using RazorPageWeddingWebsite.Helpers.Html;

namespace RazorPageWeddingWebsite.Helpers.Interfaces
{
    public interface IContentFragmentHelper
    {
        Task<IHtmlContent> BuildHtmlFragmentAsync(List<ContentFragment> fragments, string wrapperFormat);
        IHtmlContent BuildHtmlFragment(List<ContentFragment> fragments, string wrapperFormat);
    }
}
