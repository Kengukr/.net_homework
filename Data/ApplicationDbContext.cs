using Microsoft.EntityFrameworkCore;
using task_new.Models;

namespace task_new.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Author>().HasData(
                new Author { Id = 1, Name = "Тарас Шевченко", BirthYear = 1814 },
                new Author { Id = 2, Name = "Іван Франко", BirthYear = 1856 },
                new Author { Id = 3, Name = "Леся Українка", BirthYear = 1871 }
            );

            modelBuilder.Entity<Book>().HasData(
                new Book { Id = 1, Title = "Кобзар", Year = 1840, AuthorId = 1 },
                new Book { Id = 2, Title = "Гайдамаки", Year = 1841, AuthorId = 1 },
                new Book { Id = 3, Title = "Захар Беркут", Year = 1883, AuthorId = 2 },
                new Book { Id = 4, Title = "Украдене щастя", Year = 1893, AuthorId = 2 },
                new Book { Id = 5, Title = "Лісова пісня", Year = 1911, AuthorId = 3 }

            );
        }
    }
}