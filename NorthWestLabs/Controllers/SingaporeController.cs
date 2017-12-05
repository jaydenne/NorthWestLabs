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
        private NorthwestLabsEntitiesDB db = new NorthwestLabsEntitiesDB();

        // GET: Singapore
        public ActionResult Index()
        {
            return View();
        }


    }
}