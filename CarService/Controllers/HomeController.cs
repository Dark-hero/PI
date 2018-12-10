using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using CarService.Models;
using Newtonsoft.Json;

namespace CarService.Controllers
{
    public class HomeController : Controller
    {
        private autoServiceContext db;
        public HomeController(autoServiceContext context)
        {
            db = context;
        }
        [HttpGet]
        public IActionResult Index()

        {
            ViewBag.names = db.Account.ToList();
            return View(db.Comments.OrderByDescending(x => x.Date).ToList());
        }


        [HttpPost]
        public IActionResult ClientForm(string jClient)
        {
            Clients clients;
            try
            {
                clients = JsonConvert.DeserializeObject<Clients>(jClient);
                db.Clients.Add(clients);
                db.SaveChanges();
            }
            catch
            {
                ViewBag.Message = "Заявка не была отправлена";

            }

            return View("Index");
        }
        [HttpPost]
        public IActionResult Registration()
        {
            return Redirect("/User/Login");
        }

        [HttpPost]
        public IActionResult CommentAdd(string jComment)
        {
            Comments comments;
            try
            {
                comments = JsonConvert.DeserializeObject<Comments>(jComment);
            }
            catch (JsonException je)
            {
                return new EmptyResult();
            }

            var email_ = HttpContext.User.Identity.Name;

            if (email_ != null)
            {
                comments.Date = DateTime.Now.Date;
                comments.IdUser = db.Account.Where(a => a.Email == email_).Select(s => s.IdUser).FirstOrDefault();
                db.Comments.Add(comments);
                db.SaveChanges();

                ViewBag.obj = comments;
                ViewBag.Name = db.Account.Where(a => a.Email == email_).Select(s => s.Name).FirstOrDefault();
            }
            else
            {
                ViewBag.Message = "Комментарии могут оставлять только зарегистрированные пользователи";
            }
            return View();
        }
    }
}
