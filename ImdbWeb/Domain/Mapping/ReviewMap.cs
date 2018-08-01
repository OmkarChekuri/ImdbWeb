using FluentNHibernate.Mapping;
using ImdbWeb.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImdbWeb.Domain.Mapping
{
    public class ReviewMap:ClassMap<Review>
    {
        public ReviewMap()
        {

            Table("Reviews");
            Id(r => r.Id).Not.Nullable();
            Map(r => r.ReviewText).Nullable();
            Map(r => r.Rating).Not.Nullable();
            References(r => r.User, "UserId").Not.Nullable();
            References(r => r.Movie, "MovieId").Not.Nullable();
        }
    }
}
