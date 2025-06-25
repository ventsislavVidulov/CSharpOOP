namespace SOLID_exercise.OCP.Logger.LoggerMethods
{
    internal class LogToConsole : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}
