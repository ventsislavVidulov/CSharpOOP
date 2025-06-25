namespace SOLID_exercise.OCP.Logger.LoggerMethods
{
    public class LogToEmail : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine($"{message} sent to email");
        }
    }
}
