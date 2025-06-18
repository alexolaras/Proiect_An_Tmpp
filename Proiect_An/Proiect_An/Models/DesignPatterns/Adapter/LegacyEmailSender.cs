using System.Net.Mail;
using System.Net;

namespace Proiect_An.Models.DesignPatterns.Adapter
{
    public class LegacyEmailSender
    {
        private string _from = "alex.olaras11@gmail.com";
        public void SendEmail(string address, string body)
        {
            var smtp = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("alex.olaras11@gmail.com", "svvz jhxt epcx zxrp"),
                EnableSsl = true,
            };

            var mail = new MailMessage(_from, address, "Booking Confirmation", body);
            smtp.Send(mail);
        }
    }

}
