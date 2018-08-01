using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImdbWeb.Infrastructure
{
    public interface ICurrentUserContext
    {

        int UserId { get; }

        string UserName { get; }

        bool IsAuthenticated { get; }
    }
}
