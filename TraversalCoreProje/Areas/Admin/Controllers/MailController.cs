using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using TraversalCoreProje.Models;

namespace TraversalCoreProje.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MailController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(MailRequest mailRequest)
        {
            MimeMessage mimeMessage = new();

            //gondericek olan kisi bilgileri
            MailboxAddress mailboxAddressFrom = new("Admin", "traversalcore2@gmail.com");
            mimeMessage.From.Add(mailboxAddressFrom);

            //alicak olan kisi bilgileri
            MailboxAddress mailboxAddressTo = new("User", mailRequest.ReceiverMail);
            mimeMessage.To.Add(mailboxAddressTo);
            
            var bodyBuilder = new BodyBuilder();
            bodyBuilder.TextBody=mailRequest.Body;
            mimeMessage.Body = bodyBuilder.ToMessageBody();
            
            mimeMessage.Subject = mailRequest.Subject;
            SmtpClient smtpClient = new();
            smtpClient.Connect("smtp.gmail.com", 587, false);
            smtpClient.Authenticate("traversalcore2@gmail.com", "alınansifreyazilacak");
            smtpClient.Send(mimeMessage);
            smtpClient.Disconnect(true);
            return View();

            //iki adımlı dogrulamayı acmamız gerekiyor hata verilmemesi icin.Sonrasında Uyguluma sifresini girmemiz gerekiyor.
        }
    }
}
