using Blackpool.Zengenti.CMS.Helpers;
using Blackpool.Zengenti.CMS.Models.Canvas.Images;
using Blackpool.Zengenti.CMS.Models.Canvas.Paragraphs;
using Blackpool.Zengenti.CMS.Models.GenericTypes;
using Microsoft.AspNetCore.Html;
using RazorPageWeddingWebsite.Core.Services.ContentHandling.Interfaces;
using RazorPageWeddingWebsite.Core.Services.ContentProcessing.Interfaces;
using RazorPageWeddingWebsite.Helpers.Serialisation;
using RazorPageWeddingWebsite.Helpers.Wrappers;

namespace RazorPageWeddingWebsite.Core.Services.ContentHandling.Handlers
{
    public class CanvasParagraphHandler : IContentHandler
    {
        private readonly ISerializationHelper _serializer;
        private readonly ITextProcessor _textProcessor;

        public CanvasParagraphHandler(ISerializationHelper serializer, ITextProcessor textProcessor)
        {
            _serializer = serializer;
            _textProcessor = textProcessor;
        }

        public bool CanHandle(string className) => className == typeof(CanvasParagraph).Name;

        public async Task<IHtmlContent> HandleAsync(SerialisedItem item)
        {
            var objItem = await _serializer.DeserializeAsync<CanvasParagraph>(item);

            var processedText = await _textProcessor.ProcessAsync(objItem?.Value);

            return new HtmlString($"<p class=\"shade-black\">{processedText}</p>");
        }
    }
}
