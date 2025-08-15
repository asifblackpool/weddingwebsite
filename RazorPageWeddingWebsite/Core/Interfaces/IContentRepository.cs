﻿using Blackpool.Zengenti.CMS.Models.Interfaces;
using Zengenti.Contensis.Delivery;

namespace RazorPageWeddingWebsite.Core.Interfaces
{
    public interface IContentRepository
    {
        //List<T> GetChildEntries<T>(string parentUri);
        List<T> GetChildEntries<T>(string parentUri) where T : class, IGettingMarried;
    }
}
