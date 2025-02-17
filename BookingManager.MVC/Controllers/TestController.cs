using BookingManager.MVC.Models;
using Humanizer;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;

namespace BookingManager.MVC.Controllers
{
    public class TestController(SmtpClient smtpClient) : Controller
    {
        //private readonly SmtpClient smtpClient;
        //public TestController(SmtpClient client)
        //{
        //    smtpClient = client;
        //}

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
        public IActionResult Contact(
            [FromForm] ContactFormViewModel form
        )
        {
            if(ModelState.IsValid)
            {
                // faire un traitement pour envoyer un email
                
                MailMessage message = new();
                message.Subject = form.Sujet;
                message.Body = form.Message;
                message.From = new MailAddress("test@khunly.be");
                message.To.Add(new MailAddress("lykhun@gmail.com"));
                smtpClient.Send(message);

                // redirection
                return RedirectToAction("Index", "Home");
            }
            // sinon revenir à la vue
            return View();
        }
    }
}
