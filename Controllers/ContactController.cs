using Microsoft.AspNetCore.Mvc;

namespace PTUDTMDT.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Contact()
        {
            return View();
        }
    }
}
