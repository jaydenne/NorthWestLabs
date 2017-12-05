using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace NorthWestLabs.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Login()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection form, string returnUrl, bool rememberMe = false)
        {
            String user = form["Username"].ToString();
            String password = form["Password"].ToString();

            if(string.Equals(user, "Missouri") && (string.Equals(password, "ShowMe")))
            {
                FormsAuthentication.SetAuthCookie(user, rememberMe);

                if (!string.IsNullOrEmpty(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                    
                return RedirectToAction("UpdateData");

            }
            else
            {
                return View();
            }

        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}