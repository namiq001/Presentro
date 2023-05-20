using PresentroMVC.Models;

namespace PresentroMVC.ViewModels.WorkerVM;

public class EditWorkerVM
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int WorkId { get; set; }
    public string? ImageName { get; set; }
    public IFormFile? Image { get; set; }
    public List<Work>? Works { get; set; } 
}
