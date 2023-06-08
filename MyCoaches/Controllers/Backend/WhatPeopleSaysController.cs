using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyCoaches.Models;

namespace MyCoaches.Controllers.Backend
{
    public class WhatPeopleSaysController : Controller
    {
        private PFEEntities db = new PFEEntities();

        // GET: WhatPeopleSays
        public ActionResult Index()
        {
            var whatPeopleSays = db.WhatPeopleSays.Include(w => w.Personne);
            return View(whatPeopleSays.ToList());
        }

        // GET: WhatPeopleSays/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WhatPeopleSay whatPeopleSay = db.WhatPeopleSays.Find(id);
            if (whatPeopleSay == null)
            {
                return HttpNotFound();
            }
            return View(whatPeopleSay);
        }

        // GET: WhatPeopleSays/Create
        public ActionResult Create()
        {
            ViewBag.C_idPesonne = new SelectList(db.Personnes, "id", "Nom");
            return View();
        }

        // POST: WhatPeopleSays/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,TextSay,C_idPesonne,show")] WhatPeopleSay whatPeopleSay)
        {
            if (ModelState.IsValid)
            {
                db.WhatPeopleSays.Add(whatPeopleSay);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.C_idPesonne = new SelectList(db.Personnes, "id", "Nom", whatPeopleSay.C_idPesonne);
            return View(whatPeopleSay);
        }

        // GET: WhatPeopleSays/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WhatPeopleSay whatPeopleSay = db.WhatPeopleSays.Find(id);
            if (whatPeopleSay == null)
            {
                return HttpNotFound();
            }
            ViewBag.C_idPesonne = new SelectList(db.Personnes, "id", "Nom", whatPeopleSay.C_idPesonne);
            return View(whatPeopleSay);
        }

        // POST: WhatPeopleSays/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,TextSay,C_idPesonne,show")] WhatPeopleSay whatPeopleSay)
        {
            if (ModelState.IsValid)
            {
                db.Entry(whatPeopleSay).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.C_idPesonne = new SelectList(db.Personnes, "id", "Nom", whatPeopleSay.C_idPesonne);
            return View(whatPeopleSay);
        }

        // GET: WhatPeopleSays/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WhatPeopleSay whatPeopleSay = db.WhatPeopleSays.Find(id);
            if (whatPeopleSay == null)
            {
                return HttpNotFound();
            }
            return View(whatPeopleSay);
        }

        // POST: WhatPeopleSays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WhatPeopleSay whatPeopleSay = db.WhatPeopleSays.Find(id);
            db.WhatPeopleSays.Remove(whatPeopleSay);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
