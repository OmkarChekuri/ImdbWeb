using FluentNHibernate.Mapping;
using ImdbWeb.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImdbWeb.Domain.Mapping
{
    public class UserMap:ClassMap<User>
    {

        public UserMap ()
        {

            Table("Users");
            Id(u => u.Id).Not.Nullable();
            Map(u => u.Username).Not.Nullable();
            Map(u => u.Password).Not.Nullable();
            Map(u => u.Email).Not.Nullable();
            Map(u => u.Bio).Nullable();
            Map(u => u.DOB).Not.Nullable();
            Map(u => u.Country).CustomType<Ctry>();
            
        }



    }
}
