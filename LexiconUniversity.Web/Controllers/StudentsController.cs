﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LexiconUniversity.Core.Entities;
using LexiconUniversity.Persistence.Data;
using LexiconUniversity.Web.Models;
using System.Diagnostics;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Bogus;

namespace LexiconUniversity.Web.Controllers;

public class StudentsController(LexiconUniversityContext context, IMapper mapper) : Controller
{
    private readonly LexiconUniversityContext _context = context;
    private readonly IMapper _mapper = mapper;
    private readonly Faker _faker = new();
    private readonly MapperConfiguration _config = new(
        cfg => cfg.CreateProjection<Student, StudentSummaryViewModel>()
            .ForMember(ssvm => ssvm.FullName, conf => conf.MapFrom(s => $"{s.FirstName} {s.LastName}")));

    // GET: Students
    public async Task<IActionResult> Index(int? pageNumber)
    {
        //var query = _context.Student
        //    .Select(s => new StudentSummaryViewModel(s.Id, s.Avatar, s.FirstName, s.LastName, s.Email));
        //var query = _mapper.ProjectTo<StudentSummaryViewModel>(
        //    _context.Student.OrderBy(s => s.Id));
        var query = _context.Student.OrderBy(s => s.Id).ProjectTo<StudentSummaryViewModel>(_config);

        int pageSize = 10;
        var page = PaginatedList<StudentSummaryViewModel>.CreateAsync(query, pageNumber ?? 1, pageSize);
        return View(await page);
    }

    // GET: Students/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var student = await _context.Student.AsNoTracking()
            .Include(s => s.Enrollments)
            .ThenInclude(e => e.Course)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (student == null)
        {
            return NotFound();
        }

        return View(student);
    }

    // GET: Students/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Students/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(StudentCreateViewModel studentModel)
    {
        if (ModelState.IsValid)
        {
            Random rand = new();
            var student = _mapper.Map<Student>(studentModel);
            student.Avatar = _faker.Internet.Avatar();

            foreach (var courseId in studentModel.SelectedCourses)
            {
                student.Enrollments.Add(new Enrollment
                {
                    CourseId = courseId,
                    Grade = rand.Next(1, 6)
                });
            }

            _context.Add(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(studentModel);
    }

    // GET: Students/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var student = await _context.Student.FindAsync(id);
        if (student == null)
        {
            return NotFound();
        }
        return View(student);
    }

    // POST: Students/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Avatar,FirstName,LastName,Email")] Student student)
    {
        if (id != student.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(student);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(student.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(student);
    }

    // GET: Students/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var student = await _context.Student
            .FirstOrDefaultAsync(m => m.Id == id);
        if (student == null)
        {
            return NotFound();
        }

        return View(student);
    }

    // POST: Students/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var student = await _context.Student.FindAsync(id);
        if (student != null)
        {
            _context.Student.Remove(student);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    private bool StudentExists(int id)
    {
        return _context.Student.Any(e => e.Id == id);
    }
}
