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
        public string password;
        // GET: RegisterandLogin
        public ActionResult Register()
        {
            
            return View();
        }
        public ActionResult Login()
        {

            return View();
        }
        public ActionResult updateUsername()
        {
            return View();
        }
        public ActionResult updateUsernameDB(string userName)
        {
            using (WatchrDataContext db = new WatchrDataContext())
            {
                var temp = db.Users.Where(x => x.user_id == (int)Session["userID"]).SingleOrDefault();
                temp.user_name = userName;
                db.SubmitChanges();
                Session["userName"] = userName;
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult LoginButton(string userName,string pass)
        {
            using (WatchrDataContext wdc = new WatchrDataContext())
            {
                userID = wdc.Users.Where(x=>x.user_name ==userName && x.user_password==pass).Select(x=>x.user_id).SingleOrDefault();
                password = wdc.Users.Where(x => x.user_name == userName && x.user_password == pass).Select(x => x.user_password).SingleOrDefault();
                if (password == pass) { 
                Session["userName"] = userName;
                Session["userID"] = userID;
            }
                else { return RedirectToAction("Login","RegisterandLogin"); }
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult RegisterButton(string userName, string email, string pass, int birthYear)
        {
            User user = new User { user_name = userName, user_email = email, user_birthyear = birthYear, user_password = pass };
            using (WatchrDataContext wdc = new WatchrDataContext())
            {
                wdc.Users.InsertOnSubmit(user);
                wdc.SubmitChanges();
            }
            return RedirectToAction("Login");
        }
        public ActionResult Logout()
        {
            Session["userID"] = null;
            Session["userName"] = null;
            return RedirectToAction("Index","Home");
        }
    }
}