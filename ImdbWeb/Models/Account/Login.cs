using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImdbWeb.Domain.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ImdbWeb.Models.Account
{
    public class Login
    {

            [StringLength(50, MinimumLength = 1)]
            [Required]
            public string UserName { get; set; }

            [StringLength(50, MinimumLength = 1)]
            [DataType(DataType.Password)]
            [Required]
            public string Password { get; set; }

    }

}

