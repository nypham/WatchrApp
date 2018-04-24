using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Watchr.Controllers
{
    public class FeedbackController : Controller
    {
        // GET: Feedback
        public ActionResult Feedback()
        {
            if (Session["userID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "RegisterandLogin");
            }
        }
        [HttpPost]
        public ActionResult SendFeedback(string comments,string rating)
        {
            
                using (WatchrDataContext wdc = new WatchrDataContext())
                {

                    Feedback fb = new Feedback { fb_comments = comments, fb_rating = Convert.ToInt32(rating), user_id = (int)Session["userID"] };
                    wdc.Feedbacks.InsertOnSubmit(fb);
                    wdc.SubmitChanges();
                }
                return PartialView();
            
        }
    }
}