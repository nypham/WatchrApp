using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Watchr.Models;

namespace Watchr.App_Start
{
    public class AdminController : Controller
    {
        DBModel db = new DBModel();
        // GET: Admin
        public ActionResult Admin()
        {
            return View(db);
        }
    }
}