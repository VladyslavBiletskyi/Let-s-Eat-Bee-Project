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
        
        LEBDatabaseModelContainer db = new LEBDatabaseModelContainer();
        // GET: Order
        public ActionResult Index()
        {         
            return View(db.Set<Order>());
        }
        public ActionResult Create()
        {
            NewOrder order = new NewOrder();
            
            if (Session["UserId"] != null ) //fixed by Tatiana, винить ее
            {
                int id = (int)Session["UserId"];
                User user = db.UserSet.Find(id);
                order.UserId = user.Id;
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
            if (_order.UserId != 0)
            {
                user = db.Set<User>().Find(_order.UserId);
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