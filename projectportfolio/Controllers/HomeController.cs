using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projectportfolio.Data;
using projectportfolio.Models;
using projectportfolio.ViewModels;
using System.Diagnostics;

namespace projectportfolio.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        // Database connection property
        private readonly ApplicationDbContext? _context;

        // Database connection added as argument in constructor
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;

            // Save database connection to property.
            _context = context;
        }
        
        public IActionResult Index()
        {
            return View();
        }

        [Route("/om-sofia")]
        public IActionResult About()
        {
            return View();
        }

        [Route("/alla-projekt")]
        public async Task<IActionResult> Projects()
        {
            // Get all projects and related information from database and save to a variable as a viewmodel
            var viewModel = new ProjectViewModel();
            viewModel.Projects = _context.Projects
                .Include(p => p.Category)
                .Include(p => p.DetailImg)
                .Include(p => p.LogoImg)
                .Include(p => p.MockupImg)
                .Include(p => p.Competences);

            // Return all projects to view
            return View(viewModel);
        }

        [Route("/projekt/{id?}")]
        public async Task<IActionResult> Project(int? id)
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

        [Route("/cv")]
        public IActionResult CV()
        {
            // Get all experiences and competences from database and save to ViewBags.
            ViewBag.Experiences = _context.Experiences.Include(e => e.Category).ToList();
            ViewBag.Competences = _context.Competences.Include(c => c.Category).ToList();
            return View();
        }

        [Route("/kontakt")]
        public IActionResult Contact()
        {
            return View();
        }

        [Authorize]
        [Route("/administration")]
        public IActionResult Admin()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}