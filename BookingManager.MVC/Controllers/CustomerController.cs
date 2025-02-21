using BookingManager.Application.Abstractions.Business;
using BookingManager.Application.Abstractions.Repositories;
using BookingManager.Application.Exceptions;
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
    public class CustomerController(ICustomerService customerService) : Controller
    {
        public IActionResult Index([FromQuery]CustomerSearchFormViewModel model)
        {
            if(ModelState.IsValid)
            {
                model.Results = customerService.FindByKeyword(model.Search)
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
            if (!ModelState.IsValid)
            {
                // revenir sur le formulaire
                return View();
            }
            // mapper les données dans une entité
            Customer c = new Customer
            {
                LastName = form.LastName,
                FirstName = form.FirstName,
                Email = form.Email,
                PhoneNumber = form.PhoneNumber,
            };
            try
            {
                customerService.Create(c);
            }
            catch(DuplicateFieldException ex)
            {
                ModelState.AddModelError(ex.FieldName, ex.Message);
                return View();
            }
            catch(SmtpException)
            {
                TempData["error"] = "L'email n'a pas pu être envoyé";
                return View();
            }
            TempData["success"] = "Enregistrement OK";
            return RedirectToAction("Index");
            
        }

        public IActionResult Delete([FromRoute]int id)
        {
            try
            {
                customerService.Delete(id);
                return RedirectToAction("Index");
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
