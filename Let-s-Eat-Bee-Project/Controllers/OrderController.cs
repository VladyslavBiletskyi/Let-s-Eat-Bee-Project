using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static System.Net.WebRequestMethods;
using System.Data.Entity;
using Let_s_Eat_Bee_Project.Models;

namespace Let_s_Eat_Bee_Project.Controllers
{
    public class OrderController : Controller
    {
        LEBDatabaseModelContainer db = new LEBDatabaseModelContainer();
        // GET: Order
        public ActionResult Index()
        {         
            IEnumerable<Order> temp1 = db.OrderSet;
            return View(temp1);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Order order)
        {

            db.OrderSet.Add(order);
            db.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}