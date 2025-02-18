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
            if(!ModelState.IsValid)
            // oui 
            {
                // revenir sur le formulaire
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
            // créer un username
            string prefix = (form.LastName[..2] + form.FirstName[..2]).ToUpper();
            int count = repository.CountByUsername(prefix);
            c.Username = prefix + count.ToString().PadLeft(4, '0');
            // créer un password
            string pwd = Guid.NewGuid().ToString().Replace("-", "")[..10];
            byte[] hashedPwd = SHA512.HashData(Encoding.UTF8.GetBytes(pwd + form.Email));
            c.Password = hashedPwd;
            // sauver dans la db
            repository.Add(c);
            // envoyer un email
            MailMessage mail = new MailMessage
            {
                Subject = "Merci pour votre inscription",
                Body = $"Votre mot de passe : {pwd}",
                From = new MailAddress("noreply@test.com"),
            };
            mail.To.Add(new MailAddress(form.Email));
            smtpClient.Send(mail);
            // revenir sur autre page
            return RedirectToAction("Index");
        }
    }
}
