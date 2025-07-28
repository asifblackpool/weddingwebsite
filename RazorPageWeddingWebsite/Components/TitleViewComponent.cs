using Blackpool.Zengenti.CMS.Models.Weddings.Base;
using Microsoft.AspNetCore.Mvc;
using RazorPageWeddingWebsite.Models;

namespace RazorPageWeddingWebsite.Components
{
   
    public class TitleViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var temp = ViewData["Model"] as GettingMarriedBase;
            var model = new LayoutModel
            {
                Title = (temp != null && temp.PageTitle != null) ? temp.PageTitle : "No Title",
                IsHomePage = ViewContext.RouteData.Values["page"]?.ToString() == "/Home/Index"
            };
            return View(model);
        }
    }
}
