using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using static System.Net.WebRequestMethods;
using System.Data.Entity;
using Let_s_Eat_Bee_Project.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;

namespace Let_s_Eat_Bee_Project.Controllers
{
    public class OrderController : Controller
    {
        
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Order
        public ActionResult Index()
        {         
            return View(db.Set<Order>());
        }
        public ActionResult Create()
        {
            NewOrder order = new NewOrder();
            
            if (User.Identity.IsAuthenticated) 
            {
                string id = User.Identity.GetUserId();
                ApplicationUser user = db.Users.FirstOrDefault(x => x.Id == id);
                AuthorizedUser u = db.AuthUser.FirstOrDefault(x => x.AppUserId == id);
                order.UserId = u.Id;
                order.LastName = u.LastName;
                order.FirstName = u.FirstName;
            }
            return View(order);
        }
        [HttpPost]
        public ActionResult Create(NewOrder model)
        {
            Order order = new Order();
            Models.AbstractUser user = null;
            if (User.Identity.IsAuthenticated)
            {
                string id = User.Identity.GetUserId();
                user = db.AuthUser.FirstOrDefault( x=> x.AppUserId == id);
            }
            else
            {
                user = new UnauthorizedUser();
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                db.AllUsers.Add(user);
                db.SaveChanges();
            }
            order.Creator = user;
            order.CreatorId
                = user.Id;     
            order.Address = model.Address;
            order.CreationDateTime = DateTime.Parse(model.Date + " " + model.Time);
           
            db.Orders.Add(order);
            db.SaveChanges();

            Joining join = new Joining();
            join.Order = order;
            join.OrderId = order.Id;
            join.Text = model.TextOfOrder;
            join.User = user;

            db.Joinings.Add(join);
            db.SaveChanges();

            return View("Index");
        }
    }
}