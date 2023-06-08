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
    public class AbonnementsController : Controller
    {
        private PFEEntities db = new PFEEntities();

        // GET: Abonnements
        public ActionResult Index()
        {
            var abonnements = db.Abonnements.Include(a => a.Utilisateur).Include(a => a.Choix);
            return View(abonnements.ToList());
        }

        // GET: Abonnements/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Abonnement abonnement = db.Abonnements.Find(id);
            if (abonnement == null)
            {
                return HttpNotFound();
            }
            return View(abonnement);
        }

        // GET: Abonnements/Create
        public ActionResult Create()
        {
            ViewBag.C_idUtilisateur = new SelectList(db.Utilisateurs, "id", "id");
            ViewBag.C_idChoisez = new SelectList(db.Choixes, "id", "Category");
            return View();
        }

        // POST: Abonnements/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "C_idChoisez,C_idUtilisateur,DateDub,DateFin,Peroide,autoReview")] Abonnement abonnement)
        {
            if (ModelState.IsValid)
            {
                db.Abonnements.Add(abonnement);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.C_idUtilisateur = new SelectList(db.Utilisateurs, "id", "id", abonnement.C_idUtilisateur);
            ViewBag.C_idChoisez = new SelectList(db.Choixes, "id", "Category", abonnement.C_idChoisez);
            return View(abonnement);
        }

        // GET: Abonnements/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Abonnement abonnement = db.Abonnements.Find(id);
            if (abonnement == null)
            {
                return HttpNotFound();
            }
            ViewBag.C_idUtilisateur = new SelectList(db.Utilisateurs, "id", "id", abonnement.C_idUtilisateur);
            ViewBag.C_idChoisez = new SelectList(db.Choixes, "id", "Category", abonnement.C_idChoisez);
            return View(abonnement);
        }

        // POST: Abonnements/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "C_idChoisez,C_idUtilisateur,DateDub,DateFin,Peroide,autoReview")] Abonnement abonnement)
        {
            if (ModelState.IsValid)
            {
                db.Entry(abonnement).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.C_idUtilisateur = new SelectList(db.Utilisateurs, "id", "id", abonnement.C_idUtilisateur);
            ViewBag.C_idChoisez = new SelectList(db.Choixes, "id", "Category", abonnement.C_idChoisez);
            return View(abonnement);
        }

        // GET: Abonnements/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Abonnement abonnement = db.Abonnements.Find(id);
            if (abonnement == null)
            {
                return HttpNotFound();
            }
            return View(abonnement);
        }

        // POST: Abonnements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Abonnement abonnement = db.Abonnements.Find(id);
            db.Abonnements.Remove(abonnement);
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
