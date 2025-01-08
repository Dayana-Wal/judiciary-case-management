using Microsoft.Extensions.Hosting;
using System.Collections.Concurrent;

namespace CaseManagement.Business.Service
{
    public class BgService : BackgroundService
    {
        private readonly ConcurrentQueue<(string To, string Subject, string Body)> _emailQueue = new();
        private readonly EmailService _emailService;
        public BgService(EmailService emailService)
        {
            _emailService = emailService;
        }

        public void QueueEmail(string to, string subject, string body)
        {
            _emailQueue.Enqueue((to, subject, body));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (_emailQueue.TryDequeue(out var email))
                {
                    try
                    {
                        //Console.WriteLine(email);
                        _emailService.SendEmail(email.To, email.Subject, email.Body);
                    }
                    catch (Exception ex)
                    {
                        // Handle errors (e.g., log them)
                        Console.WriteLine($"Error sending email: {ex.Message}");
                    }
                }

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
