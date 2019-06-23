using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using CarService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;

namespace CarService.Controllers
{
    [Route("[controller]/[action]")]
    [Controller]
    public class AdminController : Controller
    {
        private autoServiceContext db;

        public AdminController(autoServiceContext context)
        {
            db = context;
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
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

        [Authorize(Roles = "admin")]
        [HttpGet]
        public JsonResult GetUsers()
        {
            var result = db.Account.ToList();
            return Json(result, new JsonSerializerSettings());
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public JsonResult SaveRole(Account account)
        {
            var role = db.Account.Where(x => x.IdUser == account.IdUser).FirstOrDefault();
            if (role!=null)
            {
                role.IdRole = account.IdRole;
            }
            db.SaveChanges();

            return Json(new JsonSerializerSettings());
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public IActionResult AccountRemove(int id)
        {
            Account account = db.Account.FirstOrDefault(x => x.IdUser == id);
            if (account == null)
            {
                return NotFound();
            }
            db.Account.Remove(account);
            db.SaveChanges();
            return Ok(account);
        }

    }
}