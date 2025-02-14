using BookingManager.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookingManager.MVC.Controllers
{
    public class TestController : Controller
    {
        public string Hello([FromQuery] string nom)
        {
            return $"Hello {nom} !!";
        }

        // [Route(@"[controller]/toto/{heure}")]
        public ViewResult Pause([FromRoute]int id)
        {
            int minutesRestantes =
                (id - DateTime.Now.Hour - 1) * 60 
                + (60 - DateTime.Now.Minute); 
            return View(minutesRestantes);
        }

        // méthode pour afficher le formulaire
        public ViewResult Contact()
        {
            return View();
        }

        // méthode pour traiter le formulaire
        [HttpPost]
        public IActionResult Contact([FromForm] ContactFormViewModel form)
        {
            if(ModelState.IsValid)
            {
                // faire un traitement pour envoyer un email


                // redirection
                return RedirectToAction("Index", "Home");
            }
            // sinon revenir à la vue
            return View();
        }
    }
}
