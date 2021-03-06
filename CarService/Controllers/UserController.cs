﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Threading.Tasks;
using CarService.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace CarService.Controllers
{
    [Route("[controller]/[action]")]
    [Controller]
    public class UserController : Controller
    {
        private async Task Authenticate(Account user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType,user.Roles?.Role )
            };
            ClaimsIdentity id = new ClaimsIdentity(claims,
                "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        private autoServiceContext db;
        public UserController(autoServiceContext context)
        {
            db = context;
        }

        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Registration([FromForm] Account registration)
        {
            if (registration.Name != null && registration.Password != null && registration.Phone != null && registration.Email != null)
            {
                bool Status = false;
                string message = "";

                var IsExist = IsEmailExist(registration.Email);
                if (IsExist)
                {
                    ModelState.AddModelError("EmailExist", "Такой электронный адрес уже существует");
                    return View(registration);
                }

                registration.ActivationCode = Guid.NewGuid();

                registration.Password = SimpleHash.ComputeHash(registration.Password);

                registration.Verified = false;
                registration.DateOfRegistration = DateTime.Now;

                Account account = db.Account.FirstOrDefault(u => u.Email == registration.Email);

                if (account != null)
                {
                    ModelState.AddModelError("", "Такой пользователь уже есть");
                }
                registration.IdRole = 1;

                db.Account.Add(registration);
                db.SaveChanges();

                SendVerificationLinkEmail(registration.Email, registration.ActivationCode.ToString());
                message = "Ссылка на активацию аккаунта " +
                    " была отправлена на указанную почту: " + registration.Email;
                Status = true;

                ModelState.Clear();
                ViewBag.Message = registration.Name + " " + " успешно зарегистрирован(a)";

                ViewBag.Message = message;
                ViewBag.Status = Status;
            }
            else
            {
                return View();
            }
            return View(registration);
        }

        [NonAction]
        public bool IsEmailExist(string emailID)
        {
            var accountEmail = db.Account.Where(a => a.Email == emailID).FirstOrDefault();
            return accountEmail != null;
        }

        [NonAction]
        public string SendVerificationLinkEmail(string emailID, string activeCode, string emailFor = "VerifyAccount")
        {
            var verifyUri = "/User/" + emailFor + "/" + activeCode;
            var link = UriHelper.GetDisplayUrl(Request).Replace(UriHelper.GetEncodedPathAndQuery(Request), verifyUri);

            var fromEmail = new MailAddress("newsportalaspcore@gmail.com", "Гараж 24");
            var toEmail = new MailAddress(emailID);
            var fromEmailPassword = "PASS";

            string subject = "";
            string body = "";
            if (emailFor == "VerifyAccount")
            {
                subject = "Ваш аккаунт был создан";
                body = "<br/><br/> Чтобы получить доступ к аккаунту пожалуйста перейдите по ссылке" +
                   "<a href='" + link + "'>" + link + "</a>";
            }
            else if (emailFor == "ResetPassword")
            {
                subject = "Восстановить пароль";
                body = "Администрация, <br/><br/> Мы сборосили пароль от вашего аккаунта. Пожалуйста, перейдите по ссылке" +
                    " чтобы восстановить ваш пароль от аккаунта <br/><br/><a href=" + link + ">Восстановление пароля</a>";
            }
            var smtp = new SmtpClient()
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
                smtp.Send(message);
            return subject;
        }

        [HttpGet("{id}")]
        public ActionResult VerifyAccount(string id)
        {
            bool Status = false;
            try
            {
                var activation_code = db.Account.Where(a => a.ActivationCode == new Guid(id)).FirstOrDefault();

                if (activation_code != null)
                {
                    activation_code.Verified = true;
                    db.SaveChanges();
                    Status = true;
                }
                else
                {
                    ViewBag.Message = "Ошибка запроса";
                }
            }
            catch
            {
                ViewBag.Message = "Ошибка запроса";
            }
            ViewBag.Status = Status;
            return RedirectToAction("Login", "User");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(ViewLogin login)
        {
            if (login.Email != null && login.Password != null)
            {
                login.Password = SimpleHash.ComputeHash(login.Password);
                Account user = await db.Account.Include(u => u.Roles).FirstOrDefaultAsync(u => u.Email == login.Email && u.Password == login.Password);
                if (user != null)
                {
                    if (user.Verified == true)
                    {
                        if (user.IdRole == 0)
                        {
                            await Authenticate(user);
                            return Redirect("/Admin/Index");
                        }
                        else if(user.IdRole == 1)
                        {
                            await Authenticate(user);
                            return Redirect("/Account/Index");
                        }
                        else
                        {
                            await Authenticate(user);
                            return Redirect("/Manager/Clients");
                        }
                    }
                    else
                    {
                        ViewBag.Message = "Для входа в аккаунт нужно подтвердить почту";
                    }
                }
                else
                {
                    ViewBag.Message = "Неверный логин и(или) пароль";
                }
            }
            else
            {
                return View();
            }
            return View(login);
        }


        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(Account model)
        {
            string message = "";
            var account = db.Account.Where(a => a.Email == model.Email).FirstOrDefault();
            if (account != null)
            {
                string resetCode = Guid.NewGuid().ToString();
                SendVerificationLinkEmail(account.Email, resetCode, "ResetPassword");
                account.ResetPasswordCode = resetCode;

                db.SaveChanges();

                message = "Ссылка на сбор пароля была отправлена на вашу почту!";
            }
            else
            {
                message = "Аккаунт с такой почтой не найден!";
            }
            ViewBag.Message = message;
            return View();
        }

        [HttpGet("{id}")]
        public IActionResult ResetPassword(string id)
        {
            var user = db.Account.Where(u => u.ResetPasswordCode == id).FirstOrDefault();
            if (user != null)
            {
                Account model = new Account();
                model.ResetPasswordCode = id;
                return View(model);
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(Account account)
        {
            string message = "";
            var userPass = db.Account.Where(u => u.ResetPasswordCode == account.ResetPasswordCode).FirstOrDefault();
            if (userPass != null)
            {
                userPass.Password = SimpleHash.ComputeHash(account.Password);
                userPass.ResetPasswordCode = "";
                db.SaveChanges();
                message = "Новый пароль успешно применен";

            }
            else
            {
                message = "Ошибка";
                return View();
            }
            ViewBag.Message = message;
            return RedirectToAction("Login");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "User");
        }
    }
}