﻿
using Blackpool.Zengenti.CMS.Models.Canvas.Paragraphs;
using Blackpool.Zengenti.CMS.Models.GenericTypes;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using RazorPageWeddingWebsite.Helpers.Interfaces;
using RazorPageWeddingWebsite.Helpers.Wrappers;
using Blackpool.Zengenti.CMS.Helpers;
using RazorPageWeddingWebsite.Core.Services.ContentHandling.Interfaces;
using Sprache;

namespace RazorPageWeddingWebsite.Models.Services.ContentHandling.Handlers
{
        public class FragmentParagraphHandler : IContentHandler
        {
            private readonly ISerializationHelper _serializationHelper;
            private readonly IParagraphHelper _paragraphHelper;
            private readonly INavigationLinkHelper _navigationLinkHelper;

            public FragmentParagraphHandler(
                ISerializationHelper serializationHelper,
                IParagraphHelper paragraphHelper,
                INavigationLinkHelper navigationLinkHelper)
            {
                _serializationHelper = serializationHelper;
                _paragraphHelper = paragraphHelper;
                _navigationLinkHelper = navigationLinkHelper;
            }

            public bool CanHandle(string className)
                => className == typeof(FragmentParagraph).Name;

            public async Task<IHtmlContent> HandleAsync(SerialisedItem item)
            {
            // Deserialize the fragment paragraph
                string result = string.Empty;
                string linkUrl = string.Empty;
                var fragment = await Task.Run(() =>
                    _serializationHelper.Deserialize<FragmentParagraph>(item));

                // Process navigation links
                if (fragment != null)
                {
                    var temp = await _navigationLinkHelper.GetLinkUrlAsync(fragment);
                    if (temp != null) { 
                        linkUrl = temp.Url.SafeString();
                        temp.Url = linkUrl;
                        fragment = temp;
                    }
                }
              

                // Generate HTML fragment
                var htmlFragment = await _paragraphHelper.FragmentParagraphAsync(fragment);

                if (htmlFragment != null)
                {
                    string? temp1 = htmlFragment?.ToString();
                    result = (temp1 != null) ? temp1.Replace("\r\n", "<br/>").Replace("\n", "<br/>") : result;

                }

            // Wrap in paragraph tag
            var pTag = new TagBuilder("p");
                pTag.InnerHtml.AppendHtml(result);

                return pTag;
            }
        }
 
}
