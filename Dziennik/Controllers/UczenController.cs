using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using AppDziennik.Models;

namespace AppDziennik.Controllers
{
    public class UczenController : Controller
    {
        public ActionResult Index()
        {
            var db_u = new UczenDataContext();
            return View("Index",db_u.Uczniowie.ToList());
        }

        //
        // GET: /Uczen/Create

        public ActionResult Create()
        {
            return View("Create");
        }

        //
        // POST: /Uczen/Create

        [HttpPost]
        public ActionResult Create(Uczen U)
        {
            var db_u = new UczenDataContext();
            if (ModelState.IsValid)
            {
                db_u.Uczniowie.Add(U);
                db_u.SaveChanges();
                return Redirect("/home/Index");
            }
            return View("Create", U);
        }

        //
        // GET: /Uczen/Edit/5

        public ActionResult Edit(int id_u)
        {
            var db_u = new UczenDataContext();
            return View("Edit",db_u.Uczniowie.Find(id_u));
        }

        //
        // POST: /Uczen/Edit/5

        [HttpPost]
        public ActionResult Edit(Uczen U)
        {
            if (ModelState.IsValid)
            {
                var db_u = new UczenDataContext();
                db_u.Uczniowie.AddOrUpdate(U);
                db_u.SaveChanges();
                return Redirect("/home/Index");
            }
            return View("Edit", U);
        }

        //
        // GET: /Uczen/Delete/5

        public ActionResult Delete(int id_u)
        {
            var db_u = new UczenDataContext();
            var u = db_u.Uczniowie.Find(id_u);
            return View("Delete", u);
        }
        
        //
        // POST: /Uczen/Delete/5

        [HttpPost]
        public ActionResult Delete(int id_u,bool form=true)
        {
            var db_u = new UczenDataContext();
            Uczen U = db_u.Uczniowie.Find(id_u);
            if (ModelState.IsValid)
            {
                db_u.Uczniowie.Remove(U);
                db_u.SaveChanges();
                return Redirect("/home/Index");
            }
            return View("Delete", U);
        }

        public ActionResult Details(int id_u)
        {
            var db_u = new DBDziennik();
            var u = db_u.Uczniowie.Find(id_u);
            return View("Details",u);
        }

        public ActionResult PartialDetails_Przedmioty(int id_u)
        {
            var db_o = new DBDziennik();

            return PartialView("PartialDetails");
        }

        public ActionResult PartialDetails(int id_u)
        {
            var db_o = new DBDziennik();
            var items = db_o.Oceny.Where(m=>m.Id_u == id_u).Join(db_o.Przedmioty.DefaultIfEmpty(), 
                        o=>o.Id_p,
                        p=>p.Id_p,
                        (o,p) => new Ocena_Przedmiot()
                                {
                                    Wartosc = o.Wartosc,
                                    Nazwa = p.Nazwa
                                }).ToList();
            return PartialView("PartialDetails", items);
        }
    }
}
