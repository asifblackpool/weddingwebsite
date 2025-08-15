﻿using Blackpool.Zengenti.CMS.Models.GenericTypes;
using Content.Modelling.Models.Components.Data;
using Microsoft.AspNetCore.Html;
using Newtonsoft.Json;
using RazorPageWeddingWebsite.Core.Services.ContentHandling.Interfaces;
using RazorPageWeddingWebsite.Helpers.Interfaces;

namespace RazorPageWeddingWebsite.Core.Services.ContentHandling.Handlers
{
    public class BGCTALinkHandler : IContentHandler
    {
        private readonly IBgCtaLinkRenderer _ctaRenderer;

        public BGCTALinkHandler(IBgCtaLinkRenderer ctaRenderer)
        {
            _ctaRenderer = ctaRenderer;
        }

        public bool CanHandle(string className)
        {
            return className == typeof(BGCTALink).Name;
        }

        public async Task<IHtmlContent> HandleAsync(SerialisedItem item)
        {
            if (item?.Content == null)
                return HtmlString.Empty;

            try
            {
                var result = JsonConvert.DeserializeObject<BGCTALink>(item.Content);
                if (result == null)
                    return HtmlString.Empty;

                return await _ctaRenderer.RenderBgCtaButtonAsync(result, "pink-button");
            }
            catch (Exception ex)
            {
                // Log error here
                return new HtmlString($"<!-- Error processing BGCTALink: {ex.Message} -->");
            }
        }
    }
}
