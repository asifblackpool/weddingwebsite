using HtmlAgilityPack;

namespace RazorPageWeddingWebsite.Helpers.Interfaces
{
    public interface IHtmlTransformation
    {
        Task ApplyAsync(HtmlDocument document);
    }
}
