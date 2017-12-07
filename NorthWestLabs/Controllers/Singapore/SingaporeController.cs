using NorthWestLabs.DAL;
using NorthWestLabs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NorthWestLabs.Controllers
{
    public class SingaporeController : Controller
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

        // GET: Singapore
        public ActionResult Index()
        {
            Employee myEmployee = db.Employees.Find(GetEmployeeID());
            return View(myEmployee);
        }
        public ActionResult ReceiveCompound()
        {
            IEnumerable<Compound> AllCompounds = db.Compounds.ToList();
            List<SelectListItem> CompoundList = new List<SelectListItem>();
            foreach(Compound item in AllCompounds)
            {
                if(item.DateArrived==null)
                {
                    CompoundList.Add(new SelectListItem
                    {
                        Text = item.LTNumber + "-" + item.SequenceCode,
                        Value = item.CompoundID.ToString()
                    });  
                    }
                }
            

            return View(CompoundList);
        }
        [HttpPost]
        public ActionResult ReceiveCompound(FormCollection form)
        {
            return RedirectToAction("Index");
        }

        public ActionResult UnassignedTests()
        {
            return View();
        }
        public ActionResult AssignedTests()
        {
            return View();
        }
        public ActionResult PendingTests()
        {
            return View();
        }
        public ActionResult UploadResults()
        {
            return View();
        }
    }
}