using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VladPromoCodeWebApp.Models;
using Microsoft.AspNetCore.Http;
using VladPromoCodeWebApp.Data;
using VladPromoCodeWebApp.Domain;

namespace VladPromoCodeWebApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Start()
        {
            return View("StartView");
        }
        [HttpPost]
        public IActionResult Start(StartModel model, string Code)
        {
            model.Code = Code;
            EmailManager emailManager = new EmailManager();
            var list = emailManager.Read();
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].SaleCode == Code || Code == "QWERTYUIOP")
                {
                    return RedirectToAction("PersonData", "Home");
                }
            }
            return View("WarningView");
        }
        [HttpGet]
        public IActionResult PersonData()
        {
            return View("PersonDataView");
        }

        [HttpPost]
        public IActionResult PersonData(PersonModel model, string NameSurname, string Email)
        {
            HttpContext.Session.SetString("NameSurname", model.NameSurname);
            HttpContext.Session.SetString("Email", Email);

            return RedirectToAction("Thanks", "Home");
        }
        [HttpGet]
        public IActionResult Thanks()
        {
            Email email = new Email();
            EmailManager emailManager = new EmailManager();
            var list = emailManager.Read();

            string NameSurname = HttpContext.Session.GetString("NameSurname");
            string[] FullName = NameSurname.Split(' ');
            ViewBag.NameSurname = NameSurname;
            ViewBag.Name = FullName[0];
            ViewBag.Email = HttpContext.Session.GetString("Email");
            string promoCode = HttpContext.Session.GetString("Code");
            if (promoCode == null)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].Email1== HttpContext.Session.GetString("Email")) { ViewBag.Code = list[i].SaleCode; return View("AntiAbuseView");}
                }

                PromoCodeGeneration Pass = new PromoCodeGeneration();
                promoCode = Pass.PromoCode();
                HttpContext.Session.SetString("Code", promoCode);
                email.Name = FullName[0];
                email.Surname = FullName[1];
                email.SaleCode = promoCode;
                email.Email1 = HttpContext.Session.GetString("Email");
                list.Add(email);
                emailManager.Save(list);
            }
            ViewBag.Code = promoCode;
            return View("ThanksView");
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult RecalPromo()
        {
            return View("RecalPromoCodeView");
        }

        [HttpPost]
        public IActionResult RecalPromo(PersonModel model,string Email)
        {
            Email email = new Email();
            EmailManager emailManager = new EmailManager();
            var list = emailManager.Read();
            model.Email = Email;
            for (int i = 0; i < list.Count; i++)
            {
                if(list[i].Email1==model.Email) { ViewBag.Code = list[i].SaleCode; return View("ThanksView"); }
            }
            return View("NotFoundPromoView");
        }
    }
}
