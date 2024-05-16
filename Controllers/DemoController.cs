using CollegeProject.MyLogging;
using Microsoft.AspNetCore.Mvc;

namespace CollegeProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        //Strongly coupled Dependency Injection
        /*private readonly IMyLoggerr _myLoggerr;
        public DemoController()
        {
            _myLoggerr = new LogToServerMemory();
        }
        [HttpGet]
        public ActionResult Index()
        {
            _myLoggerr.Log("Index method started");
            return Ok();
        }
        */
        //Loosely coupled Dependency Injection - Easier to maintain
        private readonly IMyLoggerr _myLoggerr;
        public DemoController(IMyLoggerr myLoggerr)
        {
            _myLoggerr = myLoggerr;
        }
        [HttpGet]
        public ActionResult Index()
        {
            _myLoggerr.Log("Index method started");
            return Ok();
        }
    }
}
