using Microsoft.AspNetCore.Mvc;

namespace OnePieceAPI.Controllers
{
    [ApiController]
    public class ErrorController : ControllerBase
    {

        [Route("/error")]
        public IActionResult HandleError() =>
            Problem(title: "Error interno", statusCode: 500);
    }
}
