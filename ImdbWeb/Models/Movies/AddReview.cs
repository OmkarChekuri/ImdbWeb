using ImdbWeb.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ImdbWeb.Models.Movies
{
    public class AddReview
    {
        

        public int Id { get; set; }

        public int UserId { get; set; }

        public  int MovieId { get; set; }

        public string MovieName { get; set; }

        [Range(1, 10)]
        [Required]
        public int Rating { get; set; }


        [StringLength(1000, MinimumLength = 1)]
        [Required]
        public string ReviewText { get; set; }
    }
}
