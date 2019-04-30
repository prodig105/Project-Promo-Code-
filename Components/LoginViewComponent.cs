using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Html;
namespace VladPromoCodeWebApp.Components
{
    public class LoginViewComponent : ViewComponent
    {
        public  IViewComponentResult Invoke()
        {
            string Email="";
            var ID= HttpContext.Session.GetInt32("ID");
            if (ID == 1) Email = HttpContext.Session.GetString("Email");
            else ViewBag.Email = "PromoCode";
            return Content(Email);
        }
        //public IViewComponentResult LoginStatus()
        //{
        //    var ID = HttpContext.Session.GetInt32("ID");
        //    if (ID == 1) ViewBag.Status = "Выход"; else ViewBag.Status = "Войти";
        //    return View("");
        //}
    }

}