using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using projectportfolio.Data;
using projectportfolio.Models;

namespace projectportfolio.Controllers
{
    [Authorize]
    public class ImageController : Controller
    {
        private readonly ApplicationDbContext _context;
        // Property for data about host environment
        private readonly IWebHostEnvironment _hostEnvironment;
        // Property for path to root directory
        private string? rootpath;

        // Interface with data about host environment added as argument in constructor
        public ImageController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            // Save data about host environment to variable.
            _hostEnvironment = hostEnvironment;
            // Save information about root path to variable.
            rootpath = _hostEnvironment.WebRootPath;
        }

        // GET: Image
        [Route("/admin/bilder")]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Images.Include(i => i.Category);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Image/Details/5
        [Route("/admin/bilder/detaljer/{id?}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Images == null)
            {
                return NotFound();
            }

            var image = await _context.Images
                .Include(i => i.Category)
                .FirstOrDefaultAsync(m => m.ImageId == id);
            if (image == null)
            {
                return NotFound();
            }

            return View(image);
        }

        // GET: Image/Create
        [Route("/admin/bilder/lagg-till")]
        public IActionResult Create()
        {
            // Save categories with a certain area of use to select list
            ViewData["CategoryId"] = new SelectList(_context.Categories.Where(category => category.AreaOfUse == "Bilder"), "CategoryId", "Name");
            return View();
        }

        // POST: Image/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/admin/bilder/lagg-till")]
        public async Task<IActionResult> Create([Bind("ImageId,File,AltText,CategoryId")] Image image)
        {
            if (ModelState.IsValid)
            {
                // If statement that check that File property isn't null
                if (image.File != null) {
                    // Save name of image file to variable
                    string name = Path.GetFileName(image.File.FileName);
                    // Save a path to image file to variable
                    string path = Path.Combine(rootpath + "/uploads/", name);

                    // Set name of file as value to property name in model
                    image.Name = name;

                    // Upload of image file
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await image.File.CopyToAsync(fileStream);
                    }
                } 
                else
                {
                    // Set value of property name as null
                    image.Name = null;
                }

                // Add image information to database
                _context.Add(image);
                await _context.SaveChangesAsync();

                // Redirection to Index view
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "AreaOfUse", image.CategoryId);
            
            // Return informatio about image to view
            return View(image);
        }

        // GET: Image/Edit/5
        [Route("/admin/bilder/andra/{id?}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Images == null)
            {
                return NotFound();
            }

            var image = await _context.Images.FindAsync(id);
            if (image == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories.Where(category => category.AreaOfUse == "Bilder"), "CategoryId", "Name", image.CategoryId);
            return View(image);
        }

        // POST: Image/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/admin/bilder/andra/{id?}")]
        public async Task<IActionResult> Edit(int id, [Bind("ImageId,Name,AltText,CategoryId")] Image image)
        {
            if (id != image.ImageId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(image);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ImageExists(image.ImageId))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "AreaOfUse", image.CategoryId);
            return View(image);
        }

        // GET: Image/Delete/5
        [Route("/admin/bilder/radera/{id?}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Images == null)
            {
                return NotFound();
            }

            var image = await _context.Images
                .Include(i => i.Category)
                .FirstOrDefaultAsync(m => m.ImageId == id);
            if (image == null)
            {
                return NotFound();
            }

            return View(image);
        }

        // POST: Image/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("/admin/bilder/radera/{id?}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Images == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Images'  is null.");
            }
            var image = await _context.Images.FindAsync(id);
            if (image != null)
            {
                _context.Images.Remove(image);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ImageExists(int id)
        {
          return (_context.Images?.Any(e => e.ImageId == id)).GetValueOrDefault();
        }
    }
}
