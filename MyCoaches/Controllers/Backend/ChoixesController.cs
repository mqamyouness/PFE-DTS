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
    public class ChoixesController : Controller
    {
        private PFEEntities db = new PFEEntities();

        // GET: Choixes
        public ActionResult Index()
        {
            var choixes = db.Choixes.Include(c => c.Entraineur).Include(c => c.Sport);
            return View(choixes.ToList());
        }

        // GET: Choixes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Choix choix = db.Choixes.Find(id);
            if (choix == null)
            {
                return HttpNotFound();
            }
            return View(choix);
        }

        // GET: Choixes/Create
        public ActionResult Create()
        {
            ViewBag.C_idEntraineur = new SelectList(db.Entraineurs, "id", "id");
            ViewBag.C_idSport = new SelectList(db.Sports, "id", "Libelle");
            return View();
        }

        // POST: Choixes/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,C_idEntraineur,C_idSport,Category,AbonnemetPrix,textPub,img,TypeEntrainment")] Choix choix)
        {
            if (ModelState.IsValid)
            {
                db.Choixes.Add(choix);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.C_idEntraineur = new SelectList(db.Entraineurs, "id", "id", choix.C_idEntraineur);
            ViewBag.C_idSport = new SelectList(db.Sports, "id", "Libelle", choix.C_idSport);
            return View(choix);
        }

        // GET: Choixes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Choix choix = db.Choixes.Find(id);
            if (choix == null)
            {
                return HttpNotFound();
            }
            ViewBag.C_idEntraineur = new SelectList(db.Entraineurs, "id", "id", choix.C_idEntraineur);
            ViewBag.C_idSport = new SelectList(db.Sports, "id", "Libelle", choix.C_idSport);
            return View(choix);
        }

        // POST: Choixes/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,C_idEntraineur,C_idSport,Category,AbonnemetPrix,textPub,img,TypeEntrainment")] Choix choix)
        {
            if (ModelState.IsValid)
            {
                db.Entry(choix).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.C_idEntraineur = new SelectList(db.Entraineurs, "id", "id", choix.C_idEntraineur);
            ViewBag.C_idSport = new SelectList(db.Sports, "id", "Libelle", choix.C_idSport);
            return View(choix);
        }

        // GET: Choixes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Choix choix = db.Choixes.Find(id);
            if (choix == null)
            {
                return HttpNotFound();
            }
            return View(choix);
        }

        // POST: Choixes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Choix choix = db.Choixes.Find(id);
            db.Choixes.Remove(choix);
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
