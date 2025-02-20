using BookingManager.Application.Abstractions.Business;
using BookingManager.Application.Abstractions.Repositories;
using BookingManager.Application.Exceptions;
using BookingManager.DAL.Entities;
using System.Data;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Transactions;

namespace BookingManager.Application.Services
{
    public class CustomerService(
        ICustomerRepository repository, 
        SmtpClient smtpClient
    ) : ICustomerService
    {
        public Customer Create(Customer c) 
        {
            // verifier que l'email est unique
            Customer? cu = repository.GetByEmail(c.Email);
            if (cu != null)
            {
                throw new DuplicateFieldException("Email", "Cet email existe dejà");
            }

            // créer un username
            c.Username = CreateUsername(c);
            // créer un password
            string pwd = CreatePassword();
            c.Password = HashPassword(pwd, c.Email);
            // sauver dans la db
            using TransactionScope scope = new TransactionScope();
            Customer result = repository.Add(c);
            // envoyer un email
            SendRegistrationMail(c, pwd);
            // revenir sur autre page
            scope.Complete();
            return result;
        }

        private string CreateUsername(in Customer c)
        {
            string prefix = (c.LastName[..2] + c.FirstName[..2]).ToUpper();
            int count = repository.CountByUsername(prefix) + 1;
            return prefix + count.ToString().PadLeft(4, '0');
        }

        private string CreatePassword()
        {
            return Guid.NewGuid().ToString().Replace("-", "")[..10];
        }

        private byte[] HashPassword(string pwd, string salt)
        {
            return SHA512.HashData(Encoding.UTF8.GetBytes(pwd + salt));
        }

        private void SendRegistrationMail(Customer c, string password) 
        {
            MailMessage mail = new MailMessage
            {
                Subject = "Merci pour votre inscription",
                Body = $"Votre mot de passe : {password}",
                From = new MailAddress("noreply@test.com"),
            };
            mail.To.Add(new MailAddress(c.Email));
            smtpClient.Send(mail);
        }

    }
}
