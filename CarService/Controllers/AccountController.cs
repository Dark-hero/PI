using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CarService.Controllers
{
    public class AccountController : Controller
    {
        private autoServiceContext db;
        public AccountController(autoServiceContext context)
        {
            db = context;
        }

        [Authorize]
        public IActionResult Index()
        {
            var email_ = HttpContext.User.Identity.Name;
            if (email_ != null)
            {
                var userID = db.Account.Where(a => a.Email == email_).Select(s => s.IdUser).FirstOrDefault();

                ViewBag.idUser = userID;
                ViewBag.Name_ = email_;
            }
            return View();
        }

        [Authorize]
        [HttpGet]
        public JsonResult GetOrders()
        {
            var email_ = HttpContext.User.Identity.Name;
            var userID = db.Account.Where(a => a.Email == email_).Select(s => s.IdUser).FirstOrDefault();
            var result = db.Orders.Where(x => x.IdUser == userID).Select(x => new Orders
            {
                Id = x.Id,
                VinCode = x.VinCode,
                StartDate = x.StartDate.Date,
                EndDate = x.EndDate,
                OrderCost = x.OrderCost
            }
                ).ToList();
            return Json(result, new JsonSerializerSettings());
        }

        [Authorize]
        [HttpGet]
        public JsonResult GetUserInf(Account account)
        {
            var idUser = account.IdUser;
            var result = db.Account.Where(x => x.IdUser == account.IdUser).
                Select(x => new Account
                {
                    Name=x.Name,
                    Surname= x.Surname,
                    Patronymic =x.Patronymic
                }
                ).FirstOrDefault();
            
            return Json(result, new JsonSerializerSettings());
        }

        [Authorize]
        [HttpGet]
        public JsonResult GetMasterInfo(Orders orders)
        {
            var result = (
                          from m in db.Masters
                          join o in db.Orders on m.IdMaster equals o.IdMaster
                          where o.IdUser == orders.IdUser && o.Id==orders.Id
                          select new
                          {
                              name = m.Name,
                              surname = m.Surname,
                              patronymic = m.Patronymic
                          }
                          ).Distinct();

            return Json(result, new JsonSerializerSettings());
        }

        [Authorize]
        [HttpGet]
        public JsonResult GetAutoInfo(Orders orders)
        {
            var result = (
                          from a in db.Auto
                          join o in db.Orders on a.VinCode equals o.VinCode
                          where o.IdUser == orders.IdUser && o.Id == orders.Id
                          select new
                          {
                              Mark = a.Mark,
                              Model = a.Model,
                              Year = a.Year,
                              Mileage = a.Mileage,
                              RegisterSign = a.RegisterSign
                          }
                          ).Distinct();

            return Json(result, new JsonSerializerSettings());
        }

        [Authorize]
        [HttpGet]
        public JsonResult GetPartsInfo(Orders orders)
        {
            var result = (
                          from o in db.Orders
                          join otp in db.OrdersToParts on o.Id equals otp.IdOrder
                          join p in db.Parts on otp.Artikul equals p.Artikul
                          where o.IdUser == orders.IdUser && o.Id==orders.Id
                          select new
                          {
                              Artikul = p.Artikul,
                              Name = p.Name,
                              Cost = p.Cost
                          }
                          ).ToList();

            return Json(result, new JsonSerializerSettings());
        }

        [Authorize]
        [HttpGet]
        public JsonResult GetWorksInfo(Orders orders)
        {
            var result = (
                          from o in db.Orders
                          join otw in db.OrdersToWorks on o.Id equals otw.IdOrder
                          join tow in db.TypeOfWorks on otw.JobCode equals tow.JobCode
                          where o.IdUser == orders.IdUser && o.Id == orders.Id
                          select new
                          {
                              JobCode = tow.JobCode,
                              TypeOfWork = tow.TypeOfWork,
                              Cost = tow.Cost
                          }
                          ).ToList();

            return Json(result, new JsonSerializerSettings());
        }


        [Authorize]
        [HttpGet]
        public IActionResult Settings()
        {
            var email_ = HttpContext.User.Identity.Name;
            if (email_ != null)
            {
                var userID = db.Account.Where(a => a.Email == email_).Select(s => s.IdUser).FirstOrDefault();

                ViewBag.idUser = userID;
                ViewBag.Name_ = email_;
            }
            return View();
        }

        [Authorize]
        [HttpGet]
        public JsonResult GetUser()
        {
            var result = db.Account.Where(x => x.Email == HttpContext.User.Identity.Name).FirstOrDefault();
            return Json(result,new JsonSerializerSettings());
        }

        [Authorize]
        [HttpPut]
        public IActionResult EditUser([FromBody]Account account)
        {
            var upd = db.Account.Where(x => x.IdUser == account.IdUser).FirstOrDefault();
            if (upd!=null)
            {
                upd.Name = account.Name;
                upd.Surname = account.Surname;
                upd.Patronymic = account.Patronymic;
                upd.Email = account.Email;
                upd.Phone = account.Phone;
            }

            db.SaveChanges();
            return Ok(account);
        }

        [HttpPost]
        public JsonResult EditPassword(AccountPasswordUpdate passwordUpdate)
        {
            var pass = db.Account.Where(x => x.IdUser == passwordUpdate.IdUser).FirstOrDefault();
            if (pass.Password == SimpleHash.ComputeHash( passwordUpdate.OldPass))
            {
                pass.Password = SimpleHash.ComputeHash(passwordUpdate.Password);
            }
            db.SaveChanges();

            return Json( new JsonSerializerSettings());
        }
    }
}