using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<LoginModel> LoginModels { get; set; }
    public DbSet<RegisterModel> RegisterModels { get; set; }

    public DbSet<UserModel> UserModels { get; set; }
}