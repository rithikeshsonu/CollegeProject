using CollegeProject.Validations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace CollegeProject.Models
{
    public class StudentDTO
    {
        [ValidateNever]
        public int StudentID { get; set; }
        [Required(ErrorMessage = "Student Name field is required")]
        [StringLength(30)]
        public string StudentName { get; set; }
        [EmailAddress(ErrorMessage ="Please enter valid email address")]
        public string Email { get; set; }
        [Required(ErrorMessage ="Please enter address correctly")]
        public string Address { get; set; }
        //[Range(10, 20)]
        //public int age { get; set; }
        //public string Password { get; set; }
        ////[Compare("Password")]
        //[Compare(nameof(Password))]
        //public string ConfirmPassword { get; set; }
        //[DateCheck]
        //public DateTime AdmissionDate { get; set; }
        public DateTime DOB { get; set; }
    }
}
