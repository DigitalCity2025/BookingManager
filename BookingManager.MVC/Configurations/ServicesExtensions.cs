using System.Net;
using System.Net.Mail;

namespace BookingManager.MVC.Configurations
{
    public static class ServicesExtensions
    {
        public static void AddSmtp(this IServiceCollection services, ConfigurationManager config)
        {
            SmtpConfig conf = config.GetSection("Smtp").Get<SmtpConfig>()
                ?? throw new Exception("Missing SMTP Config");

            services.AddScoped(b => new SmtpClient
            {
                Host = conf.Host,
                Port = conf.Port,
                Credentials = new NetworkCredential
                {
                    UserName = conf.Username,
                    Password = conf.Password,
                },
                EnableSsl = conf.Ssl,
            });
        }
    }
}
