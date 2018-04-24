using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Watchr.Models;

namespace Watchr.Controllers
{
    public class MoviesController : Controller
    {
        ListofMovies movieList = new ListofMovies();
        // GET: Movies

        public ActionResult Movies()
        {
            if (Session["userID"] != null) { 

            List<Movies> keptMovies = new List<Movies>();

            using (WatchrDataContext wdc = new WatchrDataContext())
            {

                var movieIds = wdc.Movies.ToList();
                var disliked = wdc.DislikedMovies.Where(x => x.user_id == (int)Session["userID"]).Select(x => x.movie_id).ToList();
                var liked = wdc.LikedMovies.Where(x => x.user_id == (int)Session["userID"]).Select(x => x.movie_id).ToList();
                foreach (var x in movieIds)
                {
                    if (!disliked.Contains(x.ID) && !liked.Contains(x.ID))
                    {
                        keptMovies.Add(new Models.Movies { movie_id = x.ID, title = x.title });
                    }
                }
                movieList.listMovies = keptMovies;

            }


            return View(movieList);
            }
            else { return RedirectToAction("Login", "RegisterandLogin"); }
        }
        [HttpPost]
        public ActionResult AddLiked(int id)
        {
            using (WatchrDataContext wdc = new WatchrDataContext())
            {
                LikedMovy lm = new LikedMovy {movie_id=id,user_id=(int)Session["userID"] };
                wdc.LikedMovies.InsertOnSubmit(lm);
                wdc.SubmitChanges();
                
            }
            return Json("dislike");

        }
        [HttpPost]
        public ActionResult AddDisliked(int id)
        {
            using (WatchrDataContext wdc = new WatchrDataContext())
            {
                DislikedMovy dm = new DislikedMovy { movie_id = id, user_id = (int)Session["userID"] };
                wdc.DislikedMovies.InsertOnSubmit(dm);
                wdc.SubmitChanges();
            }
            return Json("dislike");

        }
        public ActionResult MovieList()
        {
            
            List<Movies> keptMovies = new List<Movies>();

            using (WatchrDataContext wdc = new WatchrDataContext())
            {

                var movieIds = wdc.Movies.ToList();
                var disliked = wdc.DislikedMovies.Where(x => x.user_id == (int)Session["userID"]).Select(x => x.movie_id).ToList();
                var liked = wdc.LikedMovies.Where(x => x.user_id == (int)Session["userID"]).Select(x => x.movie_id).ToList();
                foreach (var x in movieIds)
                {
                    if (!disliked.Contains(x.ID) && !liked.Contains(x.ID))
                    {
                        keptMovies.Add(new Models.Movies { movie_id = x.ID, title = x.title });
                    }
                }
                movieList.listMovies = keptMovies;

            }
            return PartialView(movieList);
        }
    }
}