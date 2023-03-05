using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using projectportfolio.Data;
using projectportfolio.Models;

namespace projectportfolio.Controllers
{
    public class CompetenceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CompetenceController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Competence
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Competences.Include(c => c.Category);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Competence/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Competences == null)
            {
                return NotFound();
            }

            var competence = await _context.Competences
                .Include(c => c.Category)
                .FirstOrDefaultAsync(m => m.CompetenceId == id);
            if (competence == null)
            {
                return NotFound();
            }

            return View(competence);
        }

        // GET: Competence/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories.Where(category => category.AreaOfUse == "Kompetenser"), "CategoryId", "Name");
            return View();
        }

        // POST: Competence/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CompetenceId,Name,CategoryId")] Competence competence)
        {
            if (ModelState.IsValid)
            {
                _context.Add(competence);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name", competence.CategoryId);
            return View(competence);
        }

        // GET: Competence/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Competences == null)
            {
                return NotFound();
            }

            var competence = await _context.Competences.FindAsync(id);
            if (competence == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories.Where(category => category.AreaOfUse == "Kompetenser"), "CategoryId", "Name", competence.CategoryId);
            return View(competence);
        }

        // POST: Competence/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CompetenceId,Name,CategoryId")] Competence competence)
        {
            if (id != competence.CompetenceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(competence);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompetenceExists(competence.CompetenceId))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name", competence.CategoryId);
            return View(competence);
        }

        // GET: Competence/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Competences == null)
            {
                return NotFound();
            }

            var competence = await _context.Competences
                .Include(c => c.Category)
                .FirstOrDefaultAsync(m => m.CompetenceId == id);
            if (competence == null)
            {
                return NotFound();
            }

            return View(competence);
        }

        // POST: Competence/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Competences == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Competences'  is null.");
            }
            var competence = await _context.Competences.FindAsync(id);
            if (competence != null)
            {
                _context.Competences.Remove(competence);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompetenceExists(int id)
        {
          return (_context.Competences?.Any(e => e.CompetenceId == id)).GetValueOrDefault();
        }
    }
}
