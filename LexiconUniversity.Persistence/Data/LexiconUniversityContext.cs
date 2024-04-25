using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LexiconUniversity.Core.Entities;

namespace LexiconUniversity.Persistence.Data
{
    public class LexiconUniversityContext : DbContext
    {
        public LexiconUniversityContext(DbContextOptions<LexiconUniversityContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Student { get; set; } = default!;
    }
}
