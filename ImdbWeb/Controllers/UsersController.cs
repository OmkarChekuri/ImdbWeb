using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ImdbWeb.Domain.Models;
using ImdbWeb.Models.Users;
using NHibernate;


namespace ImdbWeb.Controllers
{
    public class UsersController : Controller
    {

        private readonly NHibernate.ISession _session;

        public UsersController(NHibernate.ISession session)
        {
            _session = session;
        }

        // GET: Users
        public IActionResult Index()
        {

            Index Model3 = new Index
            {
                BananaUsers = _session.Query<User>()
            };

            return View(Model3);

        }


        [HttpGet]
        public IActionResult AddUser()
        {
            AddUser addUserModel = new AddUser { };
            return View(addUserModel);
        }

        [HttpPost, ActionName("AddUser")]
        public IActionResult AddUser(AddUser addUserModel)
        {
            if(addUserModel.Password.Equals(addUserModel.ReenterPassword))
            {
                using (var txn = _session.BeginTransaction())
                {
                    string UserName = addUserModel.UserName;
                    string Password = addUserModel.Password;
                    //string ReenterPassword = addUserModel.ReenterPassword;
                    string Email = addUserModel.Email;
                    string Bio = addUserModel.Bio;
                    DateTime DOB = addUserModel.DOB;
                    Ctry Country = addUserModel.Country;

                    User newUser = new User(UserName, Password, Email, Country, Bio, DOB);
                    _session.Save(newUser);
                    txn.Commit();
                }

                return RedirectToAction("Index");

            }
            else
            {
                ModelState.AddModelError("", "Passwords Donot Match");
                return View(addUserModel);
            }


        }

        public IActionResult Details(int id)
        {
            User model = _session.Get<User>(id);
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            User uID = _session.Get<User>(id);
            Edit model = new Edit
            {
                Id = uID.Id,
                UserName = uID.Username,
                Password = uID.Password,
                Email = uID.Email,
                Country = uID.Country,
                DOB = uID.DOB,
                Bio = uID.Bio,
                
            };


            return View(model);
        }

        [HttpPost, ActionName("Edit")]
        public IActionResult Edit(Edit mdl)
        {
            User uID = _session.Get<User>(mdl.Id);

            using (var txn = _session.BeginTransaction())
            {
                uID.Id = mdl.Id;
                uID.Username = mdl.UserName;
                uID.Password = mdl.Password;
                uID.Email = mdl.Email;
                uID.DOB = mdl.DOB;
                uID.Country = mdl.Country;
                uID.Bio = mdl.Bio;
                _session.SaveOrUpdate(uID);
                _session.Flush();
                txn.Commit();
            }
            return RedirectToAction("Index");

        }

        [HttpPost, ActionName("Delete")]
        public IActionResult Delete(int Id)
        {
            using (var txn = _session.BeginTransaction())
            {
                User uID = _session.Get<User>(Id);
                _session.Delete(uID);
                _session.Flush();
                txn.Commit();
            }
            return RedirectToAction("Index");
        }


    }

}