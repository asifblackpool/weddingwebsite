using Blackpool.Zengenti.CMS.Models.Canvas.Paragraphs;
using Microsoft.AspNetCore.Html;

namespace RazorPageWeddingWebsite.Helpers.Interfaces
{
    // IParagraphHelper.cs
    public interface IParagraphHelper
    {
   
        Task<IHtmlContent> FragmentParagraphAsync(FragmentParagraph fp);
    }
}
