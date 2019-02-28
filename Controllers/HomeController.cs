using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VladPromoCodeWebApp.Models;
using Microsoft.AspNetCore.Http;


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
            if (model.Code == "QWERTYUIOP" || model.Code==HttpContext.Session.GetString("Code"))
            {
                return RedirectToAction("PersonData","Home");
            }
            return View("WarningView");
        }
        [HttpGet]
        public IActionResult PersonData()
        {
            return View("PersonDataView");
        }

        [HttpPost]
        public IActionResult PersonData(PersonModel model, string NameSurname,string Email)
        { 
            HttpContext.Session.SetString("NameSurname", model.NameSurname);
            HttpContext.Session.SetString("Email", Email);
           
            return RedirectToAction("Thanks","Home");
        }
        [HttpGet]
        public IActionResult Thanks()
        {
            ViewBag.NameSurname = HttpContext.Session.GetString("NameSurname");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            string promoCode = HttpContext.Session.GetString("Code");
            if (promoCode == null) { 
            PromoCodeGeneration Pass = new PromoCodeGeneration();
            promoCode = Pass.PromoCode();
            HttpContext.Session.SetString("Code", promoCode);
            string pushEmail = HttpContext.Session.GetString("Email") + "\n" + HttpContext.Session.GetString("NameSurname") + "\n" + promoCode;
                Pass.PushEmail(pushEmail);
            }
           

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
    }
}
