namespace ClubAccessSystem.API.Logging
{
    public static class CustomLogger
    {
        public static void ConfigureLogging(ILoggingBuilder logging)
        {
            logging.ClearProviders();
            logging.AddConsole();
            logging.AddDebug();
            logging.AddEventLog();


            // Configurar nivel de logging
            logging.SetMinimumLevel(LogLevel.Information);
        }


    }
}
