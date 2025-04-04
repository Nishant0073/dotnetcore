using Microsoft.AspNetCore.Mvc;

namespace ViewComponentExample.ViewComponents
{
    public class GridViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("Sample");
        }
    }
}
