using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator;

namespace FluentMigrations
{
    [Migration(1)]
    public class M1_AddRatingSystem : Migration
    {
        public override void Down()
        {

            //Deletes the table if we want to undo the changes.
            Delete.Table("Reviews");
        }

        public override void Up()
        {
            Create.Table("Reviews")
                .WithColumn("Id").AsInt32().NotNullable().Identity().PrimaryKey()
                .WithColumn("UserId").AsInt32().NotNullable()
                .WithColumn("MovieId").AsInt32().NotNullable()
                .WithColumn("ReviewText").AsString(200).Nullable()
                .WithColumn("Rating").AsInt32().NotNullable();

            Create.ForeignKey()
                .FromTable("Reviews").ForeignColumn("UserId")
                .ToTable("Users").PrimaryColumn("Id");

            Create.ForeignKey()
                .FromTable("Reviews").ForeignColumn("MovieId")
                .ToTable("Movies").PrimaryColumn("Id");


        }



    }
}
