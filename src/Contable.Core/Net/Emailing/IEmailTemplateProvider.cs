namespace ApplicationBase.Net.Emailing
{
    public interface IEmailTemplateProvider
    {
        string GetEmailActivationTemplate(string name, string surname, string emailAddress, string password);
        string GetEmailPasswordResetTemplate(string name, string surname, string emailAddress, string passwordResetCode);
    }
}