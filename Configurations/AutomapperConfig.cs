//using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using CollegeProject.Data;
using CollegeProject.Models;

namespace CollegeProject.Configurations
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            //CreateMap<Student, StudentDTO>();
            CreateMap<StudentDTO, Student>().ReverseMap(); //This can be used as reversible -> From student to StudentDTO or StudentDto to Student Class
        }
    }
}
