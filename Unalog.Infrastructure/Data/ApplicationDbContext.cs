using Microsoft.EntityFrameworkCore;
using Unalog.Core.Entities;

namespace Unalog.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Motorista> Motoristas => Set<Motorista>();
}