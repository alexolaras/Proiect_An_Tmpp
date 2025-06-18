namespace Proiect_An.Models.DesignPatterns.Singleton
{
    public sealed class Logger
    {
        private static readonly Lazy<Logger> _instance = new(() => new Logger());
        private Logger() { }
        public static Logger Instance => _instance.Value;
        public void Log(string message)
        {
            File.AppendAllText("app.log", $"{DateTime.Now}: {message}\n");
        }
    }

}
