using Microsoft.AspNetCore.Mvc;

namespace PTUDTMDT.Controllers
{
    public class CheckoutController : Controller
    {
        public IActionResult Checkout()
        {
            return View();
        }
    }
}
