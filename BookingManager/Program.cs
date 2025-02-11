using BookingManager.DAL;
using BookingManager.DAL.Entities;
using Microsoft.EntityFrameworkCore;

using HotelContext ctx = new HotelContext();

/*
 * -3. Afficher toutes les réservations faites en 2024.
-2. Afficher toutes les personnes dont le nom commence par D.
-1 Afficher toutes chambres du 1 étage. 
1. Trouver tous les clients (nom et prénom) qui ont réservé une chambre au mois de juin 2024.
2. Afficher le numéro et le prix de toutes les chambres avec l'option "Wifi gratuit".
3. Combien de réservations ont été faites en mars 2024 ?
4. Trouver les noms et prénoms des clients qui ont réservé la chambre la plus chère.
5. Afficher les noms des options qui sont disponibles dans au moins deux chambres.
6. Calculer le revenu total généré par chaque chambre en 2024 (somme des prix des réservations).
 
Afficher les chambres et leur options
 */

#region Exo -3
//List<Booking> result1 = ctx.Bookings
//    .Where(b => b.BookingDate.Year == 2024)
//    .ToList();

List<Booking> result1 = (from c in ctx.Bookings
                        where c.BookingDate.Year == 2024
                        select c).ToList();

Console.WriteLine("----- Exercice -3 ------");
foreach (Booking b in result1)
{
    Console.WriteLine($"{b.StartDate} {b.EndDate} {b.CustomerId} {b.RoomId}");
}
#endregion

#region Exo -2
//List<Customer> result2 = ctx.Customers
//    .Where(c => c.LastName.StartsWith("d"))
//    .ToList();
List<Customer> result2 = ctx.Customers
    .Where(c => EF.Functions.Like(c.LastName, "d%"))
    .ToList();
Console.WriteLine("----- Exercice -2 ------");
foreach (Customer c in result2)
{
    Console.WriteLine($"{c.LastName} {c.FirstName}");
}
#endregion

#region Exo -1
var result3 = ctx.Rooms.Where(r => r.Floor == 1).ToList();
Console.WriteLine("----- Exercice -1 -----");
foreach (Room r in result3)
{
    Console.WriteLine($"{r.Number} {r.Floor}");
}
#endregion

#region Exo 1
// bool reservationJuin2024 = ctx.Bookings
//    .Any(b => b.BookingDate.Year == 2024 && b.BookingDate.Month == 6);
// Console.WriteLine(reservationJuin2024);

//List<Customer> result4 = ctx.Customers
//    .Where(c => c.Bookings
//        .Any(b => b.BookingDate.Year == 2024 && b.BookingDate.Month == 6))
//    .ToList();

List<Customer> result4 = ctx.Bookings
    .Include(b => b.Customer)
    .Where(b => b.BookingDate.Year == 2024 && b.BookingDate.Month == 6)
    .GroupBy(b => b.Customer)
    .Select(g => g.Key)
    .ToList();
foreach(Customer c in result4)
{
    Console.WriteLine($"{c.LastName} {c.FirstName}");
}
#endregion

#region Exo 2
List<Room> result5 = ctx.Rooms
    .Include(r => r.Options)
    .Where(r => r.Options.Any(o => o.Name == "Wifi gratuit")).ToList();
Console.WriteLine("----- Exercice 2 -----");
foreach (Room r in result5)
{
    Console.WriteLine(
        $"{r.Number} {r.Price} {string.Join(",", r.Options.Select(o => o.Name))}€"
    );
}
#endregion

#region Exo 3
//int result6 = ctx.Bookings
//    .Where(b => b.BookingDate.Year == 2024 && b.BookingDate.Month == 3)
//    .Count();
int result6 = ctx.Bookings
    .Count(b => b.BookingDate.Year == 2024 && b.BookingDate.Month == 3);
Console.WriteLine("----- Exercice 3 ------");
Console.WriteLine(result6);
#endregion

#region Exo 4
Room mostExpensiveRoom = ctx.Rooms.OrderByDescending(r => r.Price).First();
var result7 = ctx.Customers
    .Where(c => c.Bookings.Any(b => b.RoomId == mostExpensiveRoom.RoomId)).ToList();

foreach(Customer c in result7)
{
    Console.WriteLine($"{c.LastName} {c.FirstName}");
}
#endregion

#region Exo 5
List<Option> result8 = ctx.Options
    .Include(o => o.Rooms) // facultatif
    .Where(o => o.Rooms.Count() >= 2).ToList();
Console.WriteLine("----- Exercice 5 -----");
foreach (Option o in result8)
{
    Console.WriteLine(o.Name);
    foreach (Room r in o.Rooms)
    {
        Console.WriteLine(r.Number);
    }
    Console.WriteLine("---------------------");
}
#endregion

#region Exo 6
var result9 = ctx.Bookings
    .Where(b => b.BookingDate.Year == 2024)
    .GroupBy(b => b.Room)
    .Select(g => new { 
        Number = g.Key.Number, 
        Total = g.Sum(r => r.Price - (r.Price * (r.Discount / 100))) ,
        // Total = g.Sum(r => r.Price * (1 - r.Discount/100))
    })
    .ToList();
List<NumberTotal> result10 = ctx.Rooms
    .Select(r => new NumberTotal
    {
        Number = r.Number,
        Total = r.Bookings
            .Where(b => b.BookingDate.Year == 2024)
            .Sum(b => b.Price - (1 * b.Discount / 100))
    }).ToList();

foreach (var item in result9)
{
    Console.WriteLine(item.Number);
    Console.WriteLine(item.Total);
    Console.WriteLine("-------------------------------");
}


#endregion


class NumberTotal
{
    public string Number { get; set; } = null!;
    public decimal Total { get; set; }
}