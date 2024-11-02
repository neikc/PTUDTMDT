using Microsoft.AspNetCore.Mvc;

namespace PTUDTMDT.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult ShopIndex()
        {
            return View();
        }
    }
}
