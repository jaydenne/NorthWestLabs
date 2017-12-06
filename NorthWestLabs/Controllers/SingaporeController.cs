using NorthWestLabs.DAL;
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

        // GET: Singapore
        public ActionResult Index()
        {
            return View();
        }


    }
}