using MovieApp.Application;
using MovieApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MovieApp.Infrastructure.UnitofWork;


namespace MovieApp.Infrastructure
{
    public class UnitofWork : IUnitofWork
    {

        private readonly Movie_appContext _context;

        public UnitofWork(Movie_appContext context)
        {
            _context = context;
            Movies = new MovieRepository(_context);
        }

        // Add repositories here
        public IMovieRepository Movies { get; private set; }
        // Add more repositories

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }


    }
}
