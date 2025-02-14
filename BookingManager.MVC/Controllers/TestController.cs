using Microsoft.AspNetCore.Mvc;

namespace BookingManager.MVC.Controllers
{
    public class TestController : Controller
    {
        public string Hello([FromQuery] string nom)
        {
            return $"Hello {nom} !!";
        }
    }
}
