using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieApp.Application;
using MovieApp.Domain.Model;
using MovieApp.Infrastructure;










namespace MovieApp.Presentation.Controllers
{
    //[Authorize]
    public class MoviesController : Controller {

        private readonly IMovieService _movieService;
        private readonly ILogger<MoviesController> _logger;

        public MoviesController(IMovieService movieService, ILogger<MoviesController> logger)
        {
            _movieService = movieService;
            _logger = logger;
            //constructor: Log messages related to the initialization of the controller for dependency injection 
            _logger.LogInformation("MoviesController initialized.");
        }
       
        public async Task<IActionResult> Index(string movieGenre, string searchString)
        {
            try
            {


                var genres = await _movieService.GetGenresAsync();
                var movies = await _movieService.GetAllMoviesAsync();

           
                if (!string.IsNullOrEmpty(movieGenre))
                {
                    movies = movies.Where(m => m.Genre == movieGenre).ToList();
                }

                if (!string.IsNullOrEmpty(searchString))
                {
                    movies = movies.Where(m => m.Title.Contains(searchString)).ToList();
                }

                var movieGenreVM = new MovieGenreViewModel
                {
                    Genres = new SelectList(genres),
                    Movies = movies
                };

                return View(movieGenreVM);
            }
            catch (Exception ex)
            {

                //allows you to handle exception here and return to view 
                _logger.LogError(ex, "An error occured in the Index action");
                return RedirectToAction("Error", "Home");
                    }
        }

        


        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
       
 
        public async Task<IActionResult> Create([Bind("Id,Title,ReleaseDate,Genre,Price,Rating")] Movies movie)
        {
                 //Data access log  data access operations, database queries, updates,deletions 
            {

                _logger.LogInformation("Adding:{@movie}", movie);
                await _movieService.addmovies(movie);
                _logger.LogInformation("Movie added successfully");
                TempData["Message"] = "Movie added";
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }
        
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _movieService.GetMovieByIdAsync(id.Value);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
  
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _logger.LogInformation("Attempting to delete movie with ID: {id}", id);
            _logger.LogInformation("Movie deleted successfully.");
            await _movieService.removeMovies(id);
            TempData["Message"] = "Record deleted successfully!";
            return RedirectToAction(nameof(Index));
        }


