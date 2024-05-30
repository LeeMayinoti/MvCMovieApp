using Microsoft.EntityFrameworkCore;
using MovieApp.Application;
using MovieApp.Domain.Model;
using Microsoft.IdentityModel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace MovieApp.Infrastructure
{

    public class Movie_appContext : DbContext
    {
        
        public Movie_appContext(DbContextOptions<Movie_appContext> options)
            : base(options)
        {
        }


        public DbSet<Movies> Movies { get; set; }
        

     
    }
}

