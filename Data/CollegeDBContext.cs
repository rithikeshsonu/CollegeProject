using Microsoft.EntityFrameworkCore;

namespace CollegeProject.Data
{
    public class CollegeDBContext : DbContext
    {
        public CollegeDBContext(DbContextOptions<CollegeDBContext> options) :  base(options)
        {

        }
        DbSet<Student> Students { get; set; }
    }
}
