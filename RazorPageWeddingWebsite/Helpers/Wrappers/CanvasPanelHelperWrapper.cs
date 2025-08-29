﻿using Blackpool.Zengenti.CMS.Models.Canvas.Helpers;
using Blackpool.Zengenti.CMS.Models.Canvas.Panels;
using Microsoft.AspNetCore.Html;
using RazorPageWeddingWebsite.Helpers.Interfaces;
using System.Web;

namespace RazorPageWeddingWebsite.Helpers.Wrappers
{
    public class CanvasPanelHelperWrapper : ICanvasPanelHelper
    {
        public IHtmlContent BuildPanel(CanvasPanel panel)
        {
            string htmlString = CanvasPanelHelper.BuildPanel(panel);
            return new HtmlString(htmlString);
        }
    }
}
