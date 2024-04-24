using Microsoft.AspNetCore.Mvc;

namespace AnthonyTravelPortal.UI.Views.Shared.Components.Sidebar
{

    public class SidebarViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
