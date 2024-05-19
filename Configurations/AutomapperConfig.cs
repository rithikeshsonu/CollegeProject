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
            //CreateMap<StudentDTO, Student>().ReverseMap(); //This can be used as reversible -> From student to StudentDTO or StudentDto to Student Class
            //Class Level
            //CreateMap<StudentDTO, Student>().ReverseMap().AddTransform<string>(n => string.IsNullOrEmpty(n) ? "Value not found" : n); //Returns string "Value not found" if the value is NULL in DB
            //Property Level - Returns not found in result if any particular value is NULL in DB
            //CreateMap<StudentDTO, Student>().ReverseMap()
            //    .ForMember(n => n.Address, opt => opt.MapFrom(n => n.Address))
            //    .AddTransform<string>(n => string.IsNullOrEmpty(n) ? "No Value Found" : n) ;
            CreateMap<StudentDTO, Student>().ReverseMap()
                .ForMember(n => n.Address, opt => opt.MapFrom(n => string.IsNullOrEmpty(n.Address) ? "No address found" : n.Address));

        }
    }
}
