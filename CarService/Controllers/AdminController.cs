using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using CarService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

namespace CarService.Controllers
{
    public class AdminController : Controller
    {
        private autoServiceContext db;
        private readonly IFileProvider _fileProvider;

        public AdminController(autoServiceContext context, IFileProvider fileProvider)
        {
            db = context;
            _fileProvider = fileProvider;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var emailList = db.Account.ToList();
            return View(emailList);
        }

        [HttpPost]
        public IActionResult Index(Account account)
        {
            var data = db.Account.OrderBy(e => e.Email);
            foreach (var Emails in data)
            {
                string file = _fileProvider.GetFileInfo("/wwwroot/1.html").PhysicalPath;
                string mailbody = System.IO.File.ReadAllText(file);
                string to = Convert.ToString(Emails.Email);
                string from = "newsportalaspcore@gmail.com";
                MailMessage msg = new MailMessage(from, to);
                msg.Subject = "Auto Response Email";
                msg.Body = mailbody;
                msg.BodyEncoding = Encoding.UTF8;
                msg.IsBodyHtml = true;
                SmtpClient client = new SmtpClient("smtp.gmail.com", 25);
                NetworkCredential basicCredential = new NetworkCredential("newsportalaspcore@gmail.com", "123456789QWERtyuiop");
                client.EnableSsl = true;
                client.UseDefaultCredentials = true;
                client.Credentials = basicCredential;
                try
                {
                    client.Send(msg);
                }
                catch
                {
                    ViewBag.Message = "Failed";
                }
            }
            return View(db.Account.ToList());
            ;
        }
    }
}