using System.ComponentModel.DataAnnotations;

namespace PresentroMVC.ViewModels.AccountVM;

public class LoginVM
{
    [EmailAddress]
    public string Email { get; set; } = null!;
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;
}
