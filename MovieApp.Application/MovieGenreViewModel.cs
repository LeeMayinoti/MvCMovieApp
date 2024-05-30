using Microsoft.AspNetCore.Mvc.Rendering;
using MovieApp.Domain.Model;

namespace MovieApp.Application
{
    public class MovieGenreViewModel
    {

        public List<Movies>? Movies { get; set; }
        public SelectList? Genres { get; set; }
        public string? MovieGenre { get; set; }
        public string? SearchString { get; set; }
    }

}

