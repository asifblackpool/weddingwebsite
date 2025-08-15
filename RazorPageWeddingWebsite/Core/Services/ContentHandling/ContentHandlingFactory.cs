﻿using RazorPageWeddingWebsite.Core.Services.ContentHandling.Interfaces;

namespace RazorPageWeddingWebsite.Core.Services.ContentHandling
{
    public class ContentHandlerFactory
    {
        private readonly IEnumerable<IContentHandler> _handlers;

        public ContentHandlerFactory(IEnumerable<IContentHandler> handlers)
        {
            _handlers = handlers;
        }

        public IContentHandler GetHandler(string className)
        {
            return _handlers.FirstOrDefault(h => h.CanHandle(className))
                   ?? throw new InvalidOperationException($"No handler found for {className}");
        }
    }
}
