using Blackpool.Zengenti.CMS.Models.Canvas.Tables;
using Microsoft.AspNetCore.Html;

namespace RazorPageWeddingWebsite.Helpers.Interfaces
{
    public interface ITableHelper
    {
        IHtmlContent BuildHtmlTable(Table table);
    }
}
