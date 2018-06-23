using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImdbWeb.Domain.Models;

namespace ImdbWeb.Models.Movies
{
    public class Index
    {
        public IEnumerable<Movie> Movies
        {
            get;
            set;
        }


    }
}

