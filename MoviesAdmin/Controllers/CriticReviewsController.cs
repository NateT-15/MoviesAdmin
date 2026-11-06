using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesAdmin.Models;

public class CriticReviewsController : Controller
{
    private readonly MoviesAdminContext _context;

    public CriticReviewsController(MoviesAdminContext context)
    {
        _context = context;
    }

    // GET: CRITICREVIEWS/Index
    public async Task<IActionResult> Index()
    {
        ViewBag.PageTitle =
            User.IsInRole("Administrator")
                ? "Reviews"
                : "My Reviews";

        if (User.IsInRole("Administrator"))
        {
            return View(await _context.CriticReview
                .Include(r => r.Movie)
                .ToListAsync());
        }

        string currentUser =
            User.Identity?.Name ?? "";

        return View(await _context.CriticReview
            .Include(r => r.Movie)
            .Where(r => r.CreatedBy == currentUser)
            .ToListAsync());
    }

    // GET: CRITICREVIEWS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var criticreview = await _context.CriticReview
            .FirstOrDefaultAsync(m => m.Id == id);
        if (criticreview == null)
        {
            return NotFound();
        }

        return View(criticreview);
    }

    // GET: CRITICREVIEWS/Create
    public IActionResult Create()
    {
        ViewData["MovieId"] =
            new SelectList(_context.Movie, "Id", "Title");
        ViewData["Ratings"] =
            new SelectList(new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
        return View();
    }

    // POST: CRITICREVIEWS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,MovieId,Description,Rating,IsPublished")] CriticReview criticreview)
    {
        string currentUser =
            User.Identity?.Name ?? "";
        bool reviewExists =
            await _context.CriticReview.AnyAsync(r =>
                r.MovieId == criticreview.MovieId &&
                r.CreatedBy == currentUser);

        if (reviewExists)
        {
            ModelState.AddModelError(
                "",
                "You already reviewed this movie.");
        }

        if (ModelState.IsValid)
        {
            criticreview.CreatedBy =
                User.Identity?.Name ?? "Unknown";
            criticreview.CreatedDate =
                DateTime.Now;

            _context.Add(criticreview);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewData["MovieId"] =
            new SelectList(_context.Movie, "Id", "Title", criticreview.MovieId);
        ViewData["Ratings"] =
            new SelectList(new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
        return View(criticreview);
    }

    // GET: CRITICREVIEWS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var criticreview = await _context.CriticReview.FindAsync(id);
        if (criticreview == null)
        {
            return NotFound();
        }

        if (criticreview.IsPublished &&
           !User.IsInRole("Administrator"))
        {
            return RedirectToAction(nameof(Index));
        }

        ViewData["MovieId"] =
            new SelectList(_context.Movie, "Id", "Title", criticreview.MovieId);
        ViewData["Ratings"] =
            new SelectList(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
        return View(criticreview);
    }

    // POST: CRITICREVIEWS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,MovieId,Description,Rating,IsPublished")] CriticReview criticreview)
    {
        if (id != criticreview.Id)
        {
            return NotFound();
        }
        var existingReview =
            await _context.CriticReview
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == id);

        if (existingReview?.IsPublished == true &&
            !User.IsInRole("Administrator"))
        {
            return RedirectToAction(nameof(Index));
        }

        if (ModelState.IsValid)
        {
            try
            {
                criticreview.CreatedBy =
                    existingReview.CreatedBy;

                criticreview.CreatedDate =
                    existingReview.CreatedDate;

                _context.Update(criticreview);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CriticReviewExists(criticreview.Id))
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

        ViewData["MovieId"] =
            new SelectList(_context.Movie, "Id", "Title", criticreview.MovieId);
        ViewData["Ratings"] =
            new SelectList(new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
        return View(criticreview);
    }

    // GET: CRITICREVIEWS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var criticreview = await _context.CriticReview
            .FirstOrDefaultAsync(m => m.Id == id);
        if (criticreview == null)
        {
            return NotFound();
        }

        if (criticreview.IsPublished &&
            !User.IsInRole("Administrator"))
        {
            return RedirectToAction(nameof(Index));
        }

        return View(criticreview);
    }

    // POST: CRITICREVIEWS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var criticreview = await _context.CriticReview.FindAsync(id);
        if (criticreview == null)
        {
            return RedirectToAction(nameof(Index));
        }

        if (criticreview.IsPublished &&
            !User.IsInRole("Administrator"))
        {
            return RedirectToAction(nameof(Index));
        }

        _context.CriticReview.Remove(criticreview);

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
    private bool CriticReviewExists(int? id)
    {
        return _context.CriticReview.Any(e => e.Id == id);
    }
}
