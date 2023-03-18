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
using projectportfolio.ViewModels;

namespace projectportfolio.Controllers
{
    [Authorize]
    public class ProjectController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProjectController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Project
        [Route("/admin/projekt")]
        public async Task<IActionResult> Index()
        {
            // Get all projects and related information from database and save to a variable as a viewmodel
            var viewModel = new ProjectViewModel();
            viewModel.Projects = _context.Projects
                .Include(p => p.Category)
                .Include(p => p.DetailImg)
                .Include(p => p.LogoImg)
                .Include(p => p.MockupImg)
                .Include(p => p.Competences); ;

            // Return all projects to view
            return View(viewModel);
        }

        // GET: Project/Details/5
        [Route("/admin/projekt/detaljer/{id?}")]
        public async Task<IActionResult> Details(int? id)
        {
            // If statement that return response status Not Found if no id has been given or no project is stored in database
            if (id == null || _context.Projects == null)
            {
                return NotFound();
            }

            // Get project and related information from database and save to variable
            var project = await _context.Projects
                .Include(p => p.Category)
                .Include(p => p.DetailImg)
                .Include(p => p.LogoImg)
                .Include(p => p.MockupImg)
                .Include(p => p.Competences)
                .FirstOrDefaultAsync(m => m.ProjectId == id);

            // If statement that return response status Not Found if no project with given id is stored in database
            if (project == null)
            {
                return NotFound();
            }

            // Return project to view
            return View(project);
        }

        // GET: Project/Create
        [Route("/admin/projekt/lagg-till")]
        public IActionResult Create()
        {
            // Make a new instance of project
            var project = new Project();
            // Add an empty list of competences to project
            project.Competences = new List<Competence>();

            // Save categories with a certain area of use to select list
            ViewData["CategoryId"] = new SelectList(_context.Categories.Where(category => category.AreaOfUse == "Projekt"), "CategoryId", "Name");

            // Save images with a certain category to select list
            ViewData["DetailId"] = new SelectList(_context.Images.Where(image => image.Category.Name == "Detalj"), "ImageId", "AltText");

            // Save images with a certain category to select list
            ViewData["LogotypeId"] = new SelectList(_context.Images.Where(image => image.Category.Name == "Logotyp"), "ImageId", "AltText");

            // Save images with a certain category to select list
            ViewData["MockupId"] = new SelectList(_context.Images.Where(image => image.Category.Name == "Mockup"), "ImageId", "AltText");
            
            // Method to add comptence data to project
            ShowCompInProject(project);
            
            return View();
        }

        // POST: Project/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/admin/projekt/lagg-till")]
        public async Task<IActionResult> Create([Bind("ProjectId,Title,Published,Link,InitialDescription,TechnicalDescription,OptionalDescription,MockupId,LogotypeId,DetailId,CategoryId")] Project project, string[] selectedCompetences)
        {
            // If statement that checks there are any data in array for selected competences
            if (selectedCompetences != null)
            {
                // Add an empty list of competences to project
                project.Competences = new List<Competence>();

                // Foreach that loops through array of selected competences
                foreach (var competence in selectedCompetences)
                {
                    // Save competence id to a variable
                    var competenceToAdd = _context.Competences.Find(int.Parse(competence));
                    // Add competence to competance property for project instance
                    project.Competences.Add(competenceToAdd);
                }
            }

            if (ModelState.IsValid)
            {
                // If statement that add value to property Published if input field is left empty
                if (project.Published == null)
                {
                    project.Published = "Ej publicerad";
                }

                // If statement that add value to property Published if input field is left empty
                if (project.Link == null)
                {
                    project.Link = "Länk saknas";
                }

                // Add data for project to database
                _context.Add(project);
                await _context.SaveChangesAsync();

                // Redirection to Index view
                return RedirectToAction(nameof(Index));
            }

            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name", project.CategoryId);
            ViewData["DetailId"] = new SelectList(_context.Images, "ImageId", "AltText", project.DetailId);
            ViewData["LogotypeId"] = new SelectList(_context.Images, "ImageId", "AltText", project.LogotypeId);
            ViewData["MockupId"] = new SelectList(_context.Images, "ImageId", "AltText", project.MockupId);
            
            // Return data about project to view
            return View(project);
        }

