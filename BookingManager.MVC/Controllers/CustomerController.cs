using BookingManager.Application.Abstractions;
using BookingManager.DAL.Entities;
using BookingManager.DAL.Repositories;
using BookingManager.MVC.Mappers;
using BookingManager.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BookingManager.MVC.Controllers
{
    public class CustomerController(ICustomerRepository repository) : Controller
    {
        public IActionResult Index()
        {
            //List<Customer> clients = repository.GetAll();
            //List<CustomerIndexViewModel> model = clients
            //    // mapper chaque Customer en CustomerIndexViewModel
            //    .Select(ToViewModelMappers.ToCustomerIndex).ToList();
            return View(repository.GetAll().Select(ToViewModelMappers.ToCustomerIndex));
        }
    }
}
