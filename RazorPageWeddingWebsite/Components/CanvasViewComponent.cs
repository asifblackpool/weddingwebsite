using Blackpool.Zengenti.CMS.Models.Weddings;
using Microsoft.AspNetCore.Mvc;

namespace RazorPageWeddingWebsite.Components
{
    public class CanvasViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(GettingMarried model)
        {
            // You can modify the model here if needed
            if (model != null)
            {
                var x = model.Canvas;
                return View(model.GetSerialisedCanvas());
            }

            return View();
        }

        // Alternative async version if you need to do async work
        /*
        public async Task<IViewComponentResult> InvokeAsync(GreetingModel model)
        {
            // Do async work if needed
            return View(model);
        }
        */
    }
}
