﻿using Blackpool.Zengenti.CMS.Models.GenericTypes;
using Microsoft.AspNetCore.Html;

namespace RazorPageWeddingWebsite.Core.Services.ContentHandling.Interfaces
{
    public interface IContentHandler
    {
        bool CanHandle(string className);

        Task<IHtmlContent> HandleAsync(SerialisedItem item);
    }
}
