using NorthWestLabs.DAL;
using NorthWestLabs.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace NorthWestLabs.Controllers
{
    public class HomeController : Controller
    {
        private NorthwestLabsContext db = new NorthwestLabsContext();
        public Boolean IsClient()
        {
            Boolean client = false;
            IEnumerable<Client> ClientList = db.Clients.ToList();
            foreach(Client item in ClientList)
            {
                if(User.Identity.Name==item.Username)
                {
                    client = true;
                }
            }
            
            return client;
          }
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
            if(IsClient())
            {
                RedirectToAction("Index");
            }
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
        [Authorize]

        public ActionResult EditBankInfo()
        {
            if (IsClient())
            {
                RedirectToAction("Index");
            }
            IEnumerable<EmployeeBankInfo> AllEmployeeBankInfo = db.EmployeeBankInfoes.ToList();
            EmployeeBankInfo employeeBankInfo = new EmployeeBankInfo();
            int EmployeeID = GetEmployeeID();
            foreach(EmployeeBankInfo item in AllEmployeeBankInfo)
            {
                if(item.EmployeeID==EmployeeID)
                {
                    employeeBankInfo = item;
                }
            }

            if (employeeBankInfo == null)
            {
                return HttpNotFound();
            }
            return View(employeeBankInfo);
        }

        // POST: EmployeeBankInfoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult EditBankInfo([Bind(Include = "EmployeeBankInfoID,EmployeeID,BankAccount,RoutingNumber,AccountType,ModifiedBy,ModifiedDate")] EmployeeBankInfo employeeBankInfo)
        {
            if (IsClient())
            {
                RedirectToAction("Index");
            }
            if (ModelState.IsValid)
            {
                db.Entry(employeeBankInfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employeeBankInfo);
        }
        [Authorize]
        public ActionResult EditPersonalInfo()
        {
            if (IsClient())
            {
                RedirectToAction("Index");
            }
            Employee employee = db.Employees.Find(GetEmployeeID());
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult EditPersonalInfo([Bind(Include = "EmployeeID,Name,Address,Phone,Position,Location,StartDate,UserName,Password,ModifiedBy,ModifiedDate,CreatedBy,CreatedDate")] Employee employee)
        {
            if (IsClient())
            {
                RedirectToAction("Index");
            }
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
        }
        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        [Authorize]
        public ActionResult ChangePassword(FormCollection form, string returnUrl, bool rememberMe = false)
            {
            if (IsClient())
            {
                RedirectToAction("Index");
            }
            String password = form["OldPassword"].ToString();
            String Newpassword = form["NewPassword"].ToString();
            String Confirmpassword = form["ConfirmPassword"].ToString();
            
            Employee myEmployee = db.Employees.Find(GetEmployeeID());
            if(myEmployee.Password == password)
            {
                if (Newpassword == Confirmpassword)
                {
                    myEmployee.Password = Newpassword;
                    db.SaveChanges();
                    RedirectToAction("EmployeeAccount", "Home");
                }
            }
                                                    
                return View();
            
        }

        public ActionResult RequestQuote()
        {


            return View();
        }

    }
}