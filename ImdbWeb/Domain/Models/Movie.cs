using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImdbWeb.Domain.Models
{
    public enum RatingType
    {G, PG, PG13, R, Other}

    public enum MovieLanguage
    {
        English, Spanish, French, German, Russian, Hindi, Chinese, Japanese, Korean, Telugu, Italian, Other
    }

    public enum MovieGenre
    {
        Action, Adventure, Comedy, Crime, Drama, Fantasy, Historical, Horror, Magic, Mystery,ParanoidFiction, Philosophical, Political,
        Romance, Saga, Satire, ScienceFiction, Social, Speculative, Thriller, Urban, Western, Other
    }
    public class Movie
    {
        //convention to use caps for getters and setters
        public virtual int Id { get; set; }
        public virtual string MovieName { get; set; }
        public virtual int LengthInMin { get; set; }
        public virtual string Summary { get; set; }
        public virtual DateTime Year { get; set; }
        public virtual MovieGenre Genre { get; set; }
        public virtual RatingType ContentRating { get; set; }
        public virtual MovieLanguage Language { get; set; }
        public Movie(string newMovieName, int newLengthInMin, string newSummary, DateTime newYear, MovieGenre newGenre, RatingType newContentRating, MovieLanguage newLanguage)
        {
            MovieName   = newMovieName;
            LengthInMin = newLengthInMin;
            Summary     = newSummary;
            Year = newYear;
            Genre = newGenre;
            ContentRating = newContentRating;
            Language = newLanguage;
        }
        //for NHibernate
        protected Movie()   {       }
    }

}
