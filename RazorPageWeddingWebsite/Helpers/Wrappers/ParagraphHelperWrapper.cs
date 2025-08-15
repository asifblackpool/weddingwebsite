﻿using Blackpool.Zengenti.CMS.Models.Canvas.Helpers;
using Blackpool.Zengenti.CMS.Models.Canvas.Paragraphs;
using Microsoft.AspNetCore.Html;
using RazorPageWeddingWebsite.Helpers.Interfaces;

namespace RazorPageWeddingWebsite.Helpers.Wrappers
{
    public class ParagraphHelperWrapper : IParagraphHelper
    {
      

        public async Task<IHtmlContent> FragmentParagraphAsync(FragmentParagraph fp)
        {
            if (fp == null)
            {
                throw new ArgumentNullException(nameof(fp));
            }

            try
            {
                // Execute the CPU-bound operation on a thread pool thread
                var htmlContent = await Task.Run(() => ParagraphHelper.FragmentParagraph(fp))
                    .ConfigureAwait(false);

                // Ensure we return a proper IHtmlContent (wrap string if needed)
                return new HtmlString(htmlContent?.ToString());
            }
            catch (Exception ex)
            {
       
                return new HtmlString(string.Empty); // Return safe empty content
            }
        }
    }
}
