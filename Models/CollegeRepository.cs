namespace CollegeProject.Models
{
    public static class CollegeRepository
    {
        public static List<Student> Students { get; set; } = new List<Student> {
                new Student
                {
                    StudentID = 1,
                    StudentName = "StudentRam",
                    Email = "Email@sample.com",
                    Address = "Address 1"
                },
                new Student
                {
                    StudentID = 2,
                    StudentName = "StudentKrish",
                    Email = "Email2@sample.com",
                    Address = "Address 2"
                }
            };
    }
}
