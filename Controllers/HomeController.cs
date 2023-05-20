using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PresentroMVC.Models;
using PresentroMVC.PresentroDataContext;
using PresentroMVC.ViewModels;

namespace PresentroMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly PresentroDbContext _context;

        public HomeController(PresentroDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<Worker> workers = await _context.Workers.Include(x=>x.Works).ToListAsync();
            HomeVM homeVM = new HomeVM()
            {
                Workers = workers
            };
            return View(homeVM);
        }

        
    }
}