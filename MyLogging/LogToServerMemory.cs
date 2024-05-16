namespace CollegeProject.MyLogging
{
    public class LogToServerMemory : IMyLoggerr
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
            Console.WriteLine("Log to Server Memory");
        }
    }
}
