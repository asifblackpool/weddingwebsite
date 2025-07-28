using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPageWeddingWebsite.Helpers;
using RazorPageWeddingWebsite.Services;
using RazorPageWeddingWebsite.Services.Breadcrumb;
using RazorPageWeddingWebsite.Services.Interfaces;

namespace RazorPageWeddingWebsite.Models
{
    public class BasePageModel<T> : PageModel where T : class
    {
        protected readonly ILogger<BasePageModel<T>> _logger;
        protected readonly IDataService<T> _dataService;
        protected readonly BreadcrumbService _breadcrumb;

        public string UrlSiteViewPath
        {
            get
            {
                string? path = HttpContext.Request.Path;
                path = (path == null) ? string.Empty : path.RemoveFileExtension(FILE_Extension.ASPX);
                return (path == null) ? string.Empty : path;
            }
        }

        public List<BreadcrumbItem> Breadcrumbs 
        {
                get { return _breadcrumb.GetBreadcrumbs(this.HttpContext);  }
        } 

        // Shared properties
        public string PageType => typeof(T).Name;
        public List<T> Items { get; protected set; } = new();

        // Constructor with DI
        public BasePageModel(ILogger<BasePageModel<T>> logger,IDataService<T> dataService, BreadcrumbService breadcrumb)
        {
            _logger = logger;
            _dataService = dataService;
            _breadcrumb = breadcrumb;
        }

        // Shared initialization
        public virtual async Task OnGetAsync()
        {
            _logger.LogInformation($"Loading {PageType} data");
            Items = await _dataService.GetAllAsync();
            ViewData["Title"] = $"{PageType}s - {DateTime.Now.Year}";
            StoreModel(Items);
        }

        public virtual async Task OnGetByPathAsync(string path)
        {
            _logger.LogInformation($"Loading {PageType} data");
            Items = await _dataService.GetAllAsync(path);
            ViewData["Title"] = $"{PageType}s - {DateTime.Now.Year}";
            StoreModel(Items);
       
        }

        private void StoreModel(List<T> items)
        {
            ViewData["Model"] = (items != null && items.Count > 0) ? items.Take(1).FirstOrDefault() : null;
            var temp = ViewData["Model"];
        }

        private void StoreTitle(List<T> items)
        {
            ViewData["Title"] = $"{PageType}s - {DateTime.Now.Year}";
            var temp = (items != null && items.Count > 0) ? items.Take(1).FirstOrDefault() : null;
            if (temp != null)
            {
                ViewData["Title"] = null;
            }
            
        }

        // Shared method
        protected void LogAction(string action)
        {
            _logger.LogInformation($"{PageType} action: {action}");
        }
    }
}
