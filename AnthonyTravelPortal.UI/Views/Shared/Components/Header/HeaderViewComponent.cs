using Microsoft.AspNetCore.Mvc;

namespace AnthonyTravelPortal.UI.Views.Shared.Components.Header
{
    public class HeaderViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
