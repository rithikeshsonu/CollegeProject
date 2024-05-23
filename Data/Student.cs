using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CollegeProject.Data
{
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StudentID { get; set; }
        public string StudentName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime DOB { get; set; }
        //Common column to hold reference to Department table.
        //A student can either have a department or not have a department.
        public int? DepartmentID { get; set; }
        //One student can only belong to one dept. Dept could be null as well...
        public virtual Department? Department { get; set; }
    }
}
