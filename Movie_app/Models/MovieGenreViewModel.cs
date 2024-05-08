using Humanizer.Localisation;
using Humanizer;
using Microsoft.AspNetCore.Mvc.Rendering;
using Movie_app.Models;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;

namespace Movie_app.Models
{
    public class MovieGenreViewModel
    {
        public List<Movie>? Movies { get; set; }
        public SelectList? Genres { get; set; }
        public string? MovieGenre { get; set; }
        public string? SearchString { get; set; }
    }
}

//    The movie-genre view model will contain:

//A list of movies.
//A SelectList containing the list of genres. This allows the user to select a genre from the list.
//MovieGenre, which contains the selected genre.
//SearchString, which contains the text users enter in the search text box.