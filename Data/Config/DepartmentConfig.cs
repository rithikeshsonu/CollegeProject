using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CollegeProject.Data.Config
{
    public class DepartmentConfig : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.ToTable("Departments");
            builder.HasKey(x => x.DeptID);
            builder.Property(x => x.DeptID).UseIdentityColumn();
            builder.Property(x => x.DeptName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Description).IsRequired(false).HasMaxLength(400);
            builder.HasData(new List<Department>()
            {
                new Department
                {
                    DeptID = 1,
                    DeptName = "CSE",
                    Description = "Computer Science..."
                },
                new Department
                {
                    DeptID = 2,
                    DeptName = "ECE",
                    Description = "Electronic Engineering..."
                }
            });
        }
    }
}
