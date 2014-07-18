using AppDziennik.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dziennik.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View("Index");
        }

        public ActionResult Ocena_Uczen_Przedmiot()
        {
            return View("Ocena_Uczen_Przedmiot");
        }
        [HttpPost]
        public ActionResult Ocena_Uczen_Przedmiot(Ocena O)
        {
            var db_o = new OcenaDataContext();
            if (ModelState.IsValid)
            {
                db_o.Oceny.Add(O);
                db_o.SaveChanges();
                return Redirect("/home/Index");
            }
            return View("Ocena_Uczen_Przedmiot", O);
        }
    }
}
