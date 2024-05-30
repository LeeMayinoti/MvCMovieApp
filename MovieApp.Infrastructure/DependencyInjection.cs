using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MovieApp.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Infrastructure
{
    //public static class DependencyInjection
    //{
    //    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    //    {
    //        services.AddDbContext<Movie_appContext>(options => 
    //        options.UseSqlServer(configuration.GetConnectionString("Movie_appContext")));

    //    return services;
    //    }
    //}
    public class DependencyInjection
    {
        public static void RegisterServices(IServiceCollection services)
        {
            //CleanArchitecture.Application
            services.AddScoped<IMovieService, MovieService>();

            //CleanArchitecture.Domain.Interfaces | CleanArchitecture.Infra.Data.Repositories
            services.AddScoped<IMovieRepository, MovieRepository>();

            // Register UnitOfWork
            services.AddScoped<IUnitofWork, UnitofWork>();

            //services.AddDbContext < AppDBContext >> (c => c.UseSqlServer(connectionString));
        }


    }
}
