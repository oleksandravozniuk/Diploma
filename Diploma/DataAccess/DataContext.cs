using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DatabaseAccess
{
    public class DataContext:DbContext
    {
        public DbSet<IndividualProblem> IndividualProblems { get; set; }
        public DbSet<Research> Researches { get; set; }
        public DbSet<File> Files { get; set; }


        public DataContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=Diploma.db");
        }
    }
}
