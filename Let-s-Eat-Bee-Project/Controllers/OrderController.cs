using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using static System.Net.WebRequestMethods;
using System.Data.Entity;
using Let_s_Eat_Bee_Project.Models;
using System.ComponentModel.DataAnnotations;

namespace Let_s_Eat_Bee_Project.Controllers
{
    public class OrderController : Controller
    {
        public class NewOrder
        {
            public string Email { set; get; }
            [Required(AllowEmptyStrings = false, ErrorMessage = "Input First Name")]
            public string FirstName { set; get; }
            [Required(AllowEmptyStrings = false, ErrorMessage = "Input Last Name")]
            public string LastName { set; get; }
            [Required(AllowEmptyStrings = false, ErrorMessage = "Input Address")]
            public string Address { set; get; }
            [Required(AllowEmptyStrings = false, ErrorMessage = "Input Date in format dd:MM:yyyy")]
            public string Date { set; get; }
            [Required(AllowEmptyStrings = false, ErrorMessage = "Input Time in format hh:mm")]
            public string Time { set; get; }
            public string TextOfOrder { set; get; }
        }


        LEBDatabaseModelContainer db = new LEBDatabaseModelContainer();
        // GET: Order
        public ActionResult Index()
        {         
            return View(db.Set<Order>());
        }
        public ActionResult Create()
        {
            NewOrder order = new NewOrder();
            if (Session["email"] != null)
            {

                var usersSet = db.Set<User>();
                User user = usersSet.Find(Session["email"]);

                order.Email = user.Email;
                order.LastName = user.LastName;
                order.FirstName = user.FirstName;

            }
            return View(order);
        }
        [HttpPost]
        public ActionResult Create(NewOrder _order)
        {
            Order order = new Order();
            User user = null;
            if (_order.Email != null)
            {
                user = db.Set<User>().Find(_order.Email);
            }
            order.Creator = user;
            
            order.Address = _order.Address;
            order.CompleteDateTime = DateTime.Parse(_order.Date + " " + _order.Time);
            

            db.Set<Order>().Add(order);
            db.SaveChanges();

            Joining join = new Joining();
            join.Order = order;
            join.OrderId = order.Id;
            join.TextOfOrder = _order.TextOfOrder;
            join.UserFirstName = _order.FirstName;
            join.UserLastName = _order.LastName;
            join.User = user;

            db.Set<Joining>().Add(join);
            db.SaveChanges();

            if (user != null)
            {
                user.Orders.Add(order);
                user.Joinings.Add(join);
                db.SaveChanges();
            }

            return RedirectToAction("Index", "Home");
        }
    }
}