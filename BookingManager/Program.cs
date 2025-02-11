using BookingManager.DAL;
using BookingManager.DAL.Entities;
using Microsoft.EntityFrameworkCore;

using HotelContext ctx = new HotelContext();

// Permet de ne pas tracker les éléments chargés
// améliore les performances
Customer customer = ctx.Customers.AsNoTracking().ToList()[3];
customer.LastName = "Person";
ctx.SaveChanges();