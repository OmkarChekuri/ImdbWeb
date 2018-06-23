using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImdbWeb.Domain.Models
{
    public class User
    {
        public virtual int Id { get; set; }

        public virtual string Username { get; set; } 
        
        public virtual string Password { get; set; }

        public virtual string Address { get; set; }

        public User(string NewUsername, string NewPassword, string NewAddress)
        {
            Username = NewUsername;
            Password = NewPassword;
            Address = NewAddress;

        }
        //for NHibernate
        protected User()
        {
           

        }

    }
}
