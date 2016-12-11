using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Let_s_Eat_Bee_Project.Models;
using System.Collections.Generic;

namespace Let_s_Eat_Bee_Project.Controllers
{
    public class ModeratorController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Moderator
        public ActionResult Index()
        {
            return View(new Let_s_Eat_Bee_Project.Models.DBDump {
                Orders =db.Orders.ToList(),
                AuthUsers =db.AllUsers.OfType<AuthorizedUser>().ToList(),
                UnUsers =db.AllUsers.OfType<UnauthorizedUser>().ToList()});
        }

        [HttpPost]
        public ActionResult UserDetail(int id)
        {
            AuthorizedUser user = db.AllUsers.OfType<AuthorizedUser>().FirstOrDefault(x => x.Id == id);
            if (user == null)
            {
                return HttpNotFound();
            }

            return PartialView(user);
        }
        [HttpPost]
        public void UserEdit(Let_s_Eat_Bee_Project.Models.AuthorizedUser user)
        {
            if (user == null)
            {
                Response.Write("404 NOT FOUND");
                //return HttpNotFound();
            }
            else
            {
                AuthorizedUser old = db.AllUsers.OfType<AuthorizedUser>().Where(x => x.Id == user.Id).FirstOrDefault();
                old.FirstName = user.FirstName;
                old.LastName = user.LastName;
                old.Organization = user.Organization;
                db.SaveChanges();
                Response.Write("<script>history.go(-1);</script>");
                //return RedirectToAction("Index");
            }

        }
        [HttpPost]
        public ActionResult OrderDetail(int id)
        {
            Order order = db.Orders.FirstOrDefault(x => x.Id == id);
            if (order == null)
            {
                return HttpNotFound();
            }

            return PartialView(order);
        }
        [HttpPost]
        public void OrderEdit(Let_s_Eat_Bee_Project.Models.Order order)
        {
            if (order == null)
            {
                //return HttpNotFound();
                Response.Write("404 NOT FOUND");
            }
            else
            {
                Order old = db.Orders.Where(x => x.Id == order.Id).FirstOrDefault();
                old.CompletionDateTime = order.CompletionDateTime;
                old.Address = order.Address;
                db.SaveChanges();
                Response.Write("<script>history.go(-1);</script>");
                //return RedirectToAction("Index");
            }

        }
    }
}