﻿using Blackpool.Zengenti.CMS.Models.Canvas.Panels;
using Blackpool.Zengenti.CMS.Models.GenericTypes;
using Microsoft.AspNetCore.Html;
using RazorPageWeddingWebsite.Core.Services.ContentHandling.Interfaces;
using RazorPageWeddingWebsite.Helpers.Interfaces;
using RazorPageWeddingWebsite.Helpers.Wrappers;

namespace RazorPageWeddingWebsite.Core.Services.ContentHandling.Handlers
{
    public class CanvasPanelHandler : IContentHandler
    {
        private readonly ISerializationHelper _serializer;
        private readonly ICanvasPanelHelper _panelHelper;

        public CanvasPanelHandler(ISerializationHelper serializer, ICanvasPanelHelper panelHelper)
        {
            _serializer = serializer;
            _panelHelper = panelHelper;
        }

        public bool CanHandle(string className) => className == typeof(CanvasPanel).Name;

        public async Task<IHtmlContent> HandleAsync(SerialisedItem item)
        {
            var htmlContent = new HtmlContentBuilder();

            try
            {
                // Deserialize the panel content
                var panel = await _serializer.DeserializeAsync<CanvasPanel>(item);

                if (panel != null)
                {
                    // Build and append the panel HTML
                    var panelHtml = _panelHelper.BuildPanel(panel);
                    htmlContent.AppendHtml(panelHtml);
                }
            }
            catch (Exception ex)
            {
                htmlContent.AppendHtml($"<!-- Error processing CanvasPanelComplex handler: {ex.Message} -->");
            }

            return htmlContent;
        }
    }
}
