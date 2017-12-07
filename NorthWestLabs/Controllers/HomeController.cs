﻿using NorthWestLabs.DAL;
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

        public int GetEmployeeID()
        {
            int EmployeeID = 0;
            IEnumerable<Employee> EmployeeList = db.Employees.ToList();
            foreach (Employee myEmployee in EmployeeList)
            {
                if (User.Identity.Name == myEmployee.UserName)
                {
                    EmployeeID = myEmployee.EmployeeID;
                }
            }

            return EmployeeID;
        }













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

            int EmployeeID = GetEmployeeID();
            AllEmployeeInfo AllInfo = new AllEmployeeInfo();
            AllInfo.Employee = db.Employees.Find(EmployeeID);
            IEnumerable<EmployeeBankInfo> AllBankInfo = db.EmployeeBankInfoes.ToList();
            foreach(EmployeeBankInfo item in AllBankInfo)
            {
                if(item.EmployeeID==EmployeeID)
                {
                    AllInfo.BankInfo = item;
                }
            }
            IEnumerable<EmployeeWageInfo> AllWageInfo = db.EmployeeWageInfoes.ToList();
            foreach (EmployeeWageInfo item in AllWageInfo)
            {
                if (item.EmployeeID == EmployeeID)
                {
                    AllInfo.WageInfo = item;
                }
            }

            return View(AllInfo);
        }

        public ActionResult EditBankInfo()
        {
            return View();
        }
        [HttpPost]
        public ActionResult EditBankInfo(int placeholder)
        {
            return View();
        }

    }
}