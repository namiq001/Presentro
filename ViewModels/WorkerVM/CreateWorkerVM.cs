using PresentroMVC.Models;
using System.ComponentModel.DataAnnotations;

namespace PresentroMVC.ViewModels.WorkerVM;

public class CreateWorkerVM
{
    [MaxLength(50)]
    public string Name { get; set; } = null!;
    [MaxLength(100)]
    public string? Description { get; set; }

    public int WorkId { get; set; }
    public IFormFile Image { get; set; } = null!;
    public List<Work>? Works { get; set; }
}
