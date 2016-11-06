using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using static System.Net.WebRequestMethods;
using System.Data.Entity;
using Let_s_Eat_Bee_Project.Models;

namespace Let_s_Eat_Bee_Project.Controllers
{
    public class OrderController : Controller
    {
        public class NewOrder
        {
            public int UserID { set; get; }
            public string FirstName { set; get; }
            public string LastName { set; get; }
            public string Address { set; get; }
            public string Date { set; get; }
            public string Time { set; get; }
        }

        LEBDatabaseModelContainer db = new LEBDatabaseModelContainer();
        // GET: Order
        public ActionResult Index()
        {         
            IEnumerable<Order> temp1 = db.OrderSet;
            return View(temp1);
        }
        public ActionResult Create()
        {
            NewOrder order = new NewOrder();
            if (Session["UserId"] != null)
            {

                var usersSet = db.Set<User>();
                User user = usersSet.Find((int)Session["UserID"]);

                order.UserID = user.Id;
                order.LastName = user.LastName;
                order.FirstName = user.FirstName;

            }
            return View(order);
        }
        [HttpPost]
        public ActionResult Create(NewOrder _order)
        {
            Order order = new Order();
            order.UserId = _order.UserID;
            order.Address = _order.Address;
            order.CompleteDateTime = DateTime.Parse(_order.Date + " " + _order.Time);
            User creator = db.Set<User>().Find(_order.UserID);

            db.Set<Order>().Add(order);
            db.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}