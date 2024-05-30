using MovieApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Application
{
    public class MovieService : IMovieService
    {
        private readonly IUnitofWork _unitofWork;
        public MovieService(IUnitofWork unitofWork)
        {

            _unitofWork = unitofWork;

        }

        public async Task addmovies(Movies movie)
        {

            if (movie == null)
            {
                throw new ArgumentNullException(nameof(movie));
            }

            _unitofWork.Movies.addMovieAsync(movie);
            await _unitofWork.CompleteAsync();
        }

        public async Task<MovieGenreViewModel> getMovie()
        {
            var movies = await _unitofWork.Movies.GetMoviesAsync();
            return new MovieGenreViewModel()
            {
                Movies = movies,
            };
        }

        public async Task<Movies> GetMovieByIdAsync(int id)
        {
            return  await _unitofWork.Movies.GetMovieByIdAsync(id);
        }

        public async Task removeMovies(int id)
        {
             await _unitofWork.Movies.DeleteMovieAsync(id);
        }

        public async Task upDateMovie(Movies movie)
        {
            {
                if (movie == null)
                {
                    throw new ArgumentNullException(nameof(movie));
                }

                await _unitofWork.Movies.UpdateMovie(movie);
            }
        }

        public async Task<Movies> GetMovieDetailsAsync(int id)
        {
            return await _unitofWork.Movies.GetMovieByIdAsync(id);
        }

        public async Task<List<string>> GetGenresAsync()
        {
            return await _unitofWork.Movies.GetGenresAsync();
        }

        public async Task<List<Movies>> GetAllMoviesAsync()
        {
            return await _unitofWork.Movies.GetAllMoviesAsync();
        }
    }
}

