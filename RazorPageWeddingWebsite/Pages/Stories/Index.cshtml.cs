using Microsoft.AspNetCore.Mvc.RazorPages;
using Blackpool.Zengenti.CMS.Models.Weddings;
using RazorPageWeddingWebsite.Services.Interfaces;

using RazorPageWeddingWebsite.Services.Breadcrumb;
using RazorPageWeddingWebsite.Core.Models;
using RazorPageWeddingWebsite.Core.Interfaces;


namespace RazorPageWeddingWebsite.Pages.Stories
{
    public class IndexModel : BasePageModel<GettingMarriedBlog>
    {

        public IndexModel(ILogger<BasePageModel<GettingMarriedBlog>> logger,
                          IDataService<GettingMarriedBlog> dataService,
                          IContentRepository contentRepository, BreadcrumbService breadcrumb) : base(logger, dataService, contentRepository, breadcrumb) { }


        public override async Task OnGetAsync() // Default handler
        {
            ViewData["Title"] = "Stories Homepage";
            

            await base.OnGetByPathAsync(base.UrlSiteViewPath);
            Items = Items.Take(1).ToList();
            ViewData["Children"] = base.GetChildEntries<GettingMarriedBlog>(Items[0].Sys.Uri);
            LogAction("Getting Married Stories (Homepage) data loaded");
            //var temp = base._breadcrumb.GetBreadcrumbs(this.HttpContext);
        }

    }


}
