using Microsoft.EntityFrameworkCore;

namespace CollegeProject.Data
{
    public class CollegeDBContext : DbContext
    {
        public CollegeDBContext(DbContextOptions<CollegeDBContext> options) :  base(options)
        {

        }
        DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasData(new List<Student>() 
            {
                new Student
                {
                    StudentID = 1,
                    StudentName = "Sample1",
                    Address = "India",
                    Email = "india@gmail.com",
                    DOB = new DateTime(2008, 5, 12)
                },
                new Student
                {
                    StudentID = 2,
                    StudentName = "Sample2",
                    Address = "USA",
                    Email = "usa@gmail.com",
                    DOB = new DateTime(2012, 3, 8)
                }
            });
        }
    }
}
