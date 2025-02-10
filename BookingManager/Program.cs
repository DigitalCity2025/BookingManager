using BookingManager.DAL;
using BookingManager.DAL.Entities;
using Microsoft.EntityFrameworkCore;

using HotelContext ctx = new HotelContext();

// récupérer des données
// SELECT * FROM Room
List<Room> result = ctx.Rooms.ToList();

// SELECT * FROM Room WHERE Surface > 100
result = ctx.Rooms.Where(r => r.Surface > 100).ToList();

// SELECT * FROM Room WHERE Id = 42
Room? room = ctx.Rooms.Find(42);

// SELECT * FROM example WHERE ID1 = 45 AND ID2 = 73
// var ex = ctx.Examples.Find(45, 73) 

// SELECT * FROM Customer WHERE email LIKE 'lykhun@gmail.com'
Customer? user = ctx.Customers.SingleOrDefault(c => c.Email == "lykhun@gmail.com");
Customer? user2 = ctx.Customers.SingleOrDefault(c => EF.Functions.Like(c.Email, "lykhun@gmail.com"));

// SELECT TOP 1 * FROM Customer WHERE lastName LIKE 'a%'
Customer? user3 = ctx.Customers.FirstOrDefault(c => c.LastName.StartsWith("a"));
Customer? user4 = ctx.Customers.FirstOrDefault(c => EF.Functions.Like(c.LastName, "a%"));

// PASSER 10 LIGNES et récupérer les 10 suivantes
// SELECT * FROM Customer
// ORDER BY LastName
// OFFSET 10 ROWS FECTH 10 ROWS ONLY
List<Customer> results2 = ctx.Customers
    .OrderBy(c => c.LastName)
    .Skip(10).Take(10).ToList();

// SELECT COUNT(*) FROM Customer
int count = ctx.Customers.Count();

// SELECT AVG(price) FROM Room
// decimal moyenne = ctx.Rooms.Average(r => r.Price);

// SELECT c.*, b.*, r.*
// FROM Customer c
// LEFT Booking b ON b.CutomerId = c.CustomerId
// LEFT JOIN r ON b.RoomId = r.RoomId
List<Customer> result3 = ctx.Customers
    .Include(c => c.Bookings).ThenInclude(b => b.Room).ToList();

// DML Insert, Update, Delete

// INSERT INTO Customer
// (LastName, FirstName, Email, Password, PhoneNumber, UserName)
// VALUES
// ('LY', 'Khun', 'lykhun@gmail.com', [], null, 'K')
//Customer customer = new Customer { 
//    LastName = "Ly",
//    FirstName = "Khun",
//    Email = "lykhun@gmail.com",
//    Password = new byte[0],
//    PhoneNumber = null,
//    Username = "K",
//};
//ctx.Customers.Add(customer);
//ctx.SaveChanges();


//Booking b = new Booking
//{
//    BookingDate = DateTime.Now,
//    StartDate = DateTime.Now,
//    EndDate = DateTime.Now.AddDays(42),
//    Status = BookingManager.DAL.Enums.BookingStatus.InProgress,
//    // dans le cas ou on souhaite créer le customer en meme tant que la réservation
//    // Customer = new Customer { /*LastName = "Ly", ...*/ }
//    // dans le cas ou on souhaite utiliser un customer existant
//    CustomerId = 1,
//    RoomId = 42
//};

//ctx.Bookings.Add(b);
//ctx.SaveChanges();

//List<Option> options = [
//    new Option
//    {
//        Name = "Vue sur Mer"
//    },
//    new Option{  Name = "Suite" },
//    new Option{  Name = "Air conditionné" },
//    new Option{  Name = "Mini Bar" },
//    new Option{  Name = "Jaccuzzi" },
//    new Option{  Name = "Casino" },
//];
//// insersions multiple
//ctx.Options.AddRange(options);


//Room r = new Room
//{
//    Floor = 42,
//    ImageUrl = "",
//    MaxCapacity = 2,
//    Number = "42A",
//    Price = 42,
//    Surface = 42,
//    // ajoute dans la table entre option et room
//    Options = [options[0], options[2], options[4]]
//};
//ctx.Rooms.Add(r);
//ctx.SaveChanges();

// modification
// UPDATE Customer SET LastName = 'Lee' WHERE CustomerId = 1
//Customer? c = ctx.Customers.Find(1);
//c.LastName = "Lee";
//ctx.SaveChanges();

// suppression
// DELETE FROM Customer WHERE Id = 1
//Customer? c = ctx.Customers.Find(1);
//if(c != null) ctx.Remove(c);
//ctx.SaveChanges();

// SELECT floor AS FLOOR, AVG(price) AS Moyenne FROM Room GROUP BY floor
var moyennes = ctx.Rooms.GroupBy(r => r.Floor)
    .ToList()
    .Select(g => new
    {
        Floor = g.Key,
        Moyenne = g.Average(r => r.Price)
    }).ToList();

foreach (var m in moyennes)
{
    Console.WriteLine(m.Floor);
    Console.WriteLine(m.Moyenne);
}