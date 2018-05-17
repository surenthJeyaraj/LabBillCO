namespace Web.Service
{
    public interface IEmailSender
    {
        void Send(string emailContent, string subject, string receiverAddress, bool isHtml);
        void Send(string emailContent, string subject, string[] receiverAddress, bool isHtml);
    }
}