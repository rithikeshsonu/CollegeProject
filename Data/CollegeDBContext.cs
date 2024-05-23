using CollegeProject.Data.Config;
using CollegeProject.Models;
using Microsoft.EntityFrameworkCore;

namespace CollegeProject.Data
{
    public class CollegeDBContext : DbContext
    {
        public CollegeDBContext(DbContextOptions<CollegeDBContext> options) :  base(options)
        {

        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Department> Departments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //table 1
            modelBuilder.ApplyConfiguration(new StudentConfig());
            modelBuilder.ApplyConfiguration(new DepartmentConfig());

            /*
            //modelBuilder.Entity<Student>().HasData(new List<Student>() 
            //{
            //    new Student
            //    {
            //        StudentID = 1,
            //        StudentName = "Sample1",
            //        Address = "India",
            //        Email = "india@gmail.com",
            //        DOB = new DateTime(2008, 5, 12)
            //    },
            //    new Student
            //    {
            //        StudentID = 2,
            //        StudentName = "Sample2",
            //        Address = "USA",
            //        Email = "usa@gmail.com",
            //        DOB = new DateTime(2012, 3, 8)
            //    }
            //});

            //modelBuilder.Entity<Student>(entity =>
            //{
                //entity.Property(n => n.StudentID).IsRequired();
                //entity.Property(n => n.StudentName).IsRequired().HasMaxLength(50);
                //entity.Property(n => n.Address).IsRequired(false).HasMaxLength(400);
                //entity.Property(n => n.Email).IsRequired().HasMaxLength(200);
                //entity.Property(n => n.Email).IsRequired(false);
            //});
            */
        }
    }
}
