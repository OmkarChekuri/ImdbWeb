using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;   //Claims principle

namespace ImdbWeb.Infrastructure
{
    public class AspNetCurrentUserContext : ICurrentUserContext
    {

        private readonly Func<ClaimsPrincipal> _provider;
        public AspNetCurrentUserContext(Func<ClaimsPrincipal> userProvider)
        {
            _provider = userProvider;
        }

        public bool IsAuthenticated => _provider().Identity.IsAuthenticated;

        public const String UserNameKey = ClaimTypes.Name;

        public const String UserIdKey = "UserId";

        public string UserName
        {
            get
            {

                string value = _provider().Claims.SingleOrDefault(x => x.Type == UserNameKey)?.Value;  //  ?.value Avoids Null pointer exception
                if (value == null)
                {
                    // We throw our OWN "KeyNotFoundException" here instead of just calling "Single" above because the error message that "Single" provides when it
                    // doesn't find anything isn't very helpful in diagnosing what went wrong.
                    throw new KeyNotFoundException($"The cookie did not contain a value for the key \"{UserNameKey}\".");
                }
                return value;
            }
        }

        public int UserId
        {
            get
            {
                string rawValue = _provider().Claims.SingleOrDefault(x => x.Type == UserIdKey)?.Value;
                if (rawValue == null)
                {
                    throw new KeyNotFoundException($"The cookie did not contain a value for the key \"{UserIdKey}\".");
                }
                if (!Int32.TryParse(rawValue, out int result))
                {
                    throw new FormatException($"The cookie value for the User Id was in a bad format!");
                }
                return result;
            }
        }

    }
}
