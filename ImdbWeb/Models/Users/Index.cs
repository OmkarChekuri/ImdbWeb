using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImdbWeb.Domain.Models;

namespace ImdbWeb.Models.Users
{
    public class Index
    {
        public IEnumerable<User> BananaUsers
        {
            get;
            set;
        }
    }
}
