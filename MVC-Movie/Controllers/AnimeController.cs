using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MVC_Movie.Models;

namespace MVC_Movie.Controllers
{
    public class AnimeController : Controller
    {
        private readonly MvcMovieContext _context;
        public AnimeController(MvcMovieContext context)
        {
            _context = context;
        }
        // GET: AnimeController
        public async Task<IActionResult> Index(string? searchString)
        {
            if (_context.Animes is null)
            {
                return Problem("Entity set 'MvcMovieContext.Anime' is null.");
            }

            var animeList = from a in _context.Animes
                            select a;

            if (!string.IsNullOrEmpty(searchString))
            {
                animeList = animeList.Where(a => a.Title!.ToUpper().Contains(searchString.ToUpper()));
            }
            return View(await animeList.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id, Title, ImageURL, Metadata, Rating, Description")] Anime anime)
        {
            if (ModelState.IsValid)
            {
                _context.Animes.Add(anime);
                TempData["success"] = "Anime Created Sucessfully!";
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(anime);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var anime = await _context.Animes.FindAsync(id);
            if (anime == null)
            {
                return NotFound();
            }

            return View(anime);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Title, ImageURL, Metadata, Rating, Description")] Anime anime)
        {
            if (id != anime.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Animes.Update(anime);
                    TempData["success"] = "Anime Updated Successfully!";
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnimeExists(anime.Id))
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

            return View(anime);

        }

        // Http GET method is being called here
        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null || id == 0)
            {
                return BadRequest();
            }

            var anime = await _context.Animes.FirstOrDefaultAsync(anime => anime.Id == id);

            if(anime == null)
            {
                return NotFound();
            }

            return View(anime);
        }

        // DELETE POST for deleting the anime
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePOST(int? id)
        {
            if(id == null || id == 0)
            {
                return BadRequest();
            }
            var anime = await _context.Animes.FindAsync(id);
            if(anime == null)
            {
                return NotFound();
            }
            _context.Animes.Remove(anime);
            TempData["success"] = "Anime Deleted Successfully!";
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool AnimeExists(int animeId)
        {
            return _context.Animes.Any(a => a.Id == animeId);
        }

    }
}
