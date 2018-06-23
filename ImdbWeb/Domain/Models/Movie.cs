using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImdbWeb.Domain.Models
{
    public enum RatingType
    {
        G,PG,PG13,R
    }
    public class Movie

    {
        //convention to use caps for getters and setters

        public virtual int Id { get; set; }

        public virtual string movieName { get; set; }

        public virtual int LengthInMin { get; set; }

        public virtual string Summary { get; set; }

        public virtual int Year { get; set; }

        public virtual string Genre { get; set; }

        public virtual RatingType ContentRating { get; set; }

        public Movie(string newMovieName, int newLengthInMin, string newSummary, int newYear, string newGenre, RatingType newContentRating)
        {
            movieName   = newMovieName;
            LengthInMin = newLengthInMin;
            Summary     = newSummary;
            Year = newYear;
            Genre = newGenre;
            ContentRating = newContentRating;

        }
        //for NHibernate
        protected Movie()
        {


        }


    }


}
