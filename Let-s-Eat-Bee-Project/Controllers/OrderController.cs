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
        public ActionResult Index(int page = 1)
        {
            IEnumerable<Order> orders = from u in db.Orders
                                        where u.CompletionDateTime >= DateTime.Now
                                        && u.IsPrivate == false
                                        orderby u.CompletionDateTime
                                        select u;

            ViewBag.Page = page;
            return View(orders.ToList<Order>());
        }
        public ActionResult Create()
        {
            NewOrder order = new NewOrder();

            if (User.Identity.IsAuthenticated)
            {
                string id = User.Identity.GetUserId();
                IEnumerable<AuthorizedUser> u = db.AllUsers.OfType<AuthorizedUser>();
                AuthorizedUser user = u.FirstOrDefault<AuthorizedUser>(x => x.AppUserId == id);

                order.LastName = user.LastName;
                order.FirstName = user.FirstName;
            }
            return View(order);
        }
        [HttpPost]
        public ActionResult Create(NewOrder model)
        {
            Order order = new Order();
            AbstractUser user = null;
            if (User.Identity.IsAuthenticated)
            {
                string id = User.Identity.GetUserId();
                IEnumerable<AuthorizedUser> u = db.AllUsers.OfType<AuthorizedUser>();
                user = u.FirstOrDefault(x => x.AppUserId == id);
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
            order.CreatorId = user.Id;
            order.Address = model.Address;
            order.CompletionDateTime = DateTime.Parse(model.Date + " " + model.Time);
            order.OrderPlace = model.Place;
            order.IsPrivate = model.IsPrivate;

            db.Orders.Add(order);
            db.SaveChanges();

            Joining join = new Joining();
            join.Order = order;
            join.OrderId = order.Id;
            join.Text = model.TextOfOrder;
            join.User = user;
            join.UserId = user.Id;
            db.Joinings.Add(join);
            db.SaveChanges();
            user.Orders.Add(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                Order order = db.Orders.FirstOrDefault(x => x.Id == id);
                if (order != null)
                {
                    AuthorizedUser creator = (AuthorizedUser)order.Creator;
                    if (creator.AppUserId == null && creator.AppUserId != User.Identity.GetUserId())
                    {
                        return View("Index");
                    }
                    EditOrder currentOrder = new EditOrder();
                    currentOrder.Address = order.Address;
                    currentOrder.Date = order.CompletionDateTime.Date.ToShortDateString();
                    currentOrder.Time = order.CompletionDateTime.ToShortTimeString();
                    currentOrder.Text = order.Joinings.First().Text;
                    return View(currentOrder);
                }
            }
            return RedirectToAction("SignIn", "Account");

        }
        [HttpPost]
        public ActionResult Edit(EditOrder model, int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                Order order = db.Orders.FirstOrDefault(x => x.Id == id);
                if (order != null)
                {
                    order.Address = model.Address;
                    order.CompletionDateTime = DateTime.Parse(model.Date + " " + model.Time);
                    order.Joinings.First().Text = model.Text;
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index", "Account");
        }
        public ActionResult Delete(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                Order order = db.Orders.FirstOrDefault(x => x.Id == id);
                if (order != null)
                {
                    db.Orders.Remove(order);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index", "Account");
        }
        public ActionResult Detail(int id)
        {
            Order order = db.Orders.Find(id);
            if (order != null)
            {
                string userId = User.Identity.GetUserId();
                if (userId != null)
                {
                    ViewBag.UserId = db.AllUsers.OfType<AuthorizedUser>().FirstOrDefault(x => x.AppUserId == userId).Id;
                }
                if (!order.IsPrivate)
                {
                    return View(order);
                }
                else
                {
                    if ((order.Creator is AuthorizedUser) && ((AuthorizedUser)order.Creator).AppUserId + order.Id == Request.QueryString["oid"])
                    {
                        return View(order);
                    }
                }
            }
            return RedirectToAction("Index", "Order", null);
        }
        [HttpPost]
        public ActionResult Join (NewJoining newJoining)
        {
            if (newJoining.OrderID != 0)
            {
                string id = User.Identity.GetUserId();
                AbstractUser user = db.AllUsers.OfType<AuthorizedUser>().Where(x => x.AppUser.Id == id).FirstOrDefault();
                if (user == null)
                {
                    user = new UnauthorizedUser();
                    user.FirstName = newJoining.UserFirstName;
                    user.LastName = newJoining.UserLastName;
                    db.AllUsers.Add(user);
                    db.SaveChanges();
                }
                if (newJoining.UserFirstName == null || newJoining.UserLastName == null)
                {
                    newJoining.UserFirstName = user.FirstName;
                    newJoining.UserLastName = user.LastName;
                }
                Order order = db.Orders.Where(x => x.Id == newJoining.OrderID).FirstOrDefault();
                Joining joining = new Joining();
                joining.Order = order;
                joining.OrderId = order.Id;
                joining.User = user;
                joining.UserId = user.Id;
                joining.Text = newJoining.Text;
                db.Joinings.Add(joining);
                db.SaveChanges();
                order.Joinings.Add(joining);
                db.SaveChanges();
                user.Joinings.Add(joining);
                db.SaveChanges();
            }
            return RedirectToAction("Index","Order");   
        }
        [HttpPost]
        public ActionResult JoiningDelete(int Id)
        {
            Joining joining = db.Joinings.Find(Id);
            if (joining != null)
            {
                db.Joinings.Remove(joining);
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Account");

        }
        [HttpPost]
        public ActionResult JoiningEdit(Joining newJoining)
        {
            Joining joining = db.Joinings.Find(newJoining.Id);
            if (joining != null)
            {
                joining.Text = newJoining.Text;
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Account");
        }
        public ActionResult MessageDelete(int Id)
        {
            Message message = db.Messages.Find(Id);
            if (message != null)
            {
                db.Messages.Remove(message);
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Account");
        }
    }
}