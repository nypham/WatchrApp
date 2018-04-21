using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Watchr.Models
{
    public class Movies
    {
        public int movie_id { get; set; }
        public string title { get; set; }
    }
    public class ListofMovies
    {
        public List<Movies> listMovies { get; set; }
    }
}