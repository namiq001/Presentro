using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PresentroMVC.Models;
using PresentroMVC.PresentroDataContext;

namespace PresentroMVC.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize]

public class WorkController : Controller
{
    private readonly PresentroDbContext _context;

    public WorkController(PresentroDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        List<Work> works = await _context.Works.ToListAsync();
        return View(works);
    }
}
