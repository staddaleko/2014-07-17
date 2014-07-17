using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using AppDziennik.Models;

namespace Dziennik.Controllers
{
    public class PrzedmiotController : Controller
    {
        public ActionResult Index()
        {
            var db_p = new PrzedmiotDataContext();
            return View("Index",db_p.Przedmioty.ToList());
        }

        public ActionResult Details(int id)
        {
            return View();
        }

     
        public ActionResult Create()
        {
            return View("Create");
        }

        
        [HttpPost]
        public ActionResult Create(Przedmiot P)
        {
            var db_p = new PrzedmiotDataContext();
            if (ModelState.IsValid)
            {
                db_p.Przedmioty.Add(P);
                db_p.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Create", P);
        }

        public ActionResult Edit(int id_p)
        {
            var db_p = new PrzedmiotDataContext();
            return View("Edit", db_p.Przedmioty.Find(id_p));
        }

        [HttpPost]
        public ActionResult Edit(Przedmiot P)
        {
            if (ModelState.IsValid)
            {
                var db_p = new PrzedmiotDataContext();
                db_p.Przedmioty.AddOrUpdate(P);
                db_p.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Edit", P);
        }

        public ActionResult Delete(int id_p)
        {
            var db_p = new PrzedmiotDataContext();
            var p = db_p.Przedmioty.Find(id_p);
            return View("Delete", p);
        }

        [HttpPost]
        public ActionResult Delete(int id_p, bool form=true)
        {
            var db_p = new PrzedmiotDataContext();
            Przedmiot P  = db_p.Przedmioty.Find(id_p);
            if (ModelState.IsValid)
            {
                db_p.Przedmioty.Remove(P);
                db_p.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Delete", P); 
        }
    }
}
