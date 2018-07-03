using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ImdbWeb.Domain.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ImdbWeb.Models.Movies
{
    public class Edit
    {
        public int Id { get; set; }

        [StringLength(50, MinimumLength = 1)]
        [Required]
        public string MovieName { get; set; }


        [Range(1, 180)]
        public int LengthInMin { get; set; }

        [StringLength(1000, MinimumLength = 1)]
        [Required]
        public string Summary { get; set; }

       
        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        public DateTime Year { get; set; }

        public MovieGenre Genre { get; set; }

        public IEnumerable<SelectListItem> Genres
        {
            get
            {
                return Enum.GetValues(typeof(MovieGenre))
                    .Cast<MovieGenre>()
                    .Select(x => new SelectListItem
                    {
                        Value = x.ToString(),
                        Text = x.ToString(),
                        Selected = (x == this.Genre)
                    });
            }
        }


        public RatingType ContentRating { get; set; }

        public IEnumerable<SelectListItem> Ratings
        {
            get
            {

                return Enum.GetValues(typeof(RatingType))
                    .Cast<RatingType>()
                    .Select(x => new SelectListItem
                    {
                        Value = x.ToString(),
                        Text = x.ToString(),
                        Selected = (x == this.ContentRating)

                    });

            }

        }

        public MovieLanguage Language { get; set; }

        public IEnumerable<SelectListItem> Languages
        {
            get
            {

                return Enum.GetValues(typeof(MovieLanguage))
                    .Cast<MovieLanguage>()
                    .Select(x => new SelectListItem
                    {
                        Value = x.ToString(),
                        Text = x.ToString(),
                        Selected = (x == this.Language)
                    });
            }
        }

    }
}
