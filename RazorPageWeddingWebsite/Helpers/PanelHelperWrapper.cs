﻿using Blackpool.Zengenti.CMS.Models.Canvas.Helpers;
using Blackpool.Zengenti.CMS.Models.Canvas.Panels;
using Microsoft.AspNetCore.Html;
using RazorPageWeddingWebsite.Helpers.Interfaces;
using System.Web;

namespace RazorPageWeddingWebsite.Helpers
{
    public class PanelHelperWrapper : IPanelHelper
    {
        public IHtmlContent BuildPanel(CanvasPanelComplex panel)
        {
            string htmlString = PanelHelper.BuildPanel(panel); 
            return new HtmlString(htmlString);
        }
    }
}
