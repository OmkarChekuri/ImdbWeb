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

        private readonly ISession _session;

        public MoviesController(ISession session)
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


    }
}