﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using projectportfolio.Data;
using projectportfolio.Models;

namespace projectportfolio.Controllers
{
    [Authorize]
    public class ExperienceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExperienceController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Experience
        [Route("/admin/utbildning-och-jobb")]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Experiences.Include(e => e.Category);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Experience/Details/5
        [Route("/admin/utbildning-och-jobb/detaljer/{id?}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Experiences == null)
            {
                return NotFound();
            }

            var experience = await _context.Experiences
                .Include(e => e.Category)
                .FirstOrDefaultAsync(m => m.ExperienceId == id);
            if (experience == null)
            {
                return NotFound();
            }

            return View(experience);
        }

        // GET: Experience/Create
        [Route("/admin/utbildning-och-jobb/lagg-till")]
        public IActionResult Create()
        {
            // Save categories with a certain area of use to select list
            ViewData["CategoryId"] = new SelectList(_context.Categories.Where(category => category.AreaOfUse == "Erfarenheter"), "CategoryId", "Name");
            return View();
        }

        // POST: Experience/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/admin/utbildning-och-jobb/lagg-till")]
        public async Task<IActionResult> Create([Bind("ExperienceId,What,Where,Start,End,Description,CategoryId")] Experience experience)
        {
            if (ModelState.IsValid)
            {
                if (experience.End == null)
                {
                    experience.End = "Pågående";
                }

                _context.Add(experience);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name", experience.CategoryId);
            return View(experience);
        }

        // GET: Experience/Edit/5
        [Route("/admin/utbildning-och-jobb/andra/{id?}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Experiences == null)
            {
                return NotFound();
            }

            var experience = await _context.Experiences.FindAsync(id);
            if (experience == null)
            {
                return NotFound();
            }

            // Save categories with a certain area of use to select list
            ViewData["CategoryId"] = new SelectList(_context.Categories.Where(category => category.AreaOfUse == "Erfarenheter"), "CategoryId", "Name", experience.CategoryId);
            return View(experience);
        }

        // POST: Experience/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/admin/utbildning-och-jobb/andra/{id?}")]
        public async Task<IActionResult> Edit(int id, [Bind("ExperienceId,What,Where,Start,End,Description,CategoryId")] Experience experience)
        {
            if (id != experience.ExperienceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(experience);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExperienceExists(experience.ExperienceId))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name", experience.CategoryId);
            return View(experience);
        }

        // GET: Experience/Delete/5
        [Route("/admin/utbildning-och-jobb/radera/{id?}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Experiences == null)
            {
                return NotFound();
            }

            var experience = await _context.Experiences
                .Include(e => e.Category)
                .FirstOrDefaultAsync(m => m.ExperienceId == id);
            if (experience == null)
            {
                return NotFound();
            }

            return View(experience);
        }

        // POST: Experience/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("/admin/utbildning-och-jobb/radera/{id?}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Experiences == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Experiences'  is null.");
            }
            var experience = await _context.Experiences.FindAsync(id);
            if (experience != null)
            {
                _context.Experiences.Remove(experience);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExperienceExists(int id)
        {
          return (_context.Experiences?.Any(e => e.ExperienceId == id)).GetValueOrDefault();
        }
    }
}
