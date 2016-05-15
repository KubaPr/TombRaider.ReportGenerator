using System.Net;
using System.Net.Mail;

namespace ReportGenerator
{
    class Email
    {
        public void Send(string message)
        {
            var fromAddress = new MailAddress("tombraider.api@o2.pl", "TombRaider");
            var toAddress = new MailAddress("kuba.prokopiuk@gmail.com", "");
            const string fromPassword = "tombraider";
            const string subject = "TombRaider API raport użycia";

            var smtp = new SmtpClient
            {
                Host = "poczta.o2.pl",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var emailMessage = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = message
            })
            {
                smtp.Send(emailMessage);
            }
        }
    }
}
