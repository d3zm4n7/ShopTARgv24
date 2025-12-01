using MailKit.Net.Smtp; // Нужно добавить этот using для SmtpClient
using Microsoft.Extensions.Configuration;
using MimeKit;
using ShopTARgv24.Core.Dto;
using ShopTARgv24.Core.ServiceInterface;
using System.IO;

namespace ShopTARgv24.ApplicationServices.Services
{
    
    public class EmailServices : IEmailServices
    {
        private readonly IConfiguration _config;
        public EmailServices(
            IConfiguration config)
        {
            _config = config;
        }

        public void SendEmail(EmailDto dto)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config.GetSection("EmailUserName").Value));
            email.To.Add(MailboxAddress.Parse(dto.To!));
            email.Subject = dto.Subject;

            var builder = new BodyBuilder
            {
                HtmlBody = dto.Body
            };
            //vaja teha foreach et lisada mitu faili
            //vaja kasutada kontrolli, kus kui faili pole, siis ei lisa


            if (dto.Attachment != null)
            {
                foreach (var file in dto.Attachment)
                {
                    if (file.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            var fileBytes = ms.ToArray();

                            builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                        }
                    }
                }
            }

            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();

            string host = _config.GetSection("EmailHost").Value;
            int port = int.Parse(_config.GetSection("EmailPort").Value);

            smtp.Connect(host, port, MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate(_config.GetSection("EmailUserName").Value, _config.GetSection("EmailPassword").Value);

            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
