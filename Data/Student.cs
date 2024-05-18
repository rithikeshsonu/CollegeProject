﻿using System.ComponentModel.DataAnnotations;
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
    }
}
