using Microsoft.EntityFrameworkCore;
using Callapp.Models;

namespace Callapp.Context;

public class Callappdb : DbContext
{
    private readonly IConfiguration _configuration;

    public Callappdb(IConfiguration configuration)
    {
        _configuration = configuration;
        this.ChangeTracker.LazyLoadingEnabled = true;
    }

    public DbSet<User> Users { get; set; }
    public DbSet<UserProfile> UserProfiles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_configuration.GetConnectionString("ApiDatabase"));
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasOne(u => u.UserProfile)
            .WithOne(up => up.User)
            .HasForeignKey<UserProfile>(up => up.UserIdFk)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<UserProfile>()
            .HasOne(u => u.User)
            .WithOne(up => up.UserProfile)
            .HasForeignKey<User>(up => up.UserProfIdFk)
            .OnDelete(DeleteBehavior.Cascade);
        
    }
}