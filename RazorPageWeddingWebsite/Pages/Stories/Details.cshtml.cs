using RazorPageWeddingWebsite.Helpers;
using Blackpool.Zengenti.CMS.Models.Weddings;
using RazorPageWeddingWebsite.Services.Interfaces;
using RazorPageWeddingWebsite.Services.Breadcrumb;
using RazorPageWeddingWebsite.Core.Models;
using RazorPageWeddingWebsite.Core.Interfaces;

namespace RazorPageWeddingWebsite.Pages.Stories
{
    public class DetailsModel : BasePageModel<GettingMarriedBlog>
    {

        public DetailsModel(ILogger<BasePageModel<GettingMarriedBlog>> logger,
                            IDataService<GettingMarriedBlog> dataService,
                            IContentRepository contentRepository, BreadcrumbService breadcrumb) : base(logger, dataService, contentRepository, breadcrumb) { }


        public override async Task OnGetAsync()
        {
            ViewData["Title"] = "stories details page";
            ViewData["Model"] = null;

            string? path = HttpContext.Request.Path;
            path = (path == null) ? string.Empty : path.RemoveFileExtension(FILE_Extension.ASPX);
            if (path != null)
            {
                await base.OnGetByPathAsync(path);
            }
            else
            {
                await base.OnGetAsync();
            }
            Items = Items.Take(1).ToList();
            LogAction("Getting Stories specific data loaded");
        }



    }
}

