using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PresentroMVC.Models;
using System.Security.Cryptography.X509Certificates;

namespace PresentroMVC.PresentroDataContext;

public class PresentroDbContext : IdentityDbContext<AppUser> 
{
    public PresentroDbContext(DbContextOptions<PresentroDbContext> options) : base(options)
    {
        
    }
    public DbSet<Work> Works { get; set; }
    public DbSet<Worker> Workers { get; set; }

}
