using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImdbWeb.Domain.Models;

namespace ImdbWeb.Models.Movies
{
    public class Index
    {
        public IEnumerable<Movie> BananaMovies
        {
            get;
            set;
        }

        public string UserName
        {
            get;
            set;
        }

        public IEnumerable<Review> BananaReviews
        {
            get;
            set;
        }
    }
}

