using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImdbWeb.Domain.Models
{
    public class Review
    {
        
        public virtual int Id { get; set; }
        public virtual User User { get; set; }
        public virtual Movie Movie { get; set; }
        public virtual string ReviewText { get; set; }
        public virtual int Rating { get; set; }
        
        public Review( User newUser, Movie newMovie, string newReviewText, int newRating)
        {
            User = newUser;
            Movie = newMovie;
            ReviewText = newReviewText;
            Rating = newRating;
        }
        //for NHibernate
        protected Review() { }

        
    }
}
