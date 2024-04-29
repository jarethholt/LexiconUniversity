using Microsoft.EntityFrameworkCore;
using LexiconUniversity.Core.Entities;
//using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Configuration.Json;

namespace LexiconUniversity.Persistence.Data;

public class LexiconUniversityContext(DbContextOptions<LexiconUniversityContext> options)
    : DbContext(options)
{
    public DbSet<Student> Student { get; set; } = default!;

    public DbSet<Course> Course { get; set; } = default!;

    public DbSet<Enrollment> Enrollment { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var navBuilder = modelBuilder.Entity<Student>().OwnsOne(student => student.Address);
        navBuilder.Property(address => address.Street).HasColumnName("StreetAddress");
        navBuilder.Property(address => address.City).HasColumnName("City");
        navBuilder.Property(address => address.ZipCode).HasColumnName("ZipCode");

        // Set the context up to understand a many-to-many table for enrollments
        modelBuilder.Entity<Student>()
            .HasMany(student => student.Courses)
            .WithMany(course => course.Students)
            .UsingEntity<Enrollment>(
                e => e.HasOne(e => e.Course).WithMany(c => c.Enrollments),
                e => e.HasOne(e => e.Student).WithMany(s => s.Enrollments),
                e => e.HasKey(e => new { e.CourseId, e.StudentId }));
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", false)
            .Build();
        string? connectionString = configuration.GetConnectionString("LexiconUniversityContext");
        optionsBuilder.UseSqlServer(connectionString)
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        base.OnConfiguring(optionsBuilder);
    }
}

// This factory could should only be enabled when scaffolding; it will mess up migrations
//public class LexiconUniversityContextFactory : IDesignTimeDbContextFactory<LexiconUniversityContext>
//{
//    public LexiconUniversityContext CreateDbContext(string[] args)
//    {
//        var optionsBuilder = new DbContextOptionsBuilder<LexiconUniversityContext>();
//        IConfigurationRoot configuration = new ConfigurationBuilder()
//            .AddJsonFile("appsettings.json", false)
//            .Build();
//        string? connectionString = configuration.GetConnectionString("LexiconUniversityContext");
//        optionsBuilder.UseSqlServer(connectionString);
//        return new LexiconUniversityContext(optionsBuilder.Options);
//    }
//}
