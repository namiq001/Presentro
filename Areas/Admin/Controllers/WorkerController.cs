using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PresentroMVC.Models;
using PresentroMVC.PresentroDataContext;
using PresentroMVC.ViewModels.WorkerVM;

namespace PresentroMVC.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize]

public class WorkerController : Controller
{
    private readonly PresentroDbContext _context;
    private readonly IWebHostEnvironment _environment;

    public WorkerController(PresentroDbContext context, IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
    }

    public async Task<IActionResult> Index()
    {
        List<Worker> workers = await _context.Workers.Include(x => x.Works).ToListAsync();
        return View(workers);
    }
    public async Task<IActionResult> Create()
    {
        CreateWorkerVM createWorker = new CreateWorkerVM()
        {
            Works = await _context.Works.ToListAsync(),
        };
        return View(createWorker);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateWorkerVM createWorker)
    {
        createWorker.Works = await _context.Works.ToListAsync();
        if (!ModelState.IsValid) { return View(createWorker); }
        string newFileName = Guid.NewGuid().ToString() + createWorker.Image.FileName;
        string path = Path.Combine(_environment.WebRootPath, "assets", "img", "testimonials", newFileName);
        using (FileStream fileStream = new FileStream(path, FileMode.CreateNew))
        {
            await createWorker.Image.CopyToAsync(fileStream);
        }
        Worker worker = new Worker()
        {
            Name = createWorker.Name,
            Description = createWorker.Description,
            WorkId = createWorker.WorkId,
        };
        worker.ImageName = newFileName;
        _context.Workers.Add(worker);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        Worker? worker = await _context.Workers.FindAsync(id);
        if (worker is null)
        {
            return NotFound();
        }
        string path = Path.Combine(_environment.WebRootPath, "assets", "img", "testimonials", worker.ImageName);
        if (System.IO.File.Exists(path))
        {
            System.IO.File.Delete(path);
        }
        _context.Workers.Remove(worker);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Edit(int Id)
    {
        Worker? worker = await _context.Workers.FindAsync(Id);
        if (worker is null) { return NotFound(); }
        EditWorkerVM editWorker = new EditWorkerVM()
        {
            Name=worker.Name,
            ImageName = worker.ImageName,
            Works = await _context.Works.ToListAsync(),
            Description = worker.Description,
            WorkId = worker.WorkId,
        };
        return View(editWorker);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int Id, EditWorkerVM newWorker)
    {
        Worker? worker = await _context.Workers.FindAsync(Id);
        if (worker is null) { return NotFound(); }
        if (!ModelState.IsValid)
        {
            newWorker.Works = await _context.Works.ToListAsync();
            return View(newWorker);
        }
        if (newWorker.Image is not null)
        {
            string path = Path.Combine(_environment.WebRootPath, "assets", "img", "testimonials", worker.ImageName);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }

            string newFileName = Guid.NewGuid().ToString() + newWorker.Image.FileName;
            string newpath = Path.Combine(_environment.WebRootPath, "assets", "img", "testimonials", newFileName);
            using (FileStream fileStream = new FileStream(newpath, FileMode.CreateNew))
            {
                await newWorker.Image.CopyToAsync(fileStream);
            }
            worker.ImageName = newFileName;
        }
        worker.Name = newWorker.Name;
        worker.Description = newWorker.Description;
        worker.WorkId = newWorker.WorkId;

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
