namespace PresentroMVC.Models;

public class Work
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public List<Worker> Workers { get; set;}
}
