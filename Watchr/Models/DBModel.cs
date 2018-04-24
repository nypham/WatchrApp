using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Watchr.Models
{
    public class DBModel
    {
        public List<Movie> movies { get; set; }
        public List<LikedMovy> lm { get; set; }
        public List<DislikedMovy> dm { get; set; }
        public List<User> users { get; set; }
        public List<Feedback> feedbacks { get; set; }

        public DBModel()
        {
            using (WatchrDataContext db = new WatchrDataContext())
            {
                movies = new List<Movie>();
                lm = new List<LikedMovy>();
                dm = new List<DislikedMovy>();
                users = new List<User>();
                feedbacks = new List<Feedback>();

                movies.AddRange(db.Movies.ToList());
                foreach(var x in db.LikedMovies)
                {
                    lm.Add(new LikedMovy { user_id = x.user_id, movie_id = x.movie_id });
                }
                foreach (var x in db.DislikedMovies)
                {
                    dm.Add(new DislikedMovy { user_id = x.user_id, movie_id = x.movie_id });
                }
                users.AddRange(db.Users.ToList());
                feedbacks.AddRange(db.Feedbacks.ToList());
            }
        }
    }
}