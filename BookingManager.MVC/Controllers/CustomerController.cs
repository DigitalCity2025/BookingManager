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
        public IActionResult Index([FromQuery]CustomerSearchFormViewModel model)
        {
            if(ModelState.IsValid)
            {
                model.Results = repository.FindByKeyword(model.Search)
                    .Select(ToViewModelMappers.ToCustomerIndex).ToList();
            }
            return View(model);
        }
    }
}
