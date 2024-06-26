﻿
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace CollegeProject.Data.Repository
{
    public class StudentRepository : CollegeRepository<Student>, IStudentRepository
    {
        private readonly CollegeDBContext _collegeDBContext;
        public StudentRepository(CollegeDBContext collegeDBContext) : base(collegeDBContext) 
        {
            _collegeDBContext = collegeDBContext;
        }

        public Task<List<Student>> GetStudentsByFeeStatus(int feeStatus)
        {
            //throw new NotImplementedException();
            //Logic
            return null;

        }

        //As we have created the same functionality in common repository, removing this code
        /*
        public async Task<List<Student>> GetAll()
        {
            return await _collegeDBContext.Students.ToListAsync();
        }
        public async Task<Student> GetById(int id, bool useNoTracking = false)
        {
            if (useNoTracking)
            {
                return await _collegeDBContext.Students.AsNoTracking().Where(n => n.StudentID == id).FirstOrDefaultAsync();
            }
            else
            {
                return await _collegeDBContext.Students.Where(n => n.StudentID == id).FirstOrDefaultAsync();
            }
        }
        public async Task<Student> GetByName(string name)
        {
            return await _collegeDBContext.Students.Where(n => n.StudentName.ToLower().Contains(name.ToLower())).FirstOrDefaultAsync();
        }
        public async Task<int> Create(Student student)
        {
            await _collegeDBContext.Students.AddAsync(student);
            await _collegeDBContext.SaveChangesAsync();
            return student.StudentID;
        }
        public async Task<int> Update(Student student)
        {
            _collegeDBContext.Update(student);
            await _collegeDBContext.SaveChangesAsync();
            return student.StudentID;
        }

        public async Task<bool> DeleteStudent(Student student)
        {
            _collegeDBContext.Students.Remove(student);
            await _collegeDBContext.SaveChangesAsync();
            return true;
        }
        */
    }
}
