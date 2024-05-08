using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Movie_app.Models;

namespace Movie_app.Data
{
    public class Movie_appContext : DbContext
    {
        public Movie_appContext (DbContextOptions<Movie_appContext> options)
            : base(options)
        {
        }

        public DbSet<Movie_app.Models.Movie> Movie { get; set; } = default!;
    }
}
