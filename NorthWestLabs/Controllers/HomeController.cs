using NorthWestLabs.DAL;
using NorthWestLabs.Models;
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
        private NorthwestLabsContext db = new NorthwestLabsContext();
        
        public ActionResult Login()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection form, string returnUrl, bool rememberMe = false)
        {
            String user = form["Username"].ToString();
            String password = form["Password"].ToString();
            IEnumerable<Client> ClientList = db.Clients.ToList();
            IEnumerable<Employee> EmployeeList = db.Employees.ToList();
            Boolean SingaporeEmployee = false;
            Boolean SeattleEmployee = false;
            String SavedPassword = null;


            foreach (Client ClientItem in ClientList)
            {
                if (user == ClientItem.Username)
                {
                    SavedPassword = ClientItem.PasswordHash;
                }
            }

            if (string.Equals(password, SavedPassword))
            {
                FormsAuthentication.SetAuthCookie(user, rememberMe);
                if (!string.IsNullOrEmpty(returnUrl))
                {
                    return Redirect(returnUrl);
                }

                return RedirectToAction("Index", "Client");

            }
            foreach (Employee Employee in EmployeeList)
            {
                if (user == Employee.UserName)
                {
                    SavedPassword = Employee.Password;
                    if (Employee.Location == "Singapore")
                    {
                        SingaporeEmployee = true;
                    }
                    else
                    {
                        SeattleEmployee = true;
                    }

                }
                if (string.Equals(password, SavedPassword))
                {
                    FormsAuthentication.SetAuthCookie(user, rememberMe);

                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else if (SingaporeEmployee)
                    {
                        return RedirectToAction("Index", "Singapore");
                    }
                    else
                    {
                        return RedirectToAction("SeattleIndex", "Seattle");

                    }
                }

            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            

            return View();
        }

        public ActionResult Contact()
        {
            

            return View();
        }

        public ActionResult Catalog()
        {

            return View(db.ProtocolNotebooks.ToList());
        }

        [Authorize]
        public ActionResult  EmployeeAccount()
        {

            return View();
        }

    }
}