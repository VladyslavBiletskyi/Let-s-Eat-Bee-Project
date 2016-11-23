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
            return View(new Let_s_Eat_Bee_Project.Models.DBDump { Orders=db.Orders.ToList(),Users=db.AllUsers.ToList()});
        }
    }
}