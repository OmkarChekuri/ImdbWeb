﻿using FluentNHibernate.Mapping;
using ImdbWeb.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImdbWeb.Domain.Mapping
{
    public class MovieMap:ClassMap<Movie>
    {

        public MovieMap()
        {

            Table("Movies");
            Id(u => u.Id).Not.Nullable();
            Map(u => u.movieName).Not.Nullable();
            Map(u => u.LengthInMin).Not.Nullable();
            Map(u => u.Summary).Not.Nullable();
            Map(u => u.Year).Not.Nullable();
            Map(u => u.Genre).Not.Nullable();
            Map(u => u.ContentRating).CustomType<RatingType>();
                        
        }


    }
}
