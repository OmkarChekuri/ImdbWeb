using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ImdbWeb.Domain.Models;
using ImdbWeb.Models.Movies;
using NHibernate;

namespace ImdbWeb.Controllers
{
    public class MoviesController : Controller
    {

        private readonly NHibernate.ISession _session;
       

        public MoviesController(NHibernate.ISession session)
        {
            _session = session;
        }

        public IActionResult Index()
        {
            Index Modell = new Index
            {
                BananaMovies = _session.Query<Movie>()
            };

            return View(Modell);
        }

        [HttpGet]
        public IActionResult AddMovie()
        {
            AddMovie addMovieModel = new AddMovie { };
            return View(addMovieModel);
        }

        [HttpPost, ActionName("AddMovie")]
        public IActionResult AddMovie(AddMovie addMovieModel)
        {

            
            using (var txn = _session.BeginTransaction())
            {
                string MovieName = addMovieModel.MovieName;
                int LengthInMin = addMovieModel.LengthInMin;
                string Summary = addMovieModel.Summary;
                DateTime Year = addMovieModel.Year;
                MovieGenre Genre = addMovieModel.Genre;
                RatingType ContentRating = addMovieModel.ContentRating;
                MovieLanguage Language = addMovieModel.Language;

                Movie newMovie = new Movie(MovieName, LengthInMin,  Summary, Year, Genre, ContentRating, Language );
                _session.Save(newMovie);
                txn.Commit();
            }

            //Movie newMovie = new Movie(MovieName, LengthInMin, Summary, Year, Genre, ContentRatingEnum);
            // session.Save(newMovie);
            return RedirectToAction("Index");
        }
        


        public IActionResult Details(int id)
        {
            Movie model = _session.Get<Movie>(id);
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Movie mID = _session.Get<Movie>(id);
            Edit model = new Edit
            {
                Id = mID.Id,
                MovieName = mID.MovieName,
                LengthInMin = mID.LengthInMin,
                Summary = mID.Summary,
                Year = mID.Year,
                Genre = mID.Genre,
                ContentRating = mID.ContentRating,
                Language = mID.Language,
            };

                       
            return View(model);
        }

        [HttpPost, ActionName("Edit")]
        public IActionResult Edit(Edit mdl)
         {
            Movie mID = _session.Get<Movie>(mdl.Id);

            using (var txn = _session.BeginTransaction())
            {
                mID.Id = mdl.Id;
                mID.MovieName = mdl.MovieName;
                mID.LengthInMin = mdl.LengthInMin;
                mID.Summary = mdl.Summary;
                mID.Year = mdl.Year;
                mID.Genre = mdl.Genre;
                mID.ContentRating = mdl.ContentRating;
                mID.Language = mdl.Language;
                _session.SaveOrUpdate(mID);
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
                Movie mID = _session.Get<Movie>(Id);
                _session.Delete(mID);
                _session.Flush();
                txn.Commit();
            }
            return RedirectToAction("Index");
        }



    }
}