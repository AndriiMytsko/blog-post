using BlogPost.Bll.Managers.Interfaces;
using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Threading.Tasks;

namespace BlogPost.Bll.Managers
{
    public class EmailManager : IEmailManager
    {
        private readonly Func<SmtpClient> _smptClientFactory;

        public EmailManager(Func<SmtpClient> smptClientFactory)
        {
            _smptClientFactory = smptClientFactory;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Адміністрація сайта", "blogpost838@gmail.com"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = $"Для сброса пароля пройдите по ссылке: <a href='{message}'>link</a>"
            };

            using (var client = _smptClientFactory())
            {
                await client.ConnectAsync("smtp.gmail.com", 587, false);
                await client.AuthenticateAsync("blogpost838@gmail.com", "Admin123$!");
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
    }
}
