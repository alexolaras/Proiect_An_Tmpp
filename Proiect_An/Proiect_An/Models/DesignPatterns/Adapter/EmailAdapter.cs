namespace Proiect_An.Models.DesignPatterns.Adapter
{
    public class EmailAdapter : INotifier
    {
        private readonly LegacyEmailSender _legacyEmailSender;

        public EmailAdapter(LegacyEmailSender legacyEmailSender)
        {
            _legacyEmailSender = legacyEmailSender;
        }

        public void Send(string to, string message)
        {
            _legacyEmailSender.SendEmail(to, message);
        }
    }

}
