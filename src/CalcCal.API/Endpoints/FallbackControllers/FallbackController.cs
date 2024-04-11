using Microsoft.AspNetCore.Mvc;

namespace CalcCal.API.Endpoints.FallbackControllers;

public sealed class FallbackController : Controller
{
    public IActionResult Index() =>
        PhysicalFile(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "index.html"), "text/html");
}