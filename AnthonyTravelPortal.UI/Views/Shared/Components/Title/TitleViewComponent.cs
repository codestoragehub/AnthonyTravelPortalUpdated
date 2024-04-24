using Microsoft.AspNetCore.Mvc;

namespace AnthonyTravelPortal.UI.Views.Shared.Components.Title
{
    public class TitleViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
