namespace Web.Service
{
    public interface IUserCreationConfirmationService
    {
        void SendConfirmationEmail(string confirmationUrl, string email, string userName, string subject);
    }
    public interface IResetPasswordInformationService
    {
        void SendPasswordResetEMail(string resetUrl, string email, string userName, string subject);
    }
}

