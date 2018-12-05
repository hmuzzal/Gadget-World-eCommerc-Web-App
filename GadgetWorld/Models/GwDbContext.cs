using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GadgetWorld.Models
{
    public class GwDbContext:DbContext
    {
        public GwDbContext() : base("GadgetWorld")
        {
        }

        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Product> Products { get; set; }

        public DbSet<Gender> Genders { get; set; }
        public System.Data.Entity.DbSet<GadgetWorld.Models.UserLoginModel> UserLoginModels { get; set; }
    }
}