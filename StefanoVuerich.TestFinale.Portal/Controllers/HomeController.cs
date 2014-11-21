using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StefanoVuerich.TestFinale.Portal.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
        public ActionResult Login()
        {
            ViewBag.Title = "Login";

            return View();
        }
        public ActionResult ShowData()
        {
            ViewBag.Title = "Show Data";

            return View();
        }
        public ActionResult Register()
        {
            ViewBag.Title = "Register";

            return View();
        }
        public ActionResult ChangePassword()
        {
            ViewBag.Title = "Change Password";

            return View();
        }
    }
}
