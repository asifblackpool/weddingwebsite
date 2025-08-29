using Blackpool.Zengenti.CMS.Models.Canvas.Panels;
using Microsoft.AspNetCore.Html;

namespace RazorPageWeddingWebsite.Helpers.Interfaces
{
    public interface ICanvasPanelHelper
    {
        IHtmlContent BuildPanel(CanvasPanel panel);
    }
}
