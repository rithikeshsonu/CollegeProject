using Microsoft.AspNetCore.Mvc;
using CollegeProject.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.JsonPatch;
using CollegeProject.MyLogging;
using CollegeProject.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using AutoMapper;
using CollegeProject.Data.Repository;

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
        //private readonly CollegeDBContext _collegeDBContext;
        private readonly IMapper _mapper;
        private readonly IStudentRepository _studentRepository;
        //public StudentController(ILogger<StudentController> logger, CollegeDBContext collegeDBContext, IMapper mapper)
        public StudentController(ILogger<StudentController> logger, IMapper mapper, IStudentRepository studentRepository)
        {
            _logger = logger;
            //_collegeDBContext = collegeDBContext;
            _mapper = mapper;
            _studentRepository = studentRepository;
        }

        [HttpGet]
        [Route("All", Name = "GetAllStudents")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<StudentDTO>>> GetStudents()
        {
            //_myLoggerr.Log("Your logging message"); //for custom Dependency Injection
            _logger.LogInformation("Get Students method started");
            //var students = await _collegeDBContext.Students.ToListAsync(); //Entity framework
            var students = await _studentRepository.GetAll();
            //var students = await _collegeDBContext.Students.Select(temp => new StudentDTO()
            //{
            //    StudentID = temp.StudentID,
            //    StudentName = temp.StudentName,
            //    Email = temp.Email,
            //    Address = temp.Address,
            //    //DOB = temp.DOB.ToShortDateString()
            //    DOB = temp.DOB
            //}).ToListAsync(); //Added inorder for it to configure with XML

            //return Ok(students);
            //return "Sample student name";
            var studentDTOData = _mapper.Map<List<StudentDTO>>(students);
            return Ok(studentDTOData);
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
        public async Task<ActionResult<StudentDTO>> GetStudentById(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Bad request");
                return BadRequest();
            }

            //var student = await _collegeDBContext.Students.Where(temp => temp.StudentID == id).FirstOrDefaultAsync();
            var student = await _studentRepository.GetById(id);
            if (student == null)
            {
                _logger.LogError("Student with id not found");
                return NotFound($"Student with id = {id} not found");
            }
            //var studentDTO = new StudentDTO
            //{
            //    StudentID = student.StudentID,
            //    StudentName = student.StudentName,
            //    Email = student.Email,
            //    Address = student.Address,
            //    //DOB=student.DOB.ToShortDateString()
            //    DOB = student.DOB
            //};
            //return Ok(studentDTO);
            var studentDTO = _mapper.Map<StudentDTO>(student);
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
        public async Task<ActionResult<StudentDTO>> GetStudentByName(string name)
        {
            if(string.IsNullOrEmpty(name))
            {
                return BadRequest();
            }
            //var student = await _collegeDBContext.Students.Where(temp => temp.StudentName == name).FirstOrDefaultAsync();
            var student = await _studentRepository.GetByName(name);

            if (student == null)
            {
                return NotFound($"Student with name = {name} not found");
            }
            //var studentDTO = new StudentDTO
            //{
            //    StudentID = student.StudentID,
            //    StudentName = student.StudentName,
            //    Email = student.Email,
            //    Address = student.Address,
            //    //DOB = student.DOB.ToShortDateString()
            //    DOB = student.DOB
            //};
            //return Ok(studentDTO);
            var studentDTO = _mapper.Map<StudentDTO>(student);
            return Ok(studentDTO);
        }

        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<StudentDTO>> CreateStudent([FromBody]StudentDTO model)
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
            //Student student = new Student()
            //{
            //    //StudentID = newId,
            //    StudentName = model.StudentName,
            //    Email = model.Email,
            //    Address = model.Address,
            //    DOB = model.DOB
            //    //DOB = Convert.ToDateTime(model.DOB)
            //    //DOB = model.DOB.ToString("yyyy-MM-dd");

            //    //age = model.age,
            //    //Password = model.Password,
            //    //ConfirmPassword = model.ConfirmPassword
            //};
            Student student = _mapper.Map<Student>(model);
            //await _collegeDBContext.Students.AddAsync(student);
            //await _collegeDBContext.SaveChangesAsync();
            var id = await _studentRepository.Create(student);
            model.StudentID = id;
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
        public async Task<ActionResult> UpdateStudent([FromBody]StudentDTO model)
        {
            if(model == null || model.StudentID <= 0)
            {
                return BadRequest();
            }
            //To not track the existing recors which is tracking our ID in entity framework
            //var existingStudent = await _collegeDBContext.Students.AsNoTracking().Where(temp => temp.StudentID == model.StudentID).FirstOrDefaultAsync();
            var existingStudent = await _studentRepository.GetById(model.StudentID, true);
            if(existingStudent == null)
            {
                return NotFound();
            }

            //var newStudentRecord = new Student()
            //{
            //    StudentID = existingStudent.StudentID,
            //    StudentName = model.StudentName,
            //    Address = model.Address,
            //    DOB = model.DOB,
            //    Email = model.Email
            //};
            var newStudentRecord = _mapper.Map<Student>(model);
            //_collegeDBContext.Students.Update(newStudentRecord);

            //existingStudent.StudentName = model.StudentName;
            //existingStudent.Email = model.Email;
            //existingStudent.Address = model.Address;
            ////existingStudent.DOB = Convert.ToDateTime(model.DOB);
            //existingStudent.DOB = model.DOB;

            //await _collegeDBContext.SaveChangesAsync();
            await _studentRepository.Update(newStudentRecord);
            return NoContent(); //204 success to indicate record updated but no output needed to show
        }

        //We need to install two libraries - JSONPatch and NewtonsoftJson from Nuget Package Manager
        [HttpPatch]
        [Route("{studentID:int}/UpdatePartial")]
        //api/student/id/UpdatePartial
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> PartialUpdateStudent(int studentID, [FromBody] JsonPatchDocument<StudentDTO> patchDocument)
        {
            if (patchDocument == null || studentID <= 0)
            {
                return BadRequest();
            }
            //var existingStudent = await _collegeDBContext.Students.AsNoTracking().Where(temp => temp.StudentID == studentID).FirstOrDefaultAsync();
            var existingStudent = await _studentRepository.GetById(studentID, true);
            if (existingStudent == null)
            {
                return NotFound();
            }
            //var studentDTO = new StudentDTO
            //{
            //    StudentID = existingStudent.StudentID,
            //    StudentName = existingStudent.StudentName,
            //    Email = existingStudent.Email,
            //    Address = existingStudent.Address,
            //    //DOB = existingStudent.DOB.ToShortDateString()
            //    DOB = existingStudent.DOB
            //};
            var studentDTO = _mapper.Map<StudentDTO>(existingStudent);
            patchDocument.ApplyTo(studentDTO, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            existingStudent = _mapper.Map<Student>(studentDTO);
            //_collegeDBContext.Students.Update(existingStudent);
            //existingStudent.StudentName = studentDTO.StudentName;
            //existingStudent.Email = studentDTO.Email;
            //existingStudent.Address = studentDTO.Address;
            ////existingStudent.DOB =  Convert.ToDateTime(studentDTO.DOB);
            //existingStudent.DOB = studentDTO.DOB;
            //await _collegeDBContext.SaveChangesAsync();
            await _studentRepository.Update(existingStudent);
            return NoContent(); //204 success to indicate record updated but no output needed to show
        }

        [HttpDelete]
        [Route("Delete/{StudentID}", Name = "DeleteStudentById")]
        //Student/Delete/{StudentID}
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> DeleteStudent(int StudentID)
        {
            if (StudentID <= 0)
            {
                return BadRequest();
            }
            //var student = await _collegeDBContext.Students.Where(temp => temp.StudentID == StudentID).FirstOrDefaultAsync();
            var student = await _studentRepository.GetById(StudentID);
            if (student == null)
            {
                return NotFound($"Student with {StudentID} not found");
            }

            //_collegeDBContext.Students.Remove(student);
            //await _collegeDBContext.SaveChangesAsync();
            await _studentRepository.DeleteStudent(student);
            return Ok(true);
        }
    }
}
