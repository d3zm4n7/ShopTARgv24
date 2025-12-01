using MailKit;
using Microsoft.AspNetCore.Mvc;
using ShopTARgv24.ApplicationServices.Services;
using ShopTARgv24.Core.Dto;
using ShopTARgv24.Core.ServiceInterface;
using ShopTARgv24.Models.Email;

namespace ShopTARgv24.Controllers
{
    public class EmailController : Controller
    {
        private readonly IEmailServices _emailServices;

        public EmailController
            (
                IEmailServices emailServices
            )
        {
            _emailServices = emailServices;
        }
        public IActionResult Index()
        {
            return View();
        }
       
        [HttpPost]
        public IActionResult SendEmail(EmailViewModel vm)
        {
            var files = Request.Form.Files.Any() ? Request.Form.Files.ToList() : new List<IFormFile>();

            var dto = new EmailDto
            {
                To = vm.To,
                Subject = vm.Subject,
                Body = vm.Body,
                Attachment = files
            };

            _emailServices.SendEmail(dto);

            return RedirectToAction(nameof(Index));
        }

    }
}
