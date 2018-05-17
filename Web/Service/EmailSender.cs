using System.Linq;
using System.Net.Mail;

namespace Web.Service
{
    public class EmailSender : IEmailSender
    {
        private int _smtpPort;
        private string _host;
        private string _senderEmailAddress;

        public EmailSender(int smtpPort, string host, string senderEmailAddress)
        {
            _smtpPort = smtpPort;
            _host = host;
            _senderEmailAddress = senderEmailAddress;
        }

        public void Send(string emailContent, string subject, string receiverAddress, bool isHtml)
        {
            var mailmessage = new MailMessage();
            mailmessage.From = new MailAddress(_senderEmailAddress);
            mailmessage.To.Add(new MailAddress(receiverAddress));
            mailmessage.IsBodyHtml = isHtml;
            mailmessage.Subject = subject;
            mailmessage.Body = emailContent;
            SendMessage(mailmessage);
        }

        public void Send(string emailContent, string subject, string[] receiverAddress, bool isHtml)
        {
            var mailmessage = new MailMessage();
            mailmessage.From = new MailAddress(_senderEmailAddress);
            receiverAddress.ToList().ForEach(x => mailmessage.To.Add(new MailAddress(x)));
            mailmessage.IsBodyHtml = isHtml;
            mailmessage.Subject = subject;
            mailmessage.Body = emailContent;
            SendMessage(mailmessage);
        }

        private void SendMessage(MailMessage mailMessage)
        {
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Port = _smtpPort;
            smtpClient.Host = _host;
            smtpClient.Timeout = 10000;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Send(mailMessage);
        }

       


    }
}