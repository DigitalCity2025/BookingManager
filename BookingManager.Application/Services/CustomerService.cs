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

        public void Delete(int id)
        {
            Customer? c = repository.GetById(id);
            if(c == null)
            {
                throw new KeyNotFoundException("le client n'existe pas");
            }
            else
            {
                c.Deleted = true;
                c.PhoneNumber = null;
                repository.Update(c);
            }
        }

        public IEnumerable<Customer> FindByKeyword(string? search)
        {
            return repository.FindByKeyword(search)
                .Where(c => !c.Deleted);
        }

        public Customer? GetById(int id)
        {
            Customer? customer = repository.GetById(id);
            if (customer is not null && customer.Deleted)
            {
                return null;
            }
            return customer;
        }

        public void Update(int id, string lastName, string firstName, string? password, string? phoneNumber)
        {
            Customer? customer = repository.GetById(id);
            if(customer == null)
            {
                throw new KeyNotFoundException();
            }
            customer.LastName = lastName;
            customer.FirstName = firstName;
            customer.PhoneNumber = phoneNumber;

            if(password != null)
            {
                byte[] hash = HashPassword(password, customer.Email);
                // if(hash.SequenceEqual(customer.Password))
                if(Encoding.UTF8.GetString(hash) == Encoding.UTF8.GetString(customer.Password))
                {
                    throw new DuplicateFieldException(
                        nameof(customer.Password), 
                        "Le mot de passe doit être différent"
                    );
                }
                customer.Password = hash;
            }
            repository.Update(customer);
        }
    }
}
