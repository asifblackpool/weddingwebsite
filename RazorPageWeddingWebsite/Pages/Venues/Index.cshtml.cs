using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPageWeddingWebsite.Core.Models;
using Blackpool.Zengenti.CMS.Models.Weddings;
using RazorPageWeddingWebsite.Services.Interfaces;

using RazorPageWeddingWebsite.Services.Breadcrumb;
using RazorPageWeddingWebsite.Core.Interfaces;


namespace RazorPageWeddingWebsite.Pages.Venues
{
    public class IndexModel : BasePageModel<GettingMarried>
    {

        public IndexModel(ILogger<BasePageModel<GettingMarried>> logger,
                            IDataService<GettingMarried> dataService,
                            IContentRepository contentRepository, BreadcrumbService breadcrumb) : base(logger, dataService, contentRepository, breadcrumb) { }

        public override async Task OnGetAsync() // Default handler
        {
            ViewData["Title"] = "Venue Homepage";
            ViewData["Model"] = null;

            await base.OnGetByPathAsync(base.UrlSiteViewPath);
            Items = Items.Take(1).ToList();
            LogAction("Getting Married Venue (Homepage) data loaded");
            //var temp = base._breadcrumb.GetBreadcrumbs(this.HttpContext);
        }

    }


}
