using Microsoft.AspNetCore.Mvc;

namespace PTUDTMDT.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Cart()
        {
            return View();
        }
    }
}
