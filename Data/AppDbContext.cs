using Microsoft.EntityFrameworkCore;
using PresentadorDotNet.Models;

namespace PresentadorDotNet.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<PostContent> PostContents => Set<PostContent>();
}
