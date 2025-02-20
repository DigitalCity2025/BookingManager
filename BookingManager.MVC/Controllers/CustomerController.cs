using BookingManager.Application.Abstractions;
using BookingManager.DAL.Entities;
using BookingManager.DAL.Repositories;
using BookingManager.MVC.Mappers;
using BookingManager.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Transactions;

namespace BookingManager.MVC.Controllers
{
    public class CustomerController(ICustomerRepository repository, SmtpClient smtpClient) : Controller
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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CustomerCreateFormViewModel form) 
        {
            // verifier si le formulaire est invalide
            try
            {
                if (!ModelState.IsValid)
                // oui 
                {
                    // revenir sur le formulaire
                    return View(form);
                }
                // verifier que l'email est unique
                Customer? cu = repository.GetByEmail(form.Email);
                if (cu != null)
                {
                    ModelState.AddModelError(nameof(form.Email), "L'email existe déjà");
                    return View(form);
                }


                // non
                // traiter les données
                // mapper les données dans une entité
                Customer c = new Customer
                {
                    LastName = form.LastName,
                    FirstName = form.FirstName,
                    Email = form.Email,
                    PhoneNumber = form.PhoneNumber,
                };
                
                TempData["success"] = "Enreistrement OK";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return View(form);
            }
        }
    }
}
