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
        //private readonly IMyLoggerr _myLoggerr;
        //public DemoController(IMyLoggerr myLoggerr)
        //{
        //    _myLoggerr = myLoggerr;
        //}
        private readonly ILogger<DemoController> _logger;
        public DemoController(ILogger<DemoController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public ActionResult Index()
        {
            //_myLoggerr.Log("Index method started");
            _logger.LogTrace("Log message from Trace - Index method started");
            _logger.LogDebug("Log message from Debug - Index method started");
            _logger.LogInformation("Log message from Information - Index method started");
            _logger.LogWarning("Log message from Warning - Index method started");
            _logger.LogError("Log message from Error - Index method started");
            _logger.LogCritical("Log message from Critical - Index method started");

            return Ok();
        }
    }
}
