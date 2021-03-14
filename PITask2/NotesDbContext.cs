using Microsoft.EntityFrameworkCore;

namespace PITask2
{
    public class NotesDbContext: DbContext
    {
        public DbSet<Note> Notes { get; set; }

        public NotesDbContext(DbContextOptions<NotesDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
