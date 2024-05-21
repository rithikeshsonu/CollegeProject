using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CollegeProject.Data.Config
{
    public class StudentConfig :IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("Students");
            builder.HasKey(x => x.StudentID);
            builder.Property(x=>x.StudentID).UseIdentityColumn();
            builder.Property(x => x.StudentID).IsRequired();
            builder.Property(x => x.StudentName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Address).IsRequired(false).HasMaxLength(400);
            builder.Property(x => x.Email).IsRequired(false).HasMaxLength(200);
            builder.HasData(new List<Student>()
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
