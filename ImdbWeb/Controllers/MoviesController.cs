using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ImdbWeb.Domain.Models;
using ImdbWeb.Models.Movies;
using NHibernate;
using ImdbWeb.Infrastructure;
using System.Security.Claims;

namespace ImdbWeb.Controllers
{
    public class MoviesController : Controller
    {

        private readonly NHibernate.ISession _session;
       
        private readonly ICurrentUserContext _context;
        

        public MoviesController(NHibernate.ISession session,  ICurrentUserContext context)
        {
            _session = session;
            _context = context;
        }

        public IActionResult Index( )
        {


            Index Modell = new Index
            {
                BananaMovies = _session.Query<Movie>(),
                UserName = _context.UserName

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

        //Movie MovieName = _session.Get<Movie>(addReviewModel.MovieId);
        //User UserName = _session.Get<User>(addReviewModel.UserId);
        //string ReviewText = addReviewModel.ReviewText;
        //int Rating = addReviewModel.Rating;
        //Review newReview = new Review(UserName, MovieName, ReviewText, Rating);


        [HttpGet]
        public IActionResult Edit(int id)
        {
            Movie mID = _session.Get<Movie>(id);
            Review review = mID.Reviews.SingleOrDefault(r => r.User.Id  == _context.UserId);

            int rating = review?.Rating ?? 0;
            string reviewText = review?.ReviewText ?? "Not Yet Reviewed";

               // Get<Review>().Where(x =>x.MovieId == id).FirstOrDefault;
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
                Rating = rating,
                ReviewText = reviewText,
            };

                       
            return View(model);
        }

        [HttpPost, ActionName("Edit")]
        public IActionResult Edit(Edit mdl)
         {
            Movie mID = _session.Get<Movie>(mdl.Id);
            Review review = mID.Reviews.SingleOrDefault(r => r.User.Id == _context.UserId);
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
                review.Rating = mdl.Rating;
                review.ReviewText = mdl.ReviewText;

                //_session.SaveOrUpdate(mID);
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


        [HttpPost, ActionName("ReviewDelete")]
        public IActionResult ReviewDelete(int Id)
        {
            using (var txn = _session.BeginTransaction())
            {
                Review rID = _session.Get<Review>(Id);
                _session.Delete(rID);
                _session.Flush();
                txn.Commit();
            }
            return RedirectToAction("Index");
        }



        [HttpGet]
        public IActionResult AddReview(int Id)
        {
            Movie mID = _session.Get<Movie>(Id);
            AddReview addReviewModel = new AddReview
            {
               UserId = _context.UserId,
               MovieId = mID.Id,
               MovieName = mID.MovieName,
            };
            return View(addReviewModel);
        }


        [HttpPost, ActionName("AddReview")]
        public IActionResult AddReview(AddReview addReviewModel)
        {
            
            using (var txn = _session.BeginTransaction())
            {

                //Movie MovieName = addReviewModel.Movie;
                //User UserName = addReviewModel.User;
                Movie MovieName = _session.Get<Movie>(addReviewModel.MovieId);
                User UserName = _session.Get<User>(addReviewModel.UserId);
                string ReviewText = addReviewModel.ReviewText;
                int Rating = addReviewModel.Rating;
                Review newReview = new Review(UserName, MovieName, ReviewText, Rating);
                _session.Save(newReview);
                txn.Commit();
            }
            return RedirectToAction("Index");

        }

          

    }
}