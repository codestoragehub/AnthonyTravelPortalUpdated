using Microsoft.AspNetCore.Mvc;

namespace AnthonyTravelPortal.UI.Views.Shared.Components.Logout
{
   
    public class LogoutViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
