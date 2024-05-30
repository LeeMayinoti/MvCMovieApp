using Microsoft.AspNetCore.Mvc.Rendering;
using MovieApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Application
{
    public  interface IMovieRepository
    {
        Task<List<Movies>> GetMoviesAsync();
        //Task<SelectList> GetMovieGenre();
        

        //Task<Movies> GetMovieByIdAsync(int id);
        Task addMovieAsync(Movies movie);
        //Task UpdateMovieAsync(Movies movie);
        Task DeleteMovieAsync(int id);
        Task<Movies> GetMovieByIdAsync(int id);

        Task UpdateMovie(Movies movie);
        Task<List<Movies>> GetAllMoviesAsync();
        Task<List<string>> GetGenresAsync();

    }

  
}
