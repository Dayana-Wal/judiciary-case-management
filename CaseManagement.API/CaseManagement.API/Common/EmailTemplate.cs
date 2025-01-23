namespace CaseManagement.API.Common
{
    public class EmailTemplate
    {
        public string Subject { get; set; }
        public string Body { get; set; }

        public EmailTemplate(string subject, string body)
        {
            Subject = subject;
            Body = body;
        }
    }

    public static class EmailTemplates
    {
        public static readonly EmailTemplate WelcomeEmail = new EmailTemplate(
            "Welcome to Case Management Website",
            "Dear User,\n\nWelcome to our Case Management Website. We're glad to have you on board.\n\nBest Regards,\nTeam"
        );

    }


}
