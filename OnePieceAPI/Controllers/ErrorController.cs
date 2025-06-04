using Microsoft.AspNetCore.Mvc;

namespace OnePieceAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ErrorController : ControllerBase
    {

        [Route("/error")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult HandleError() =>
            Problem(title: "Error interno", statusCode: 500);
    }
}
