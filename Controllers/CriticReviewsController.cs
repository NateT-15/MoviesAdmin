using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MoviesAdmin.Models;
using MoviesAdmin.Data;

namespace MoviesAdmin.Controllers
{
    public class CriticReviewsController : Controller
    {
        private readonly MoviesAdminContext _context;

        public CriticReviewsController(MoviesAdminContext context)
        {
            _context = context;
        }

        // GET: CriticReviews
        public async Task<IActionResult> Index()
        {
            var criticReviews = await _context.CriticReviews.ToListAsync();
            return View(criticReviews);
        }

        // GET: CriticReviews/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CriticReviews == null)
            {
                return NotFound();
            }

            var criticReview = await _context.CriticReviews.FindAsync(id);
            if (criticReview == null)
            {
                return NotFound();
            }

            return View(criticReview);
        }

        // GET: CriticReviews/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CriticReviews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CriticId,MovieId,ReviewText,Rating")] CriticReview criticReview)
        {
            if (ModelState.IsValid)
            {
                _context.Add(criticReview);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(criticReview);
        }

        // GET: CriticReviews/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CriticReviews == null)
            {
                return NotFound();
            }

            var criticReview = await _context.CriticReviews.FindAsync(id);
            if (criticReview == null)
            {
                return NotFound();
            }
            return View(criticReview);
        }

        // POST: CriticReviews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CriticId,MovieId,ReviewText,Rating")] CriticReview criticReview)
        {
            if (id != criticReview.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(criticReview);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CriticReviewExists(criticReview.Id))
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
            return View(criticReview);
        }

        // GET: CriticReviews/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CriticReviews == null)
            {
                return NotFound();
            }

            var criticReview = await _context.CriticReviews.FindAsync(id);
            if (criticReview == null)
            {
                return NotFound();
            }

            return View(criticReview);
        }

        // POST: CriticReviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CriticReviews == null)
            {
                return Problem("Entity set 'MoviesAdminContext.CriticReviews'  is null.");
            }

            var criticReview = await _context.CriticReviews.FindAsync(id);
            if (criticReview != null)
            {
                _context.CriticReviews.Remove(criticReview);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CriticReviewExists(int id)
        {
            return _context.CriticReviews.Any(e => e.Id == id);
        }
    }
}
