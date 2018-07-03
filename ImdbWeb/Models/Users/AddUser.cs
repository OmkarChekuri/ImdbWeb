using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ImdbWeb.Domain.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ImdbWeb.Models.Users
{
    public class AddUser
    {
        public int Id { get; set; }

        [StringLength(50, MinimumLength = 1)]
        [Required]
        public string UserName { get; set; }

        [StringLength(50, MinimumLength = 1)]
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }

        [StringLength(50, MinimumLength = 1)]
        [DataType(DataType.Password)]
        [Required]
        public string ReenterPassword { get; set; }


        [StringLength(50, MinimumLength = 1)]
        [Required]
        public string Email { get; set; }

        [StringLength(1000, MinimumLength = 1)]
        [Required]
        public string Bio { get; set; }



        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }


        public Ctry Country { get; set; }

        public IEnumerable<SelectListItem> Countries
        {
            get
            {
                return Enum.GetValues(typeof(Ctry))
                    .Cast<Ctry>()
                    .Select(x => new SelectListItem
                    {
                        Value = x.ToString(),
                        Text = x.ToString(),
                        Selected = (x == this.Country)
                    });
            }
        }
             

        
    }
}
