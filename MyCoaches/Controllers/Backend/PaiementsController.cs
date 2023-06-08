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
    public class PaiementsController : Controller
    {
        private PFEEntities db = new PFEEntities();

        // GET: Paiements
        public ActionResult Index()
        {
            var paiements = db.Paiements.Include(p => p.Entraineur);
            return View(paiements.ToList());
        }

        // GET: Paiements/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paiement paiement = db.Paiements.Find(id);
            if (paiement == null)
            {
                return HttpNotFound();
            }
            return View(paiement);
        }

        // GET: Paiements/Create
        public ActionResult Create()
        {
            ViewBag.C_idEntraineur = new SelectList(db.Entraineurs, "id", "id");
            return View();
        }

        // POST: Paiements/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Montant,Datep,C_idEntraineur")] Paiement paiement)
        {
            if (ModelState.IsValid)
            {
                db.Paiements.Add(paiement);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.C_idEntraineur = new SelectList(db.Entraineurs, "id", "id", paiement.C_idEntraineur);
            return View(paiement);
        }

        // GET: Paiements/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paiement paiement = db.Paiements.Find(id);
            if (paiement == null)
            {
                return HttpNotFound();
            }
            ViewBag.C_idEntraineur = new SelectList(db.Entraineurs, "id", "id", paiement.C_idEntraineur);
            return View(paiement);
        }

        // POST: Paiements/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Montant,Datep,C_idEntraineur")] Paiement paiement)
        {
            if (ModelState.IsValid)
            {
                db.Entry(paiement).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.C_idEntraineur = new SelectList(db.Entraineurs, "id", "id", paiement.C_idEntraineur);
            return View(paiement);
        }

        // GET: Paiements/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paiement paiement = db.Paiements.Find(id);
            if (paiement == null)
            {
                return HttpNotFound();
            }
            return View(paiement);
        }

        // POST: Paiements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Paiement paiement = db.Paiements.Find(id);
            db.Paiements.Remove(paiement);
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
