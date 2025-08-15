using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPageWeddingWebsite.Services.Interfaces;
using RazorPageWeddingWebsite.Services.Breadcrumb;
using RazorPageWeddingWebsite.Core.Models;
using Blackpool.Zengenti.CMS.Models.Weddings;
using RazorPageWeddingWebsite.Core.Interfaces;

namespace RazorPageWeddingWebsite.Pages;

public class IndexModel : BasePageModel<GettingMarriedHome>
{

    public IndexModel(ILogger<BasePageModel<GettingMarriedHome>> logger,
                      IDataService<GettingMarriedHome> dataService,
                      IContentRepository contentRepository, BreadcrumbService breadcrumb) : base(logger, dataService, contentRepository, breadcrumb) { }


    public override async Task OnGetAsync() // Default handler
    {
        ViewData["Title"] = "Homepage";
      

        await base.OnGetAsync();
        Items = Items.Take(1).ToList();
    
      LogAction("Getting Married Home data loaded");
    }

    public async void OnGetFeatured()
    {
        await base.OnGetAsync();
    }

}


