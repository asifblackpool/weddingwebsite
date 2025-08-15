using Microsoft.AspNetCore.Html;

namespace RazorPageWeddingWebsite.Helpers.Interfaces
{
    public interface IFormHelper
    {
        IHtmlContent TagBuilder(string formType, string contentTypeId);
    }
}
