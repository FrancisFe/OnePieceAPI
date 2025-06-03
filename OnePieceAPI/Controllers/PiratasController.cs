using Microsoft.AspNetCore.Mvc;

namespace OnePieceAPI.Controllers
{
    public class PiratasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
