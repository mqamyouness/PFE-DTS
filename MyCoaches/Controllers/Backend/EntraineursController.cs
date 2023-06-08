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
    public class EntraineursController : Controller
    {
        private PFEEntities db = new PFEEntities();

        // GET: Entraineurs
        public ActionResult Index()
        {
            var entraineur = db.Entraineurs.Include(e => e.Personne);
            return View(entraineur.ToList());
        }

        // GET: Entraineurs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entraineur entraineur = db.Entraineurs.Find(id);
            if (entraineur == null)
            {
                return HttpNotFound();
            }
            return View(entraineur);
        }

        // GET: Entraineurs/Create
        public ActionResult Create()
        {
            ViewBag.C_idPersonne = new SelectList(db.Personnes, "id", "Nom");
            return View();
        }

        // POST: Entraineurs/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Evaluation,Experience,DateDub,C_idPersonne")] Entraineur entraineur)
        {
            if (ModelState.IsValid)
            {
                db.Entraineurs.Add(entraineur);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.C_idPersonne = new SelectList(db.Personnes, "id", "Nom", entraineur.C_idPersonne);
            return View(entraineur);
        }

        // GET: Entraineurs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entraineur entraineur = db.Entraineurs.Find(id);
            if (entraineur == null)
            {
                return HttpNotFound();
            }
            ViewBag.C_idPersonne = new SelectList(db.Personnes, "id", "Nom", entraineur.C_idPersonne);
            return View(entraineur);
        }

        // POST: Entraineurs/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Evaluation,Experience,DateDub,C_idPersonne")] Entraineur entraineur)
        {
            if (ModelState.IsValid)
            {
                db.Entry(entraineur).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.C_idPersonne = new SelectList(db.Personnes, "id", "Nom", entraineur.C_idPersonne);
            return View(entraineur);
        }

        // GET: Entraineurs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entraineur entraineur = db.Entraineurs.Find(id);
            if (entraineur == null)
            {
                return HttpNotFound();
            }
            return View(entraineur);
        }

        // POST: Entraineurs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Entraineur entraineur = db.Entraineurs.Find(id);
            db.Entraineurs.Remove(entraineur);
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
