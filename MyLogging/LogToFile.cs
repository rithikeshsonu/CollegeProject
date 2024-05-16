namespace CollegeProject.MyLogging
{
    public class LogToFile : IMyLoggerr
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
            Console.WriteLine("Log to File");
        }
    }
}
