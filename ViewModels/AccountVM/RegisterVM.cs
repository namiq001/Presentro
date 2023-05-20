﻿using System.ComponentModel.DataAnnotations;

namespace PresentroMVC.ViewModels.AccountVM;

public class RegisterVM
{
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string UserName { get; set; } = null!;
    [EmailAddress]
    public string Email { get; set; } = null!;
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;
    [DataType(DataType.Password),Compare(nameof(Password))]
    public string ConfrimPassword { get; set; } = null!;

}
