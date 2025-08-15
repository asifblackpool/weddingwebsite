﻿using Blackpool.Zengenti.CMS.Models.Canvas.Helpers;
using Blackpool.Zengenti.CMS.Models.Canvas.Images.Base;
using Blackpool.Zengenti.CMS.Models.GenericTypes;
using Content.Modelling.Models.Canvas.Images;
using Microsoft.AspNetCore.Html;
using Newtonsoft.Json;
using RazorPageWeddingWebsite.Core.Services.ContentHandling.Interfaces;
using RazorPageWeddingWebsite.Helpers.Interfaces;


namespace RazorPageWeddingWebsite.Core.Services.ContentHandling.Handlers
{
    public class RightAlignedImageHandler : IContentHandler
    {
        private readonly IImageHelper _imageHelper;

        public RightAlignedImageHandler(IImageHelper imageHelper)
        {
            _imageHelper = imageHelper;
        }

        public bool CanHandle(string className)
        {
            return className == typeof(RightAlignedImage).Name;
        }

        public async Task<IHtmlContent> HandleAsync(SerialisedItem item)
        {
            if (item?.Content == null)
                return HtmlString.Empty;

            try
            {
                await Task.Yield(); // Ensure async context
                var settings = new JsonSerializerSettings
                {
                    Converters = new List<JsonConverter> { new CmsImageConverter() },
                    NullValueHandling = NullValueHandling.Ignore
                };

                RightAlignedImage? result = JsonConvert.DeserializeObject<RightAlignedImage>(item.Content, settings);

                if (result?.Image?.Asset?.Sys?.Uri != null)
                {
                    string? tempUrl = _imageHelper.GetImageUrl(uri: result.Image.Asset.Sys.Uri);
                    if (!string.IsNullOrEmpty(tempUrl))
                    {
                        result.Image.Asset.Sys.Uri = tempUrl;
                    }
                }

                return result != null ? new HtmlString(result.ToHtml()) : HtmlString.Empty;
            }
            catch (Exception ex)
            {
                // Log error here
                return new HtmlString($"<!-- Error processing RightAlignedImage: {ex.Message} -->");
            }
        }
    }
}
