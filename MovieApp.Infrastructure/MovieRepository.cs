using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using MovieApp.Application;
using MovieApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Infrastructure 
{

    public class MovieRepository : IMovieRepository
    {


        public Movie_appContext _context;
        public MovieRepository(Movie_appContext context)
        {
            _context = context;
        }

        public async Task addMovieAsync(Movies movie)
        {
            //await   _context.SaveChangesAsync();
            await _context.Movies.AddAsync(movie);
            //await _context.SaveChangesAsync();

        }

        public async Task DeleteMovieAsync(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie != null)
            {
                _context.Movies.Remove(movie);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Movies>> GetAllMoviesAsync()
        {
            return await _context.Movies.ToListAsync();
        }

        public async Task<List<string>> GetGenresAsync()
        {
            return await _context.Movies.Select(m => m.Genre).Distinct().ToListAsync();
        }

        public async Task<Movies> GetMovieByIdAsync(int id)
        {
            return await _context.Movies.FindAsync(id);
        }

      

        public async Task<List<Movies>> GetMoviesAsync()
        {
            return await  _context.Movies.ToListAsync();
        }

        public async  Task UpdateMovie(Movies movies)
        {
            _context.Update(movies);
            await _context.SaveChangesAsync();
        }

     
    }
}

