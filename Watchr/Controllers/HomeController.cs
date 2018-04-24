using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Watchr.Models;

namespace Watchr.Controllers
{
    public class HomeController : Controller
    {
        HomeViewModel hvm = new HomeViewModel { lm = new List<Movie>(), dm = new List<Movie>() };
        public ActionResult Index()
        {
            if (Session["userID"] != null)
            {
                List<Movie> lm = new List<Movie>();
                List<Movie> dm = new List<Movie>();
                using (WatchrDataContext wdc = new WatchrDataContext())
                {
                    var listLikedID = wdc.LikedMovies.Where(x => x.user_id == (int)Session["userID"]).Select(x => x.movie_id).ToList();
                    foreach (var x in listLikedID)
                    {
                        string movieTitle = wdc.Movies.Where(z => z.ID == x).Select(z => z.title).SingleOrDefault();
                        lm.Add(new Movie { ID = x,title=movieTitle });
                    }

                    var listDislikedID = wdc.DislikedMovies.Where(x => x.user_id == (int)Session["userID"]).Select(x => x.movie_id).ToList();
                    foreach (var x in listDislikedID)
                    {
                        string movieTitle = wdc.Movies.Where(z => z.ID == x).Select(z => z.title).SingleOrDefault();
                        dm.Add(new Movie { ID = x, title = movieTitle });
                    }

                    hvm.lm.AddRange(lm);
                    hvm.dm.AddRange(dm);
                }
            }
            return View(hvm);
        }
        public ActionResult LikedList()
        {
            
            using (WatchrDataContext wdc = new WatchrDataContext()) { 
            List<Movie> lm = new List<Movie>();
            var listLikedID = wdc.LikedMovies.Where(x => x.user_id == (int)Session["userID"]).Select(x => x.movie_id).ToList();
            foreach (var x in listLikedID)
            {
                string movieTitle = wdc.Movies.Where(z => z.ID == x).Select(z => z.title).SingleOrDefault();
                lm.Add(new Movie { ID = x, title = movieTitle });
            }

            hvm.lm.AddRange(lm);

            return PartialView(hvm);
            }
        }
        public ActionResult DislikedList()
        {

            using (WatchrDataContext wdc = new WatchrDataContext())
            {
                List<Movie> dm = new List<Movie>();


                var listDislikedID = wdc.DislikedMovies.Where(x => x.user_id == (int)Session["userID"]).Select(x => x.movie_id).ToList();
                foreach (var x in listDislikedID)
                {
                    string movieTitle = wdc.Movies.Where(z => z.ID == x).Select(z => z.title).SingleOrDefault();
                    dm.Add(new Movie { ID = x, title = movieTitle });
                }

                hvm.dm.AddRange(dm);

                return PartialView(hvm);
            }
        }
        [HttpPost]
        public ActionResult DeleteLiked(int? id)
        {
            using (WatchrDataContext wdc = new WatchrDataContext())
            {
                
              
                //LikedMovy likedMovie = new LikedMovy { user_id = (int)Session["userID"], movie_id = (int)id };
                var query = wdc.LikedMovies.Where(x => x.movie_id == id && x.user_id == (int)Session["userID"]).SingleOrDefault(); 
                wdc.LikedMovies.DeleteOnSubmit(query);
                wdc.SubmitChanges();
                    }
            return Json("hi");
        }

        public ActionResult DeleteDisliked(int? id)
        {
            using (WatchrDataContext wdc = new WatchrDataContext())
            {


                //LikedMovy likedMovie = new LikedMovy { user_id = (int)Session["userID"], movie_id = (int)id };
                var query = wdc.DislikedMovies.Where(x => x.movie_id == id && x.user_id == (int)Session["userID"]).SingleOrDefault();
                wdc.DislikedMovies.DeleteOnSubmit(query);
                wdc.SubmitChanges();
            }
            return Json("hi");
        }
    }

}