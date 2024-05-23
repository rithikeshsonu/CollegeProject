namespace CollegeProject.Data.Repository
{
    //As we have created the same functionality in common repository, removing this code
    /*
    public interface IStudentRepository 
    {
        Task<List<Student>> GetAll();
        Task<Student> GetById(int id, bool useNoTracking = false);
        Task<Student> GetByName(string name);
        Task<int> Create(Student student);
        Task<int> Update(Student student);
        Task<bool> DeleteStudent(Student student);
    }
    */
    public interface IStudentRepository : ICollegeRepository<Student>
    {
        Task <List<Student>> GetStudentsByFeeStatus(int feeStatus);
    }

}
