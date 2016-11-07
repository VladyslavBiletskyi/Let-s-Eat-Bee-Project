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
    public class AccountController : Controller
    {
        LEBDatabaseModelContainer db = new LEBDatabaseModelContainer();

        

        public ActionResult SignIn()
        {
            if (Session["email"] != null)
                return RedirectToAction("Index", "Home");
            return View(new ReportViewModel());
        }
        [HttpPost]
        public ActionResult SignIn(ReportViewModel report)
        {
            User user = db.UserSet.Where(x => x.Email == report.Email).First();
            if (user == null||user.Password!=report.Pass)
            {
                return View();
            }
            else
            {
                Session["email"] = user.Email;
                Session["UserId"] = user.Id;
                return RedirectToAction("Index", "Home");
            }
        }
        public ActionResult SignUp()
        {
            if (Session["email"] != null)
                return RedirectToAction("Index", "Home");
            return View();
        }
        [HttpPost]
        public ActionResult SignUp(User user)
        {
            db.UserSet.Add(user);
            db.SaveChanges();

            Session["email"] = user.Email;
            return RedirectToAction("Index", "Home");
        }
        public ActionResult SignOut()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}