using AdHoc_SpeechSynthesizer.Domain;
using Microsoft.EntityFrameworkCore;

namespace AdHoc_SpeechSynthesizer.Data;

public class CompanyDbContext : DbContext
{
    public CompanyDbContext(DbContextOptions<CompanyDbContext> options)
        : base(options)
    {
    }

    public DbSet<Location> Locations { get; set; } = null!;
    public DbSet<Platform> Platforms { get; set; } = null!;
    public DbSet<Domain.Route> Routes { get; set; } = null!;
    public DbSet<TargetText> TargetTexts { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Location>().ToTable("Location");
        modelBuilder.Entity<Platform>().ToTable("Platform");
        modelBuilder.Entity<Domain.Route>().ToTable("Route");
        modelBuilder.Entity<TargetText>().ToTable("TargetText");

        modelBuilder.Entity<Location>()
            .HasKey(l => new { l.VersionNr, l.LocationTypeNr, l.LocationNr, l.ControlCenterId });

        modelBuilder.Entity<Platform>()
            .HasKey(p => new { p.VersionNr, p.LocationTypeNr, p.LocationNr, p.PlatformNr, p.ControlCenterId });

        modelBuilder.Entity<Domain.Route>()
            .HasKey(r => new { r.VersionNr, r.RouteNr, r.RouteVariant, r.ControlCenterId });

        modelBuilder.Entity<TargetText>()
            .HasKey(t => new { t.VersionNr, t.TargetTextNr, t.ControlCenterId });
    }
}
