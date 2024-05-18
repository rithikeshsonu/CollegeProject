using Microsoft.AspNetCore.Mvc;
using CollegeProject.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.JsonPatch;
using CollegeProject.MyLogging;
using CollegeProject.Data;

namespace CollegeProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class StudentController : ControllerBase
    {
        //Custom Logging implemented using Dependency Injection - Loosely coupled..
        /*private readonly IMyLoggerr _myLoggerr;
        public StudentController(IMyLoggerr myLoggerr)
        {
            _myLoggerr = myLoggerr;
        }
        */
        private readonly ILogger<StudentController> _logger;
        private readonly CollegeDBContext _collegeDBContext;
        public StudentController(ILogger<StudentController> logger, CollegeDBContext collegeDBContext)
        {
            _logger = logger;
            _collegeDBContext = collegeDBContext;
        }

        [HttpGet]
        [Route("All", Name = "GetAllStudents")]
        [ProducesResponseType(200)]
        public ActionResult<IEnumerable<StudentDTO>> GetStudents()
        {
            //_myLoggerr.Log("Your logging message"); //for custom Dependency Injection
            _logger.LogInformation("Get Students method started");
            var students = _collegeDBContext.Students.Select(temp => new StudentDTO()
            {
                StudentID = temp.StudentID,
                StudentName = temp.StudentName,
                Email = temp.Email,
                Address = temp.Address,
                DOB = temp.DOB
            }).ToList(); //Added inorder for it to configure with XML

            return Ok(students);
            //return "Sample student name";
        }

        //[HttpGet]
        //[Route("{id:int:range(1, 10)}", Name = "GetStudentById")]
        //[ProducesResponseType(200)]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(404)]
        //[ProducesResponseType(500)]
        //public ActionResult<Student> GetStudentById(int id)
        //{
        //    if(id <= 0)
        //    {
        //        return BadRequest();
        //    }

        //    var student = CollegeRepository.Students.Where(temp => temp.StudentID == id).FirstOrDefault();

        //    if(student == null)
        //    {
        //        return NotFound($"Student with id = {id} not found");
        //    }    
        //    return Ok(student);
        //}

        [HttpGet]
        [Route("{id:int:range(1, 10)}", Name = "GetStudentById")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult<StudentDTO> GetStudentById(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Bad request");
                return BadRequest();
            }

            var student = _collegeDBContext.Students.Where(temp => temp.StudentID == id).FirstOrDefault();

            if (student == null)
            {
                _logger.LogError("Student with id not found");
                return NotFound($"Student with id = {id} not found");
            }
            var studentDTO = new StudentDTO
            {
                StudentID = student.StudentID,
                StudentName = student.StudentName,
                Email = student.Email,
                Address = student.Address,
                DOB=student.DOB
            };
            return Ok(studentDTO);
        }

        [HttpGet]
        //[Route("{name:regex(^[A-Za-z0-9\\- ]+$)}", Name = "GetStudentByName")]
        //api/[controller]/{name:regex(^[A-Za-z0-9\- ]+$)}
        //Route["api/[controller]/{name:regex(^[A-Za-z0-9\\- ]+$)}\r\n"]
        [Route("{name:alpha}", Name = "GetStudentByName")]
        //[ProducesResponseType(200)]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(404)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<StudentDTO> GetStudentByName(string name)
        {
            if(string.IsNullOrEmpty(name))
            {
                return BadRequest();
            }
            var student = _collegeDBContext.Students.Where(temp => temp.StudentName == name).FirstOrDefault();
            if (student == null)
            {
                return NotFound($"Student with name = {name} not found");
            }
            var studentDTO = new StudentDTO
            {
                StudentID = student.StudentID,
                StudentName = student.StudentName,
                Email = student.Email,
                Address = student.Address
            };
            return Ok(studentDTO);
        }

        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public ActionResult<StudentDTO> CreateStudent([FromBody]StudentDTO model)
        {
            if(model == null) { 
                return BadRequest();
            }

            //if(model.AdmissionDate < DateTime.Now)
            //{
            //    ModelState.AddModelError("Admission date error", "Admission date must not be in the past");
            //    return BadRequest(ModelState);
            //}

            //int newId = CollegeRepository.Students.LastOrDefault().StudentID + 1;
            Student student = new Student()
            {
                //StudentID = newId,
                StudentName = model.StudentName,
                Email = model.Email,
                Address = model.Address,
                DOB = model.DOB
                //age = model.age,
                //Password = model.Password,
                //ConfirmPassword = model.ConfirmPassword
            };
            _collegeDBContext.Students.Add(student);
            _collegeDBContext.SaveChanges();
            model.StudentID = student.StudentID;
            //Also returns a new url with whatever id it created. In Swagger you can see it as 'location' field.
            return CreatedAtRoute("GetStudentById", new { id = model.StudentID }, model);
            //return Ok(model);
        }

        [HttpPut]
        [Route("Update")]
        //api/student/update
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult UpdateStudent([FromBody]StudentDTO model)
        {
            if(model == null || model.StudentID <= 0)
            {
                return BadRequest();
            }
            var existingStudent = _collegeDBContext.Students.Where(temp => temp.StudentID == model.StudentID).FirstOrDefault();
            if(existingStudent == null)
            {
                return NotFound();
            }
            existingStudent.StudentName = model.StudentName;
            existingStudent.Email = model.Email;
            existingStudent.Address = model.Address;
            existingStudent.DOB = model.DOB;
            _collegeDBContext.SaveChanges();
            return NoContent(); //204 success to indicate record updated but no output needed to show
        }

        //We need to install two libraries - JSONPatch and NewtonsoftJson from Nuget Package Manager
        [HttpPatch]
        [Route("{studentID:int}/UpdatePartial")]
        //api/student/id/UpdatePartial
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult PartialUpdateStudent(int studentID, [FromBody] JsonPatchDocument<StudentDTO> patchDocument)
        {
            if (patchDocument == null || studentID <= 0)
            {
                return BadRequest();
            }
            var existingStudent = _collegeDBContext.Students.Where(temp => temp.StudentID == studentID).FirstOrDefault();
            if (existingStudent == null)
            {
                return NotFound();
            }
            var studentDTO = new StudentDTO
            {
                StudentID = existingStudent.StudentID,
                StudentName = existingStudent.StudentName,
                Email = existingStudent.Email,
                Address = existingStudent.Address,
                DOB = existingStudent.DOB
            };
            patchDocument.ApplyTo(studentDTO, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            existingStudent.StudentName = studentDTO.StudentName;
            existingStudent.Email = studentDTO.Email;
            existingStudent.Address = studentDTO.Address;
            existingStudent.DOB = studentDTO.DOB;
            _collegeDBContext.SaveChanges();
            return NoContent(); //204 success to indicate record updated but no output needed to show
        }

        [HttpDelete]
        [Route("Delete/{StudentID}", Name = "DeleteStudentById")]
        //Student/Delete/{StudentID}
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<bool> DeleteStudent(int StudentID)
        {
            if (StudentID <= 0)
            {
                return BadRequest();
            }
            var student = _collegeDBContext.Students.Where(temp => temp.StudentID == StudentID).FirstOrDefault();
            if (student == null)
            {
                return NotFound($"Student with {StudentID} not found");
            }

            _collegeDBContext.Students.Remove(student);
            _collegeDBContext.SaveChanges();
            return Ok(true);
        }
    }
}
