using Microsoft.EntityFrameworkCore;
using LexiconUniversity.Core.Entities;

namespace LexiconUniversity.Persistence.Data;

public class LexiconUniversityContext(DbContextOptions<LexiconUniversityContext> options)
    : DbContext(options)
{
    public DbSet<Student> Student { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Set the context up to understand a many-to-many table for enrollments
        modelBuilder.Entity<Student>()
            .HasMany(student => student.Courses)
            .WithMany(course => course.Students)
            .UsingEntity<Enrollment>(
                e => e.HasOne(e => e.Course).WithMany(c => c.Enrollments),
                e => e.HasOne(e => e.Student).WithMany(s => s.Enrollments),
                e => e.HasKey(e => new { e.CourseId, e.StudentId }));
    }
}
