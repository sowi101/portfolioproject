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
        private readonly ApplicationDbContext? _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        
        public IActionResult Index()
        {
            return View();
        }

        [Route("/om")]
        public IActionResult About()
        {
            return View();
        }

        [Route("/alla-projekt")]
        public async Task<IActionResult> Projects()
        {
            var viewModel = new ProjectViewModel();
            viewModel.Projects = _context.Projects
                .Include(p => p.Category)
                .Include(p => p.DetailImg)
                .Include(p => p.LogoImg)
                .Include(p => p.MockupImg)
                .Include(p => p.Competences);

            return View(viewModel);
        }

        [Route("/projekt/{id?}")]
        public async Task<IActionResult> Project(int? id)
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

        [Route("/cv")]
        public IActionResult CV()
        {
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