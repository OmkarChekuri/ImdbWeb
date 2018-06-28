using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NHibernate;
using ImdbWeb.Domain.Models;
using ImdbWeb.Models.Movies;

namespace ImdbWeb.Controllers
{
    public class MoviesController : Controller
    {

        //private readonly ISession _session;

        //public MoviesController(ISession session)
        //{
        //    _session = session;
        //}


        private readonly NHibernate.ISession _session;
       

        public MoviesController(NHibernate.ISession session)
        {
            _session = session;
           
        }



        public IActionResult Index()
        {
            Index Model = new Index();
            Model.Movies = _session.Query<Movie>();

            return View(Model);
        }

        public IActionResult Details(int id)
        {
            Movie model = _session.Get<Movie>(id);
            return View(model);
        }

        [HttpGet("/Edit")]
        public IActionResult Edit(int id)
        {
           
            Movie mID = _session.Get<Movie>(id);
            


            return View(mID);
        }

      
        [HttpPost]
        public IActionResult Delete(int Id)
        {
            using (var txn = _session.BeginTransaction())
            {
                Movie mID = _session.Get<Movie>(Id);
                _session.Delete(mID);
                _session.Flush();
                txn.Commit();
            }
            return RedirectToAction("Index");
        }



    }
}