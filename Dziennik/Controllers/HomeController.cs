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
            return View("Index");
        }
        [HttpPost]
        public ActionResult Ocena_Uczen_Przedmiot(int id_u, int id_p, int wartosc)
        {
            return View("Index");
        }
    }
}
