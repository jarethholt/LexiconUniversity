using LexiconUniversity.Persistence.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LexiconUniversity.Web.Services;

public interface ICoursesService
{
    Task<IEnumerable<SelectListItem>> GetCoursesAsync();
}

public class CoursesService(LexiconUniversityContext context) : ICoursesService
{
    private readonly LexiconUniversityContext _context = context;

    public async Task<IEnumerable<SelectListItem>> GetCoursesAsync()
    {
        var query = _context.Course.Select(c => new SelectListItem
        {
            Text = c.Name,
            Value = c.Id.ToString()
        });
        return await query.ToListAsync();
    }
}
