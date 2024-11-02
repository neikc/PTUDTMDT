using Microsoft.AspNetCore.Mvc;

namespace PTUDTMDT.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Error()
        {
            return View();
        }
    }
}
