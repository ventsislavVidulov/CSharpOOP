namespace SOLID_exercise.OCP.Logger.LoggerMethods
{
    internal class LogToFile : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine($"{message} saved to file");
        }
    }
}
