using System.IO;
using System.Web;

namespace Web.Service
{
    public class EmailResetPasswordInformationService : IResetPasswordInformationService
    {

        private IEmailSender _emailSender;
        public EmailResetPasswordInformationService(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }
        public void SendPasswordResetEMail(string resetUrl, string email, string userName, string subject)
        {
            var templatePath = HttpContext.Current.Server.MapPath("~/EmailTemplates/ResetPassword.cshtml"); //"~/EmailTemplates/UserConfirmation.cshtml";
            var template = File.ReadAllText(templatePath);
            var emailContent = template.Replace("/#UserName#/", userName).Replace("/#ResetPasswordLink#/", resetUrl).Replace("[Username]",userName);
            _emailSender.Send(emailContent, subject, email, true);
        }
    }
}