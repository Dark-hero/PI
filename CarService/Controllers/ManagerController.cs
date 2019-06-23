using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.Serialization.Json;
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
    public class ManagerController : Controller
    {
        private autoServiceContext db;
        private readonly IFileProvider _fileProvider;

        public ManagerController(autoServiceContext context, IFileProvider fileProvider)
        {
            db = context;
            _fileProvider = fileProvider;

        }

        [Authorize(Roles = "manager")]
        [HttpGet]
        public IActionResult Clients()
        {
            return View(db.Clients.Where(s => s.Date.Date == DateTime.Today && s.IsCancel == false && s.IsRecord == false).ToList());
        }

        [Authorize(Roles = "manager")]
        [HttpPost]
        public IActionResult CancelClient(string jClient)
        {
            Clients clients;

            clients = JsonConvert.DeserializeObject<Clients>(jClient);
            var UpdateClient = db.Clients.SingleOrDefault(c => c.IdClient == clients.IdClient);
            if (UpdateClient != null)
            {
                UpdateClient.IsCancel = true;
                db.SaveChanges();
            }

            return RedirectToAction("/Clients");
        }

        [Authorize(Roles = "manager")]
        [HttpPost]
        public IActionResult RecordClient(string jClient)
        {
            Clients clients;

            clients = JsonConvert.DeserializeObject<Clients>(jClient);
            var UpdateClient = db.Clients.SingleOrDefault(c => c.IdClient == clients.IdClient);
            if (UpdateClient != null)
            {
                UpdateClient.IsRecord = true;
                db.SaveChanges();
            }

            return RedirectToAction("Record");
        }

        [Authorize(Roles = "manager")]
        [HttpGet]
        public IActionResult Record()
        {
            return View();
        }

        [Authorize(Roles = "manager")]
        public JsonResult GetEvents()
        {
            var events = db.Records.ToList();
            var sa = new JsonSerializerSettings();
            return Json(events, sa);
        }

        [Authorize(Roles = "manager")]
        [HttpPost]
        public JsonResult SaveEvent(Records records)
        {
            var status = false;

            if (records.IdRecod > 0)
            {
                var updRecord = db.Records.Where(a => a.IdRecod == records.IdRecod).FirstOrDefault();
                if (updRecord != null)
                {
                    updRecord.Subject = records.Subject;
                    updRecord.StartDate = records.StartDate;
                    updRecord.EndDate = records.EndDate;
                    updRecord.Description = records.Description;
                    updRecord.IsFullDay = records.IsFullDay;
                    updRecord.ThemeColor = records.ThemeColor;
                }
            }
            else
            {
                db.Records.Add(records);
            }

            db.SaveChanges();
            status = true;
            return Json(new { status });
        }

        [Authorize(Roles = "manager")]
        [HttpPost]
        public JsonResult DeleteEvent(int eventID)
        {
            var status = false;

            var delRecord = db.Records.Where(a => a.IdRecod == eventID).FirstOrDefault();
            if (delRecord != null)
            {
                db.Records.Remove(delRecord);
                db.SaveChanges();
                status = true;
            }
            return Json(new { status });
        }

        [Authorize(Roles = "manager")]
        [HttpGet]
        public IActionResult Masters()
        {
            return View();
        }

        [Authorize(Roles = "manager")]
        [HttpGet]
        public IEnumerable<Masters> GetMasters()
        {
            return db.Masters.ToList();
        }

        [Authorize(Roles = "manager")]
        [HttpGet("{id}")]
        public IActionResult GetMaster(int id)
        {
            Masters masters = db.Masters.FirstOrDefault(x => x.IdMaster == id);
            if (masters == null)
                return NotFound();
            return new ObjectResult(masters);
        }

        [Authorize(Roles = "manager")]
        [HttpPost]
        public IActionResult MasterAdd([FromBody]Masters model)
        {

            model.DateOfEmployment = DateTime.Now;
            model.IsWork = true;

            db.Masters.Add(model);
            db.SaveChanges();


            return Ok(model);
        }

        [Authorize(Roles = "manager")]
        [HttpDelete("{id}")]
        public IActionResult MasterRemove (int id)
        {
            Masters masters = db.Masters.FirstOrDefault(x => x.IdMaster == id);
            if (masters == null)
            {
                return NotFound();
            }
            db.Masters.Remove(masters);
            db.SaveChanges();
            return Ok(masters);
        }

        [Authorize(Roles = "manager")]
        [HttpPut]
        public IActionResult EditMaster([FromBody]Masters masters)
        {
            if (masters == null)
            {
                return BadRequest();
            }
            if (!db.Masters.Any(x => x.IdMaster == masters.IdMaster))
            {
                return NotFound();
            }

            db.Update(masters);
            db.SaveChanges();
            return Ok(masters);
        }

        [Authorize(Roles = "manager")]
        public IActionResult MastersDetails(int id)
        {
            return View(
                db.Masters.Where(x=> x.IdMaster == id).FirstOrDefault()
                );
        }

        [Authorize(Roles = "manager")]
        [HttpGet]
        public IActionResult Parts()
        {
            return View();
        }

        [Authorize(Roles = "manager")]
        [HttpGet]
        public IEnumerable<Parts> GetParts()
        {
            return db.Parts.ToList();
        }

        [Authorize(Roles = "manager")]
        [HttpGet("{id}")]
        public IActionResult GetPart(int id)
        {
            Parts parts = db.Parts.FirstOrDefault(x => x.Artikul == id);
            if (parts == null)
                return NotFound();
            return new ObjectResult(parts);
        }

        [Authorize(Roles = "manager")]
        [HttpPost]
        public IActionResult PartAdd([FromBody]Parts model)
        {

            model.DateOfDelivery = DateTime.Now;
            db.Parts.Add(model);
            db.SaveChanges();


            return Ok(model);
        }

        [Authorize(Roles = "manager")]
        [HttpDelete("{artikul}")]
        public IActionResult PartRemove(int artikul)
        {
            Parts parts = db.Parts.FirstOrDefault(x => x.Artikul == artikul);
            if (parts == null)
            {
                return NotFound();
            }
            db.Parts.Remove(parts);
            db.SaveChanges();
            return Ok(parts);
        }

        [Authorize(Roles = "manager")]
        [HttpPut]
        public IActionResult EditPart([FromBody]Parts parts)
        {
            if (parts == null)
            {
                return BadRequest();
            }
            if (!db.Parts.Any(x => x.Artikul == parts.Artikul))
            {
                return NotFound();
            }

            db.Update(parts);
            db.SaveChanges();
            return Ok(parts);
        }

        [Authorize(Roles = "manager")]
        [HttpGet]
        public IActionResult TypeOfWorks()
        {
            return View();
        }


        [Authorize(Roles = "manager")]
        [HttpGet]
        public IEnumerable<TypeOfWorks> GetTypeOfWork()
        {
            return db.TypeOfWorks.ToList();
        }

        [Authorize(Roles = "manager")]
        [HttpGet("{id}")]
        public IActionResult GetTypeOfWork(int id)
        {
            TypeOfWorks ofWorks = db.TypeOfWorks.FirstOrDefault(x => x.JobCode == id);
            if (ofWorks == null)
                return NotFound();
            return new ObjectResult(ofWorks);
        }

        [Authorize(Roles = "manager")]
        [HttpPost]
        public IActionResult TypeOfWorkAdd([FromBody]TypeOfWorks model)
        {
            db.TypeOfWorks.Add(model);
            db.SaveChanges();

            return Ok(model);
        }

        [Authorize(Roles = "manager")]
        [HttpDelete("{id}")]
        public IActionResult DeleteTypeOfWork(int id)
        {
            TypeOfWorks ofWorks = db.TypeOfWorks.FirstOrDefault(x => x.JobCode == id);
            if (ofWorks == null)
            {
                return NotFound();
            }
            db.TypeOfWorks.Remove(ofWorks);
            db.SaveChanges();
            return Ok(ofWorks);
        }

        [Authorize(Roles = "manager")]
        [HttpPut]
        public IActionResult EditTypeOfWork([FromBody]TypeOfWorks ofWorks)
        {
            if (ofWorks == null)
            {
                return BadRequest();
            }
            if (!db.TypeOfWorks.Any(x => x.JobCode == ofWorks.JobCode))
            {
                return NotFound();
            }

            db.Update(ofWorks);
            db.SaveChanges();
            return Ok(ofWorks);
        }

        [Authorize(Roles = "manager")]
        public IActionResult Statistic()
        {
            return View();
        }

        [Authorize(Roles = "manager")]
        public JsonResult GetStatistic(string jDiagram)
        {
            ClientView clients;

            clients = JsonConvert.DeserializeObject<ClientView>(jDiagram);

            var result = db.Clients.Where(p => p.Date.CompareTo(clients.Start)>0 && p.Date.CompareTo(clients.Finish)<0).GroupBy(i => i.Date.ToString("yyyy.MM.dd")).Select(i => new {count = i.Count(), date = i.Key });
            return Json(result.ToList(), new JsonSerializerSettings());
        }

        [Authorize(Roles = "manager")]
        public IActionResult Orders()
        {
            return View();
        }

        [Authorize(Roles = "manager")]
        public IActionResult ClientSearch()
        {
            return View();
        }

        [Authorize(Roles = "manager")]
        [HttpGet]
        public IEnumerable<Clients> GetClients()
        {
            return db.Clients.ToList();
        }

        [Authorize(Roles = "manager")]
        public IActionResult AccountSearch()
        {
            return View();
        }

        [Authorize(Roles = "manager")]
        [HttpGet]
        public IEnumerable<Account> GetAccounts()
        {
            return db.Account.ToList();
        }

        [Authorize(Roles = "manager")]
        [HttpPost]
        public IActionResult CarAdd([FromBody]Auto auto)
        {
            var updAuto = db.Auto.Where(a => a.VinCode == auto.VinCode).FirstOrDefault();
            if (updAuto != null)
            {
                updAuto.Mileage = auto.Mileage;
                updAuto.RegisterSign = auto.RegisterSign;
                updAuto.EngineСapacity = auto.EngineСapacity;
            }
            else
            {
                db.Auto.Add(auto);
            }
            db.SaveChanges();
            return Ok(auto) ;
        }

        [Authorize(Roles = "manager")]
        [HttpGet]
        public JsonResult SearchId(string search)
        {
            List<Clients> allSearch = db.Clients.Where(x => x.Phone.Contains(search)).Select(x => new Clients
            {
                IdClient = x.IdClient,
                Name = x.Name,
                Surname = x.Surname,
                Patronimyc = x.Patronimyc,
                Phone = x.Phone

            }).ToList();
            return Json(allSearch,new JsonSerializerSettings());
        }

        [Authorize(Roles = "manager")]
        [HttpGet]
        public JsonResult SearchId1(string search)
        {
            List<Account> allSearch = db.Account.Where(x => x.Phone.Contains(search)).Select(x => new Account
            {
                IdUser = x.IdUser,
                Name = x.Name,
                Surname = x.Surname,
                Patronymic = x.Patronymic,
                Phone = x.Phone

            }).ToList();
            return Json(allSearch, new JsonSerializerSettings());
        }

        [Authorize(Roles = "manager")]
        [HttpPost]
        public JsonResult DropDownListMasters()
        {
            List<Masters> data = db.Masters.Select(x => new Masters
            {
                IdMaster = x.IdMaster,
                Name = x.Name,
                Surname = x.Surname,
                Patronymic = x.Patronymic
            }).ToList();
            return Json(data,new JsonSerializerSettings());
        }

        [Authorize(Roles = "manager")]
        [HttpPost]
        public JsonResult ListWorks()
        {
            List<TypeOfWorks> data = db.TypeOfWorks.Select(x => new TypeOfWorks
            {
                JobCode = x.JobCode,
                TypeOfWork = x.TypeOfWork,
                Cost =x.Cost
            }).ToList();
            return Json(data, new JsonSerializerSettings());
        }

        [Authorize(Roles = "manager")]
        [HttpPost]
        public JsonResult SaveOrder(Orders orders)
        {
                orders.OrderCost = 1;
                db.Orders.Add(orders);
            db.SaveChanges();

            var idOrder = orders.Id;
            return Json(idOrder, new JsonSerializerSettings());
        }

        [Authorize(Roles = "manager")]
        [HttpPost]
        public JsonResult UpdOrder(OrdersUpd ordersUpd)
        {
            var upd = db.Orders.Where(a => a.Id == ordersUpd.Id).FirstOrDefault();
            if (upd != null)
            {
                upd.OrderCost = Math.Round(ordersUpd.OrderCost,2);
            }

            db.SaveChanges();

            var idOrder = ordersUpd.Id;
            return Json(idOrder, new JsonSerializerSettings());
        }

        [Authorize(Roles = "manager")]
        public JsonResult SearchWorks(string searchTerm)
        {
            List<TypeOfWorks> allSearch = db.TypeOfWorks.Where(x => x.TypeOfWork.Contains(searchTerm)).Select(x => new TypeOfWorks
            {
                JobCode = x.JobCode,
                TypeOfWork = x.TypeOfWork,
                Cost = x.Cost
            }).ToList();
            return Json(allSearch, new JsonSerializerSettings());
        }

        [Authorize(Roles = "manager")]
        public JsonResult SearchParts(string search)
        {
            var searchParts = (
                from p in db.Parts
                join a in db.AutoToPart on p.Artikul equals a.Artikul
                where a.VinCode.Contains(search)
                select new {
                    artikul = p.Artikul,
                    name = p.Name,
                    cost = p.Cost
                }
                ).ToList();
            return Json( searchParts , new JsonSerializerSettings());
        }

        [Authorize(Roles = "manager")]
        [HttpPost]
        public JsonResult ListParts()
        {
            List<Parts> data = db.Parts.Select(x => new Parts
            {
                Artikul = x.Artikul,
                Name = x.Name,
                Cost = x.Cost
            }).ToList();
            return Json(data, new JsonSerializerSettings());
        }

        [Authorize(Roles = "manager")]
        [HttpPost]
        public JsonResult SaveOrderToWorks(OrdersToWorksView ordersToWorks) 
        {
            foreach (int i in ordersToWorks.JobCode) {

                var toWorks = new OrdersToWorks()
                {
                    IdOrder = ordersToWorks.IdOrder,
                    JobCode = i
                };
                db.OrdersToWorks.Add(toWorks);
            }
            db.SaveChanges();

            var dataWorksOrder = (
                from t in db.TypeOfWorks
                join o in db.OrdersToWorks on t.JobCode equals o.JobCode
                where o.IdOrder == ordersToWorks.IdOrder
                select new
                {
                    JobCode = t.JobCode,
                    TypeOfWork = t.TypeOfWork,
                    Cost = t.Cost
                }
                ).ToList();
            return Json(dataWorksOrder, new JsonSerializerSettings());
        }

        [Authorize(Roles = "manager")]
        [HttpPost]
        public JsonResult SaveOrderToParts(OrdersToPartsView ordersToParts)
        {
            foreach (int i in ordersToParts.Artikul)
            {

                var toParts = new OrdersToParts()
                {
                    IdOrder = ordersToParts.IdOrder,
                    Artikul = i
                };
                db.OrdersToParts.Add(toParts);
            }
            db.SaveChanges();

            var dataPartsOrder = (
                from t in db.OrdersToParts
                join p in db.Parts on t.Artikul equals p.Artikul
                where t.IdOrder == ordersToParts.IdOrder
                select new
                {
                    Artikul = p.Artikul,
                    Name = p.Name,
                    Cost = p.Cost
                }
                ).ToList();

            return Json(dataPartsOrder, new JsonSerializerSettings());
        }

        [Authorize(Roles = "manager")]
        [HttpGet]
        public IActionResult SendEmail()
        {
            return View();
        }

        [Authorize(Roles = "manager")]
        [HttpPost]
        public IActionResult SendEmail(Account account)
        {
            var data = db.Account.OrderBy(e => e.Email);
            foreach (var Emails in data)
            {
                string file = _fileProvider.GetFileInfo("/wwwroot/1.html").PhysicalPath;
                string mailbody = System.IO.File.ReadAllText(file);
                string to = Convert.ToString(Emails.Email);
                string from = "newsportalaspcore@gmail.com";
                MailMessage msg = new MailMessage(from, to);
                msg.Subject = "Акции от ГАРАЖ 24";
                msg.Body = mailbody;
                msg.BodyEncoding = Encoding.UTF8;
                msg.IsBodyHtml = true;
                SmtpClient client = new SmtpClient("smtp.gmail.com", 25);
                NetworkCredential basicCredential = new NetworkCredential("newsportalaspcore@gmail.com", "PASS");
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
        }
    }
}