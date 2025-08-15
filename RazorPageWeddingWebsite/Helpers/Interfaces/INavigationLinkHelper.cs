﻿using Blackpool.Zengenti.CMS.Models.Canvas.Paragraphs;

namespace RazorPageWeddingWebsite.Helpers.Interfaces
{
    // INavigationLinkHelper.cs
    public interface INavigationLinkHelper
    {
        Task<string> GetLinkUrlAsync(string url);

        Task<FragmentParagraph> GetLinkUrlAsync(FragmentParagraph fragment);
    }
}
