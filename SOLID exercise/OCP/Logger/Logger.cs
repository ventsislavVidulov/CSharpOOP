namespace SOLID_exercise.OCP.Logger
{
    public class Logger
    {
        public ILogger LoggerService { get; set; }
        public Logger(ILogger logger)
        {
           LoggerService = logger;
        }

        public void Log(string message)
        {
           LoggerService.Log(message);
        }
    }
}
