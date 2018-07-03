using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImdbWeb.Domain.Models
{
    public enum Ctry
    {
        Argentina, Australia,Belgium,Brazil, Croatia,CostaRica,Colombia, China, Denmark,
        England, Egypt, France, Germany, India, Iceland, Iran,Japan,Mexico, Morocco, Nigeria,
        Peru,  Poland, Portugal, Panama, Qatar, Russia, Switzerland, Sweden,Serbia, Spain,
        Senegal, Tunisia, Uruguay, USA, Other
    }

    public class User
    {
        public virtual int Id { get; set; }
        public virtual string Username { get; set; } 
        public virtual string Password { get; set; }
        public virtual string Email { get; set; }
        public virtual string Bio { get; set; }
        public virtual DateTime DOB { get; set; }
        public virtual Ctry Country { get; set; }
        public User(string NewUsername, string NewPassword, string NewEmail, Ctry NewCountry, string NewBio, DateTime NewDOB)
        {
            Username = NewUsername;
            Password = NewPassword;
            Email = NewEmail;
            Country = NewCountry;
            Bio = NewBio;
            DOB = NewDOB;
        }

        //for NHibernate
        protected User() { }

    }
}
