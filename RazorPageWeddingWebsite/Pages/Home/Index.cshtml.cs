using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPageWeddingWebsite.Models;
using Blackpool.Zengenti.CMS.Models.Weddings;
using RazorPageWeddingWebsite.Services.Interfaces;

using RazorPageWeddingWebsite.Services.Breadcrumb;

namespace RazorPageWeddingWebsite.Pages;

public class IndexModel : BasePageModel<GettingMarriedHome>
{

    public IndexModel(ILogger<BasePageModel<GettingMarriedHome>> logger, 
                      IDataService<GettingMarriedHome> dataService, BreadcrumbService breadcrumb) : base(logger,dataService, breadcrumb) { }
    // Set the model
    //public IEnumerable<Zengenti.Contensis.Delivery.Node> BlogNodes { get; set; }  // <-- Add this property

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


