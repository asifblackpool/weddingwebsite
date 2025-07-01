using RazorPageWeddingWebsite.Constants;
using RazorPageWeddingWebsite.Models;
using Blackpool.Zengenti.CMS.Models.Weddings;
using RazorPageWeddingWebsite.Services.Interfaces;
using Zengenti.Contensis.Delivery;
using System.Globalization;

namespace RazorPageWeddingWebsite.Services.Breadcrumb
{


    // Services/BreadcrumbService.cs
    public class BreadcrumbService
    {
        private readonly List<BreadcrumbItem> _items = new List<BreadcrumbItem>();
        private bool _autoGenerate = true;

        public void AddItem(string title, string? url = null)
        {
            _items.Add(new BreadcrumbItem { Title = title, Url = url });
            _autoGenerate = false; // Manual addition disables auto-generation
        }

        public void Reset()
        {
            _items.Clear();
            _autoGenerate = true;
        }

        public void EnableAutoGeneration()
        {
            _autoGenerate = true;
        }

        public void DisableAutoGeneration() => _autoGenerate = false;

        public List<BreadcrumbItem> GetBreadcrumbs(HttpContext context)
        {
            var finalItems = new List<BreadcrumbItem>();

            // Always start with Home
            finalItems.Add(new BreadcrumbItem { Title = "Home", Url = "/" });

            if (_autoGenerate)
            {
                // Auto-generate from route
                var path = context.Request.Path.Value;
                List<string> segments = (path != null) ? path.Split('/')
                    .Where(s => !string.IsNullOrEmpty(s))
                    .ToList() : new List<string>();

                string accumulatedPath = "";

                foreach (var segment in segments)
                {
                    accumulatedPath += $"/{segment}";
                    var title = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(segment.Replace("-", " "));
                    finalItems.Add(new BreadcrumbItem { Title = title, Url = accumulatedPath });
                }
            }
            else
            {
                // Use manually added items (but still ensure Home is first)
                if (!_items.Any() || _items[0].Title != "Home")
                {
                    finalItems.AddRange(_items);
                }
                else
                {
                    finalItems = _items.ToList();
                }
            }

            // Mark the last item as active (no link)
            if (finalItems.Count > 0)
            {
                finalItems.Last().Url = null;
            }

            return finalItems;
        }
    }

}
