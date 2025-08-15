using Blackpool.Zengenti.CMS.Models.Components;
using Microsoft.AspNetCore.Html;

namespace RazorPageWeddingWebsite.Helpers.Interfaces
{
    public interface IAccordionRenderer
    {
        IHtmlContent Render(string accordionName, List<AccordionContent> items);
    }
}
