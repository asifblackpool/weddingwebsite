using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPageWeddingWebsite.Helpers;
using RazorPageWeddingWebsite.Core.Models;
using Blackpool.Zengenti.CMS.Models.Weddings;
using RazorPageWeddingWebsite.Services.Interfaces;
using Zengenti;
using Zengenti.Contensis.Delivery;
using RazorPageWeddingWebsite.Services.Breadcrumb;
using RazorPageWeddingWebsite.Core.Interfaces;

namespace RazorPageWeddingWebsite.Pages.Home
{
    public class DetailsModel : BasePageModel<GettingMarried>
    {

        public DetailsModel(ILogger<BasePageModel<GettingMarried>> logger,
                            IDataService<GettingMarried> dataService,
                            IContentRepository contentRepository, BreadcrumbService breadcrumb) : base(logger, dataService, contentRepository, breadcrumb) { }

        public override async Task OnGetAsync()
        {
            ViewData["Title"] = "Home details page";
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
            LogAction("Getting Home details specific data loaded");
        }



    }
}

