using Microsoft.AspNetCore.Mvc;

namespace PTUDTMDT.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult BlogIndex()
        {
            return View();
        }

        public IActionResult BlogDetail()
        {
            return View();
        }
    }
}
