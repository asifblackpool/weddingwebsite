using Blackpool.Zengenti.CMS.Models.Canvas.Panels;
using Microsoft.AspNetCore.Html;

namespace RazorPageWeddingWebsite.Helpers.Interfaces
{
    public interface IPanelHelper
    {
        IHtmlContent BuildPanel(CanvasPanelComplex panel);
    }
}
