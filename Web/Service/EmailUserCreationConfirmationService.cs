using System.IO;
using System.Web;
using System.Web.Http.Routing;
using System.Web.Mvc.Razor;
using System.Web.WebPages;


namespace Web.Service
{
    public class EmailUserCreationConfirmationService : IUserCreationConfirmationService
    {
        private const string TemplateName="UserConfirmation.cshtml";
        private IEmailSender _emailSender;

        public EmailUserCreationConfirmationService(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }
        public void SendConfirmationEmail(string confirmationUrl,string email,string username, string subject)
        {

            var templatePath = HttpContext.Current.Server.MapPath("~/EmailTemplates/UserConfirmation.cshtml"); //"~/EmailTemplates/UserConfirmation.cshtml";
            var template = File.ReadAllText(templatePath);
            var emailContent = template.Replace("/#UserName#/", username).Replace("/#Url#/", confirmationUrl).Replace("[Username]", username);
            _emailSender.Send(emailContent, subject, email, true);
        }
    }
}