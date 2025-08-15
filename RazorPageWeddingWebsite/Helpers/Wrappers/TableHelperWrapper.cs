using Blackpool.Zengenti.CMS.Models.Canvas.Helpers;
using Blackpool.Zengenti.CMS.Models.Canvas.Panels;
using Blackpool.Zengenti.CMS.Models.Canvas.Tables;
using Microsoft.AspNetCore.Html;
using RazorPageWeddingWebsite.Helpers.Interfaces;

namespace RazorPageWeddingWebsite.Helpers
{
    public class TableHelperWrapper : ITableHelper
    {
        public IHtmlContent BuildHtmlTable(Table table)
        {
            string htmlString = TableHelper.BuildHtmlTable(table);
            return new HtmlString(htmlString);
        }

      
    }
}
