using Journey.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Journey.Infrastructure;

public class JourneyDbContext : DbContext
{
    public DbSet<Trip> Trips { get; set; }

    public DbSet<Activity> Activities { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=C:\\Users\\wag_p\\OneDrive\\Desktop\\Wagner\\C#\\JourneyDatabase.db");
    }

   
}

