using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using MovieApp.Application;
using MovieApp.Domain.Model;

namespace MovieApp.Application
{
    public interface IMovieService
    {
     
        Task<MovieGenreViewModel> getMovie();
        Task addmovies(Movies movie);

        Task removeMovies(int id);

        Task<Movies> GetMovieByIdAsync(int id);

        Task upDateMovie(Movies movie);

        Task<List<string>> GetGenresAsync();

        Task<List<Movies>> GetAllMoviesAsync();
    }
}