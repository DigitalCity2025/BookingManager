using BookingManager.Application.Abstractions.Business;
using BookingManager.Application.Exceptions;
using BookingManager.DAL.Entities;
using BookingManager.MVC.Mappers;
using BookingManager.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;

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
            try
            {
                // mapper les données dans une entité
                Customer c = form.ToEntity();
                // demander au service de créer un client
                customerService.Create(c);
            }
            catch(DuplicateFieldException ex)
            {
                // ajouter une erreur dans le model State
                ModelState.AddModelError(ex.FieldName, ex.Message);
                return View();
            }
            catch(SmtpException)
            {
                //ajouter un message d'erreur temporaire
                TempData["error"] = "L'email n'a pas pu être envoyé";
                return View();
            }
            //ajouter un message de success temporaire
            TempData["success"] = "Enregistrement OK";
            return RedirectToAction("Index");
            
        }

        public IActionResult Update([FromRoute] int id)
        {
            Customer? customer = customerService.GetById(id);
            if(customer is null)
            {
                return NotFound();
            }
            CustomerEditFormViewModel model = customer.ToCustomerEditForm();
            return View(model);
        }

        [HttpPost]
        public IActionResult Update([FromRoute]int id, [FromForm]CustomerEditFormViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                customerService.Update(id, model.LastName, model.FirstName, model.Password, model.PhoneNumber);
                TempData["success"] = "OK";
                return RedirectToAction("Index");
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch(DuplicateFieldException ex)
            {
                ModelState.AddModelError(ex.FieldName, ex.Message);
                return View();
            }
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
