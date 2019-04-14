using Microsoft.AspNetCore.Mvc;

namespace MyCourse.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            //return Content("Sono La Index Della Home");
            return View();
        }

        public IActionResult Detail(string id)
        {
            //return Content($"Sono Detail!!, ho ricevuto l'id {id}");
            return View();            
        }
    }
}