using Microsoft.AspNetCore.Mvc;

namespace AnthonyTravelPortal.UI.Views.Shared.Components.Footer
{
    public class FooterViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }

}
