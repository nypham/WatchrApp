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
            return View();
        }
        [HttpPost]
        public ActionResult SendFeedback(string comments,string rating)
        {
            using (WatchrDataContext wdc = new WatchrDataContext())
            {
                
                Feedback fb = new Feedback {fb_comments= comments,fb_rating=Convert.ToInt32(rating),user_id=1};
                wdc.Feedbacks.InsertOnSubmit(fb);
                wdc.SubmitChanges();
            }
            return PartialView();

        }
    }
}