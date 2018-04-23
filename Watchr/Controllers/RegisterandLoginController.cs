using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Watchr.Controllers
{
    public class RegisterandLoginController : Controller
    {
        public string userName;
        public int userID;
        // GET: RegisterandLogin
        public ActionResult Register()
        {
            
            return View();
        }
        public ActionResult Login()
        {

            return View();
        }
        [HttpPost]
        public ActionResult LoginButton(string userName,string pass)
        {
            using (WatchrDataContext wdc = new WatchrDataContext())
            {
                userID = wdc.Users.Where(x=>x.user_name ==userName && x.user_password==pass).Select(x=>x.user_id).SingleOrDefault();
                this.userName = userName;
            }
            return RedirectToAction("Home");
        }
        [HttpPost]
        public ActionResult RegisterButton(string userName, string email, string pass, string birthYear)
        {
            User user = new User { user_name = userName, user_email = email, user_birthyear = birthYear, user_password = pass };
            using (WatchrDataContext wdc = new WatchrDataContext())
            {
                wdc.Users.InsertOnSubmit(user);
                wdc.SubmitChanges();
            }
            return RedirectToAction("Login");
        }
    }
}