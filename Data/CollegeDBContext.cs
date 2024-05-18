using Microsoft.EntityFrameworkCore;

namespace CollegeProject.Data
{
    public class CollegeDBContext : DbContext
    {
        DbSet<Student> Students { get; set; }
    }
}
