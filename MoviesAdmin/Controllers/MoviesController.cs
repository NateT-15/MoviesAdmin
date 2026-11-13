using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesAdmin.Models;
using MoviesAdmin.Services;

[Authorize]

public class MoviesController : Controller
{
    private readonly MoviesAdminContext _context;
    private readonly AzureStorageService _azureStorage;

    public MoviesController(MoviesAdminContext context, AzureStorageService azureStorage)
    {
        _context = context;
        _azureStorage = azureStorage;
    }

    // GET: MOVIES/Index
    public async Task<IActionResult> Index()
    {
        List<Movie> movies = await _context.Movie
            .Include(m => m.Genre)
            .Include(m => m.AudienceRating)
            .ToListAsync();

        return View(movies);
    }

    // GET: MOVIES/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var movie = await _context.Movie
            .Include(m => m.Genre)
            .Include(m => m.AudienceRating)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (movie == null)
        {
            return NotFound();
        }

        ViewBag.ReviewCount =
            await _context.CriticReview
                .CountAsync(r =>
                    r.MovieId == movie.Id &&
                    r.IsPublished);

        return View(movie);
    }


    // GET: MOVIES/Create
    [Authorize(Roles = "Administrator")]
    public IActionResult Create()
    {
        ViewData["GenreId"] = new SelectList(_context.Genre, "Id", "Title");
        ViewData["AudienceRatingId"] = new SelectList(_context.AudienceRating, "Id", "Title");
        return View();
    }

    // POST: MOVIES/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [Authorize(Roles = "Administrator")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Title,Description,GenreId,AudienceRatingId,RuntimeMinutes,ReleaseDate")] Movie movie, IFormFile ImageUpload
        )
    {
        if (ImageUpload == null)
        {
            ModelState.AddModelError("ImageUpload", "Image file is required");
        }

        if (ModelState.IsValid)
        {
            //Upload file to Azure storage (after input validation occurs)

            //Set a unique filename (GUID.ext)
            string extension = Path.GetExtension(ImageUpload.FileName);
            string newFileName = $"{Guid.NewGuid()}{extension}";

            //upload file to Azure Storage
            string url = await _azureStorage.UploadFileAsync(ImageUpload, newFileName);

            //Set the image url
            movie.ImageUrl = url;

            //Save movie in database
            _context.Add(movie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewData["GenreId"] = new SelectList(_context.Genre, "Id", "Title", movie.GenreId);
        ViewData["AudienceRatingId"] = new SelectList(_context.AudienceRating, "Id", "Title", movie.AudienceRatingId);
        return View(movie);
    }

    // GET: MOVIES/Edit/5
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var movie = await _context.Movie.FindAsync(id);
        if (movie == null)
        {
            return NotFound();
        }

        ViewData["GenreId"] = new SelectList(_context.Genre, "Id", "Title", movie.GenreId);
        ViewData["AudienceRatingId"] = new SelectList(_context.AudienceRating, "Id", "Title", movie.AudienceRatingId);
        return View(movie);
    }

    // POST: MOVIES/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [Authorize(Roles = "Administrator")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Title,Description,GenreId,AudienceRatingId,RuntimeMinutes,ReleaseDate,ImageUrl")] Movie movie, IFormFile? ImageUpload)
    {
        if (id != movie.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                // Only upload if a new file was chosen; otherwise keep the existing ImageUrl
                if (ImageUpload != null && ImageUpload.Length > 0)
                {
                    string extension = Path.GetExtension(ImageUpload.FileName);
                    string newFileName = $"{Guid.NewGuid()}{extension}";
                    movie.ImageUrl = await _azureStorage.UploadFileAsync(ImageUpload, newFileName);
                }
                _context.Update(movie);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(movie.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }

        ViewData["GenreId"] = new SelectList(_context.Genre, "Id", "Title", movie.GenreId);
        ViewData["AudienceRatingId"] = new SelectList(_context.AudienceRating, "Id", "Title", movie.AudienceRatingId);
        return View(movie);
    }

    // GET: MOVIES/Delete/5
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var movie = await _context.Movie
            .Include(m => m.Genre)
            .Include(m => m.AudienceRating)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (movie == null)
        {
            return NotFound();
        }

        return View(movie);
    }

    // POST: MOVIES/Delete/5
    [Authorize(Roles = "Administrator")]
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var movie = await _context.Movie.FindAsync(id);
        if (movie != null)
        {
            _context.Movie.Remove(movie);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool MovieExists(int? id)
    {
        return _context.Movie.Any(e => e.Id == id);
    }
}