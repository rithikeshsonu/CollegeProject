namespace CollegeProject.MyLogging
{
    public class LogToDB : IMyLoggerr
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
            Console.WriteLine("Log to DB");
        }
    }
}
