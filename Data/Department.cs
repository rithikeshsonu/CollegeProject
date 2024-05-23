namespace CollegeProject.Data
{
    public class Department
    {
        public int DeptID { get; set; }
        public string DeptName { get; set; }
        public string Description { get; set; }
        //One dept can have any number of students...
        public virtual ICollection<Student> Students { get; set; }
    }
}
