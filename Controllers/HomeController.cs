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
            if (1 == HttpContext.Session.GetInt32("ID"))
            {
                ViewBag.Email = HttpContext.Session.GetString("Email");
                ViewBag.Name = HttpContext.Session.GetString("Name");
                ViewBag.Surname = HttpContext.Session.GetString("Surname");
                ViewBag.Code = HttpContext.Session.GetString("Code1");
                return View("UserCabinetView");
            }
            return View("StartView");
        }
        [HttpPost]
        public IActionResult Start(StartModel model, string Code)
        {
            model.Code = Code;
            PromoManager emailManager = new PromoManager();
           
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
            HttpContext.Session.SetString("User", Email);

            return RedirectToAction("Thanks", "Home");
        }
        [HttpGet]
        public IActionResult Thanks()
        {
            //User user = new User();
            Promo promo = new Promo();
            PromoManager emailManager = new PromoManager();
            var PromoCode = emailManager.Read();
            //var UserDATA = userManager.Read();
            string NameSurname = HttpContext.Session.GetString("NameSurname");
            string[] FullName = NameSurname.Split(' ');
            ViewBag.NameSurname = NameSurname;
            ViewBag.Name = FullName[0];
            ViewBag.Email = HttpContext.Session.GetString("User");
            string promoCode = HttpContext.Session.GetString("Code");
            if (promoCode == null)
            {
                //for (int i = 0; i < PromoCode.Count; i++)
                //{
                //    if (PromoCode[i].Email1 == HttpContext.Session.GetString("User"))
                //    {

                //    }
                //}

                PromoCodeGeneration Pass = new PromoCodeGeneration();
                promoCode = Pass.PromoCode();
                HttpContext.Session.SetString("Code", promoCode);
                //user.Name = FullName[0];
                //user.Surname = FullName[1];
                promo.SaleCode = promoCode;
                //user.Email1 = HttpContext.Session.GetString("User");
                promo.Email1 = HttpContext.Session.GetString("User");
                //UserDATA.Add(user);
                emailManager.Save(PromoCode);
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
            if (1 == HttpContext.Session.GetInt32("ID"))
            {
                ViewBag.Email = HttpContext.Session.GetString("Email");
                ViewBag.Name = HttpContext.Session.GetString("Name");
                ViewBag.Surname = HttpContext.Session.GetString("Surname");
                ViewBag.Code = HttpContext.Session.GetString("Code1");
                return View("UserCabinetView");
            }
            return View("RecalPromoCodeView");
        }

        [HttpPost]
        public IActionResult RecalPromo(PersonModel model, string Email)
        {
            
            //User email = new User();
            PromoManager emailManager = new PromoManager();
            var list = emailManager.Read();
            model.Email = Email;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Email1 == model.Email) { ViewBag.Code = list[i].SaleCode; return View("ThanksView"); }
            }
            return View("NotFoundPromoView");
        }

        [HttpGet]
        public IActionResult Registration()
        {

            return View("RegistrationView");
        }
        [HttpPost]
        public IActionResult Registration(Autentification model)
        {
            User user = new User();
            UserManager userManager = new UserManager();
            var Base = userManager.Read();
            for (int i = 0; i < Base.Count; i++) if (Base[i].Email1 == model.Email) { ViewBag.Email = Base[i].Email1; return View("ErrorAutentificationView"); }
            user.Email1 = model.Email;
            ViewBag.Name = user.Name = model.Name;
            ViewBag.Surname = user.Surname = model.Surname;
            user.Password = model.Password;
            Base.Add(user);
            userManager.Save(Base);
            ViewBag.Email = model.Email;
            HttpContext.Session.SetString("Email", model.Email);
            HttpContext.Session.SetInt32("ID", 1);
            PromoCodeGeneration Pass = new PromoCodeGeneration();
            string Code = Pass.PromoCode();
            ViewBag.Code = Code;
            Promo promo = new Promo();
            PromoManager emailManager = new PromoManager();
            var PromoCode = emailManager.Read();
            promo.Email1 = model.Email;
            ViewBag.Code = promo.SaleCode = Code;
            emailManager.Save(PromoCode);
            return View("UserCabinetView");
        }

        [HttpGet]
        public IActionResult Autentification()
        {

            return View("AutentView");
        }
        [HttpPost]
        public IActionResult Autentification(Autentification model)
        {
            UserManager userManager = new UserManager();
            PromoManager promoManager = new PromoManager();
            var promoBase = promoManager.Read();
            var Base = userManager.Read();
            for (int i = 0; i < Base.Count; i++)
            {
                if ((model.Email == Base[i].Email1) && (model.Password == Base[i].Password))
                {

                    for (int j = 0; j < promoBase.Count; j++) if (promoBase[j].Email1 == model.Email)
                        {
                            //ViewBag.Email = Base[i].Email1;
                            //ViewBag.Name = Base[i].Name;
                            //ViewBag.Surname = Base[i].Surname;
                            //ViewBag.Code = promoBase[j].SaleCode;
                            HttpContext.Session.SetString("Surname", Base[i].Surname);
                            HttpContext.Session.SetString("Name", Base[i].Name);
                            HttpContext.Session.SetString("Code1", promoBase[j].SaleCode);
                            HttpContext.Session.SetString("Email", model.Email);
                            ViewBag.Email = HttpContext.Session.GetString("Email");
                            ViewBag.Name = HttpContext.Session.GetString("Name");
                            ViewBag.Surname = HttpContext.Session.GetString("Surname");
                            ViewBag.Code = HttpContext.Session.GetString("Code1");

                            HttpContext.Session.SetInt32("ID", 1);
                            return View("UserCabinetView");
                        }

                }
            }

            return View("ErrorAutentView");
        }
    }
}
