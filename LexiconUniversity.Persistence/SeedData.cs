using Bogus;
using LexiconUniversity.Core.Entities;
using LexiconUniversity.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace LexiconUniversity.Persistence;

public static class SeedData
{
    private static readonly Faker faker = new(locale: "sv");

    public static async Task InitAsync(LexiconUniversityContext context)
    {
        if (await context.Student.AnyAsync())
            return;

        var students = GenerateStudents(50);
        await context.AddRangeAsync(students);
        var courses = GenerateCourses(20);
        await context.AddRangeAsync(courses);
        var enrollments = GenerateEnrollments(courses, students);
        await context.AddRangeAsync(enrollments);

        await context.SaveChangesAsync();
    }

    private static Student[] GenerateStudents(int numberOfStudents)
    {
        Student[] students = new Student[numberOfStudents];
        for (int i = 0; i < numberOfStudents; i++)
        {
            var firstName = faker.Name.FirstName();
            var lastName = faker.Name.LastName();
            students[i] = new()
            {
                Avatar = faker.Internet.Avatar(),
                FirstName = firstName,
                LastName = lastName,
                Email = faker.Internet.Email(firstName, lastName, "lexicon.se"),
                Address = new Address
                {
                    Street = faker.Address.StreetAddress(),
                    City = faker.Address.City(),
                    ZipCode = faker.Address.ZipCode()
                }
            };
        }
        return students;
    }

    private static Course[] GenerateCourses(int numberOfCourses)
    {
        Course[] courses = new Course[numberOfCourses];
        for (int i = 0; i < numberOfCourses; i++)
        {
            courses[i] = new() { 
                Name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(faker.Company.Bs()) };
        }
        return courses;
    }

    private static List<Enrollment> GenerateEnrollments(IEnumerable<Course> courses, IEnumerable<Student> students)
    {
        var rand = new Random();
        List<Enrollment> enrollments = [];

        foreach (var course in courses)
        {
            foreach (var student in students)
            {
                if (rand.Next(0, 5) == 0)
                {
                    Enrollment enrollment = new()
                    {
                        Course = course,
                        Student = student,
                        Grade = rand.Next(1, 6)
                    };
                    enrollments.Add(enrollment);
                }
            }
        }
        return enrollments;
    }
}
