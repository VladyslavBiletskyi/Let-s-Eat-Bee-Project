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
        ApplicationDbContext db = new ApplicationDbContext();

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public ActionResult Index(int page = 1)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                string myId = User.Identity.GetUserId();
                AuthorizedUser myUser = (from user in db.AllUsers.OfType<AuthorizedUser>()
                          where
                          user.AppUserId == myId
                          select user).FirstOrDefault();                
                ViewBag.Page = page;
                return View((from order in myUser.Orders
                             where order.CreationDateTime >= DateTime.Now
                             orderby order.CreationDateTime
                             select order).ToList<Order>());
            }
        }
        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }


        public ActionResult SignIn(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Account");
            }
            return View(new ReportViewModel());
        }
        [HttpPost]
        public async Task<ActionResult> SignIn(ReportViewModel report, string returnUrl)
        {
            if (!User.Identity.IsAuthenticated)
            {
                if (!ModelState.IsValid)
                {
                    return View(report);
                }

                // Сбои при входе не приводят к блокированию учетной записи
                // Чтобы ошибки при вводе пароля инициировали блокирование учетной записи, замените на shouldLockout: true
                var result = await SignInManager.PasswordSignInAsync(report.Email, report.Pass, true, shouldLockout: false);
                switch (result)
                {
                    case SignInStatus.Success:
                        return RedirectToLocal(returnUrl);
                    default:
                        ModelState.AddModelError("", "Неудачная попытка входа.");
                        return View(report);
                }
            }
            return RedirectToAction("Index", "Account");
        }
        public ActionResult SignUp()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Account");
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> SignUp(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    AuthorizedUser aUser = new AuthorizedUser();
                    aUser.AppUser = db.Users.Find(user.Id);
                    aUser.AppUserId = user.Id;
                    aUser.LastName = model.LastName;
                    aUser.Organization = model.Organization;
                    aUser.FirstName = model.FirstName;
                    db.AllUsers.Add(aUser);

                    db.SaveChanges();

                    return RedirectToAction("Index", "Account");
                }
                ModelState.AddModelError("", "Неудачная попытка входа!");
            }
            return RedirectToAction("Index", "Home");
        }
        public ActionResult SignOut()
        {
            if (User.Identity.IsAuthenticated)
                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public ActionResult UserProfile()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            string id = User.Identity.GetUserId();
            return View(db.AllUsers.OfType<AuthorizedUser>().Where(x=>x.AppUserId==id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult EditProfile(AuthorizedUser newUser)
        {
            string id = User.Identity.GetUserId();
            AuthorizedUser oldUser = db.AllUsers.OfType<AuthorizedUser>().Where(x => x.AppUserId == id).FirstOrDefault();
            oldUser.FirstName = newUser.FirstName??oldUser.FirstName;
            oldUser.LastName = newUser.LastName??oldUser.LastName;
            oldUser.Organization = newUser.Organization??oldUser.Organization;
            db.SaveChanges();
            return RedirectToAction("UserProfile", "Account");
        }
    }
}