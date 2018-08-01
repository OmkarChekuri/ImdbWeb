using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ImdbWeb.Domain.Models;
using ImdbWeb.Models.Account;
using ImdbWeb.Infrastructure;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Net;


namespace ImdbWeb.Controllers
{
    public class AccountController : Controller
    {
        private readonly NHibernate.ISession _session;

        public AccountController(NHibernate.ISession session)
        {
            _session = session;
        }

        [HttpGet]
        public IActionResult Login()
        {

            Login LoginModel = new Login { };
            return View(LoginModel);
           
        }

        [HttpPost, ActionName("Login")]
        public IActionResult Login(Login login)
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }

            User inputUser = _session.Query<User>()
                .Where(u => u.Username == login.UserName)
                .SingleOrDefault();

            if (inputUser == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid Username");
                login.UserName = string.Empty;
                login.Password = string.Empty;
                return View(login);
            }

            if (inputUser.Password != login.Password)
            {
                ModelState.AddModelError(string.Empty, "Invalid Password");
                login.Password = string.Empty;
                return View(login);
            }

            //var user = _session.Get<User>(inputUser.Id);

            Claim UserIdClaim = new Claim(AspNetCurrentUserContext.UserIdKey, inputUser.Id.ToString());

            Claim UserNameClaim = new Claim(AspNetCurrentUserContext.UserNameKey, inputUser.Username);

            ClaimsIdentity UserIdentity = new ClaimsIdentity(new Claim[] { UserIdClaim, UserNameClaim }, "ImdbLogin");

            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(UserIdentity));

            return RedirectToAction("Index", "Movies");
            
        }

        //[HttpPost, ActionName("Logout")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }

}