        // GET: Movies/Edit/5
     
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _movieService.GetMovieByIdAsync(id.Value);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
       
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ReleaseDate,Genre,Price,Rating")] Movies movie)
       {
    if (id != movie.Id)
    {
        return NotFound();
    }

    if (!ModelState.IsValid)
    {
        // If model state is not valid, return the view with the invalid model state
        return View(movie);
    }

    try
    {
        await _movieService.upDateMovie(movie);

        // Check if the movie was updated successfully
        if (await MovieExists(movie.Id))
        {
            TempData["Message"] = "Record updated successfully!";
            return RedirectToAction(nameof(Index));
        }
        else
        {
            // Movie update failed
            TempData["Error"] = "Failed to update movie!";
            return View(movie);
        }
    }
    catch (DbUpdateConcurrencyException)
    {
        if (!await MovieExists(movie.Id))
        {
            return NotFound();
        }
        else
        {
            throw;
        }
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "An error occurred while updating movie with ID {MovieId}", movie.Id);
        return View("Error");
    }
}

private async Task<bool> MovieExists(int id)
{
    return await _movieService.GetMovieByIdAsync(id) != null;
}


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _movieService.GetMovieByIdAsync(id.Value);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }
    }
}




    //public class MoviesController : Controller
    //{
    //    private readonly Movie_appContext _context;

    //    public MoviesController(Movie_appContext context)
    //    {
    //        _context = context;
    //    }

    //    // GET: Movies
    //    // GET: Movies
    //    public async Task<IActionResult> Index(string movieGenre, string searchString)
    //    {
    //        //Use LINQ to get list of genres.
    //       IQueryable<string> genreQuery = from m in _context.Movies
    //                                       orderby m.Genre
    //                                       select m.Genre;


    //        var movies = from m in _context.Movies
    //                     select m;

    //        if (!string.IsNullOrEmpty(searchString))
    //        {
    //            movies = movies.Where(s => s.Title!.Contains(searchString));
    //        }
    //        // This error suggests that the movieGenre parameter might be null when the filtering operation is performed, leading to the exception.

    //        //To tackle this issue, you should ensure that the movieGenre parameter is 
    //        //  not null before attempting to use it in the query. You can do this by adding a null check:
    //        if (movieGenre != null && !string.IsNullOrEmpty(movieGenre))
    //        {
    //            movies = movies.Where(x => x.Genre == movieGenre);
    //        }

    //        var movieGenreVM = new MovieGenreViewModel
    //        {
    //            Genres = new SelectList(await genreQuery.Distinct().ToListAsync()),
    //            Movies = await movies.ToListAsync()
    //        };

    //        return View(movieGenreVM);
    //    }

    //    // GET: Movies/Details/5
    //    public async Task<IActionResult> Details(int? id)
    //    {
    //        if (id == null || _context.Movies == null)
    //        {
    //            return NotFound();
    //        }

    //        var movie = await _context.Movies
    //            .FirstOrDefaultAsync(m => m.Id == id);
    //        if (movie == null)

    //        //    Putting it all together, this lambda expression is used as a predicate to filter the movies
    //        //    in the database context.It's essentially saying "find 
    //        //    the first movie where the Id property matches the
    //        //    value stored in the variable id".
    //        {
    //            return NotFound();
    //        }

    //        return View(movie);
    //    }

    //    // GET: Movies/Create
    //    public IActionResult Create()
    //    {
    //        return View();
    //    }

    //    // POST: Movies/Create
    //    // To protect from overposting attacks, enable the specific properties you want to bind to.
    //    // For more details, see  http://go.microsoft.com/fwlink/?LinkId=317598.
    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public async Task<IActionResult> Create([Bind("Id,Title,ReleaseDate,Genre,Price,Rating")] Movies movie)
    //    {
    //        if (ModelState.IsValid)
    //        
    //            _context.Add(movie);
    //            await _context.SaveChangesAsync();
    //            TempData["Message"] = "Movie added";
    //            return RedirectToAction(nameof(Index));

    //        }
    //        return View(movie);
    //    }

    //    // GET: Movies/Edit/5
    //    public async Task<IActionResult> Edit(int? id)
    //    {
    //        if (id == null || _context.Movies == null)
    //        {
    //            return NotFound();
    //        }

    //        var movie = await _context.Movies.FindAsync(id);
    //        if (movie == null)
    //        {
    //            return NotFound();
    //        }
    //        return View(movie);
    //    }

    //    // POST: Movies/Edit/5
    //    // To protect from overposting attacks, enable the specific properties you want to bind to.
    //    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ReleaseDate,Genre,Price,Rating")] Movies movie)
    //    {
    //        if (id != movie.Id)
    //        {
    //            return NotFound();
    //        }

    //        if (ModelState.IsValid)
    //        {
    //            try
    //            {
    //                _context.Update(movie);
    //                await _context.SaveChangesAsync();
    //                TempData["Message"] = "Record updated successfully!";
    //            }
    //            catch (DbUpdateConcurrencyException)
    //            {
    //                if (!MovieExists(movie.Id))
    //                {
    //                    return NotFound();
    //                }
    //                else
    //                {
    //                    throw;
    //                }
    //            }
    //            return RedirectToAction(nameof(Index));
    //        }
    //        return View(movie);
    //    }

    //    // GET: Movies/Delete/5
    //    public async Task<IActionResult> Delete(int? id)
    //    {
    //        if (id == null || _context.Movies == null)
    //        {
    //            return NotFound();
    //        }

    //        var movie = await _context.Movies
    //            .FirstOrDefaultAsync(m => m.Id == id);
    //        if (movie == null)
    //        {
    //            return NotFound();
    //        }

    //        return View(movie);
    //    }

    //    // POST: Movies/Delete/5
    //    [HttpPost, ActionName("Delete")]
    //    [ValidateAntiForgeryToken]
    //    public async Task<IActionResult> DeleteConfirmed(int id)
    //    {
    //        if (_context.Movies == null)
    //        {
    //            return Problem("Entity set 'Movie_appContext.Movie'  is null.");
    //        }
    //        var movie = await _context.Movies.FindAsync(id);
    //        if (movie != null)
    //        {
    //            _context.Movies.Remove(movie);
    //            TempData["Message"] = "Record deleted successfully!";
    //        }

    //        await _context.SaveChangesAsync();
    //        return RedirectToAction(nameof(Index));
    //    }

    //    private bool MovieExists(int id)
    //    {
    //        return (_context.Movies?.Any(e => e.Id == id)).GetValueOrDefault();
    //    }
    //}
