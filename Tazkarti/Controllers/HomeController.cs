using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Tazkarti.Models;

namespace Tazkarti.Controllers
{
    public class HomeController : Controller
    {
        AppDbContext context = new AppDbContext();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var t = context.Tickets.OrderBy(x => x.Time).ToList();
            return View(t);
        }
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult SaveRegister(User u, string confirmpassword)
        {
            if(u.Password == confirmpassword)
            {
                context.Users.Add(u);
                context.SaveChanges();
            }
            return RedirectToAction("SignIn");
        }
        public IActionResult SignIn()
        {
            return View();
        }
        public IActionResult AdminLogin()
        {
            var l = context.Tickets.ToList();
            return View(l);
        }
        public IActionResult Login(string email, string password)
        {
            if(email == "mohamed@admin.com" && password == "12345")
            {
                return RedirectToAction("adminlogin");
            }
            User? u = context.Users.FirstOrDefault(x => x.Email == email && x.Password == password);
            if (u is not null)
            {
                TempData["login"] = u.Name;
                TempData["id"] = u.Id;
                return RedirectToAction("index");
            }
            return Content("Not Found");
        }
        public IActionResult AddNew()
        {
            return View();
        }
        public IActionResult SaveAddNew(Ticket t)
        {
            context.Tickets.Add(t);
            context.SaveChanges();
            return RedirectToAction("adminlogin");
        }
        public IActionResult Delete(int id)
        {
            var t = context.Tickets.FirstOrDefault(x => x.Id == id);
            context.Tickets.Remove(t);
            context.SaveChanges();
            return RedirectToAction("adminlogin");
        }
        public IActionResult Cancel(int id)
        {
            var t = context.User_Tickets.FirstOrDefault(x => x.Ticket_Id == id && x.User_Id == (int)TempData["id"]);
            TempData.Keep();
            context.User_Tickets.Remove(t);
            context.SaveChanges();
            return RedirectToAction("mytickets");
        }
        public IActionResult Update(int id)
        {
            var t = context.Tickets.FirstOrDefault(x => x.Id == id);
            return View(t);
        }
        public IActionResult SaveUpdate(Ticket t)
        {
            var up = context.Tickets.FirstOrDefault(x => x.Id == t.Id);
            up.First_team = t.First_team;
            up.Second_team = t.Second_team;
            up.Time = t.Time;
            up.NumOfTickets = t.NumOfTickets;
            up.City = t.City;
            up.Price = t.Price;
            up.Stade = t.Stade;
            context.SaveChanges();
            return RedirectToAction("adminLogin");
        }
        public IActionResult LogOut()
        {
            TempData.Remove("login");
            TempData.Remove("id");
            return RedirectToAction("index");
        }
        public IActionResult Book(int id)
        {
            if (TempData.ContainsKey("id"))
            {
                var x = context.User_Tickets.FirstOrDefault(y => y.Ticket_Id == id && y.User_Id == (int)TempData["id"]);
                if(x is null)
                {
                    User_Ticket z = new User_Ticket { Ticket_Id = id, User_Id = (int)TempData["id"] };
                    context.User_Tickets.Add(z);
                    var a = context.Tickets.FirstOrDefault(x => x.Id == id);
                    a.NumOfTickets--;
                    context.SaveChanges();
                }
                TempData.Keep();
                return RedirectToAction("mytickets");
            }
            
            return RedirectToAction("signin");
        }
        public IActionResult MyTickets()
        {
            int id = (int)TempData["id"];
            TempData.Keep();
            var user_tickets = context.User_Tickets.Where(x => x.User_Id == id).ToList();
            var all_tickets = context.Tickets.ToList();
            var j = user_tickets.Join(
                all_tickets,
                u => u.Ticket_Id,
                t => t.Id,
                (us, ts) => ts
            ).OrderBy(x => x.Time).ToList();
            return View(j);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