        // GET: Project/Edit/5
        [Route("/admin/projekt/andra/{id?}")]
        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null || _context.Projects == null)
            {
                return NotFound();
            }

            // Get data about project and related competences from database and save as a project object
            Project project = _context.Projects
             .Include(p => p.Competences)
             .Where(p => p.ProjectId == id)
             .Single();

            if (project == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories.Where(category => category.AreaOfUse == "Projekt"), "CategoryId", "Name", project.CategoryId);
            ViewData["DetailId"] = new SelectList(_context.Images.Where(image => image.Category.Name == "Detalj"), "ImageId", "AltText", project.DetailId);
            ViewData["LogotypeId"] = new SelectList(_context.Images.Where(image => image.Category.Name == "Logotyp"), "ImageId", "AltText", project.LogotypeId);
            ViewData["MockupId"] = new SelectList(_context.Images.Where(image => image.Category.Name == "Mockup"), "ImageId", "AltText", project.MockupId);

            // Method to add competence data to project
            ShowCompInProject(project);

            // Return project to view
            return View(project);
        }

        // POST: Project/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/admin/projekt/andra/{id?}")]
        public async Task<IActionResult> Edit(int id, [Bind("ProjectId,Title,Published,Link,InitialDescription,TechnicalDescription,OptionalDescription,MockupId,LogotypeId,DetailId,CategoryId")] Project project, string[] selectedCompetences)
        {
            if (id != project.ProjectId)
            {
                return NotFound();
            }

            // Get data about project and related competences from database and save to a variable
            var projectToUpdate = _context.Projects
                 .Include(p => p.Competences)
                 .Where(p => p.ProjectId == id)
                 .Single();

            // If statement that return Not found if no project was found with given id
            if (projectToUpdate == null)
            {
                return NotFound();
            }

            // Save new data from form to variable that is storing data from database
            projectToUpdate.Title = project.Title;
            projectToUpdate.Published = project.Published;
            projectToUpdate.Link = project.Link;
            projectToUpdate.InitialDescription = project.InitialDescription;
            projectToUpdate.TechnicalDescription = project.TechnicalDescription;
            projectToUpdate.OptionalDescription = project.OptionalDescription;
            projectToUpdate.MockupId = project.MockupId;
            projectToUpdate.LogotypeId = project.LogotypeId;
            projectToUpdate.DetailId = project.DetailId;
            projectToUpdate.CategoryId = project.CategoryId;
            
            if (projectToUpdate.Competences == null)
            {
                return Problem("Entity set 'DataContext.Competences'  is null.");
            }

            // Foreach loop that removes stored competences for project
            foreach (var competence in projectToUpdate.Competences.ToList())
            {
                projectToUpdate.Competences.Remove(competence);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // If statement that checks that there are data in array for selected comptences
                    if (selectedCompetences != null)
                    {
                        // Foreach loop that loops through array of selected competences
                        foreach (var competence in selectedCompetences)
                        {
                            // Save competence id to variable
                            var competenceToAdd = _context.Competences.Find(int.Parse(competence));

                            if (competenceToAdd == null)
                            {
                                return Problem("Entity set 'DataContext.Competences'  is null.");
                            }

                            // Add competence to list of competences in project
                            projectToUpdate.Competences.Add(competenceToAdd);
                        }
                    }

                    // Update project in database
                    _context.Update(projectToUpdate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(project.ProjectId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                // Redirection to Index view
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "AreaOfUse", project.CategoryId);
            ViewData["DetailId"] = new SelectList(_context.Images, "ImageId", "AltText", project.DetailId);
            ViewData["LogotypeId"] = new SelectList(_context.Images, "ImageId", "AltText", project.LogotypeId);
            ViewData["MockupId"] = new SelectList(_context.Images, "ImageId", "AltText", project.MockupId);

            // Method to add competence data to project
            ShowCompInProject(projectToUpdate);

            // Return data about project to view
            return View(projectToUpdate);
        }

        // GET: Project/Delete/5
        [Route("/admin/projekt/radera/{id?}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Projects == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .Include(p => p.Category)
                .Include(p => p.DetailImg)
                .Include(p => p.LogoImg)
                .Include(p => p.MockupImg)
                .Include(p => p.Competences)
                .FirstOrDefaultAsync(m => m.ProjectId == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // POST: Project/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("/admin/projekt/radera/{id?}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Projects == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Projects'  is null.");
            }
            var project = await _context.Projects.FindAsync(id);
            if (project != null)
            {
                _context.Projects.Remove(project);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectExists(int id)
        {
          return (_context.Projects?.Any(e => e.ProjectId == id)).GetValueOrDefault();
        }

        private void ShowCompInProject(Project project)
        {
            // Get data about all competences from database and save to variable
            var allCompetences = _context.Competences;

            // Save id for all competences related to a project to a variable
            var projectCompetences = new HashSet<int>(project.Competences.Select(c => c.CompetenceId));
            
            // Make a new list based viewmodel CompInProject 
            var viewModel = new List<CompInProject>();

            // Foreach that loops through all competences
            foreach (var competence in allCompetences)
            {
                // Data about competence is added to viewmodel
                viewModel.Add(new CompInProject
                {
                    CompetenceId = competence.CompetenceId,
                    Name = competence.Name,
                    // Property is set to true if competence id is found in variable for competences in project
                    IsChecked = projectCompetences.Contains(competence.CompetenceId)
                });
            }

            // Save variable to a ViewBag
            ViewBag.Competences = viewModel;
        }
    }
}
