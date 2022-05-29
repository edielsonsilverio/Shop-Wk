using Microsoft.AspNetCore.Mvc;

namespace Shop.WebApp.MVC.Controllers;

public class QuemSomosController : Controller
{
    [Route("quemsomos")]
    public IActionResult Index()
    {
        return View();
    }
}
