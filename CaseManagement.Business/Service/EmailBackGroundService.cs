using Microsoft.Extensions.Hosting;
using System.Collections.Concurrent;

namespace CaseManagement.Business.Service
{
    public class EmailBackGroundService : BackgroundService
    {
        private readonly ConcurrentQueue<(string ToEmail, string Subject, string Body)> _emailQueue = new();
        private readonly EmailService _emailService;
        public EmailBackGroundService(EmailService emailService)
        {
            _emailService = emailService;
        }

        public void QueueEmail(string toEmail, string subject, string body)
        {
            _emailQueue.Enqueue((toEmail, subject, body));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while(!stoppingToken.IsCancellationRequested)
            {
                if (_emailQueue.TryDequeue(out var email))
                {
                    //using var scope = _serviceProvider.CreateScope();
                    //var emailService = scope.ServiceProvider.GetRequiredService<EmailService>();

                    try
                    {
                        await _emailService.SendEmail(email.ToEmail, email.Subject, email.Body);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error sending email: {ex.Message}");
                    }
                }
            }
            await Task.Delay(1000);
        }
    }
}
