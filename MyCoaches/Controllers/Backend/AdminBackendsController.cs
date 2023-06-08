using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyCoaches.Models;
using System.Security.Cryptography;
using System.Text;

namespace MyCoaches.Controllers.Backend
{
    public class AdminBackendsController : Controller
    {
        private PFEEntities db = new PFEEntities();

        // GET: AdminBackends
        public ActionResult Index()
        {
            return View(db.AdminBackends.ToList());
        }

        // GET: AdminBackends/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdminBackend adminBackend = db.AdminBackends.Find(id);
            if (adminBackend == null)
            {
                return HttpNotFound();
            }
            return View(adminBackend);
        }

        // GET: AdminBackends/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminBackends/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Nom,Prenom,email,username,password,gestionDesAdmin")] AdminBackend adminBackend)
        {
            if (ModelState.IsValid)
            {
                db.AdminBackends.Add(adminBackend);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            Cryptography(adminBackend);
            return View(adminBackend);
        }

        // GET: AdminBackends/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdminBackend adminBackend = db.AdminBackends.Find(id);
            if (adminBackend == null)
            {
                return HttpNotFound();
            }
            
            return View(adminBackend);
        }

        // POST: AdminBackends/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Nom,Prenom,email,username,password,gestionDesAdmin")] AdminBackend adminBackend)
        {
            if (ModelState.IsValid)
            {
                db.Entry(adminBackend).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            Cryptography(adminBackend);
            return View(adminBackend);
        }

        // GET: AdminBackends/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdminBackend adminBackend = db.AdminBackends.Find(id);
            if (adminBackend == null)
            {
                return HttpNotFound();
            }
            return View(adminBackend);
        }

        // POST: AdminBackends/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AdminBackend adminBackend = db.AdminBackends.Find(id);
            db.AdminBackends.Remove(adminBackend);
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
        public void Cryptography(AdminBackend admin)
        {
            admin.password = HashString(admin.password);
            db.SaveChanges();
        }
        public string HashString(string text)
        {
            SHA1 sha = new SHA1CryptoServiceProvider();

            byte[] hashedData = sha.ComputeHash(Encoding.Unicode.GetBytes(text));

            StringBuilder stringBuilder = new StringBuilder();

            foreach (byte b in hashedData)
            {
                stringBuilder.Append(String.Format("{0,2:X2}", b));
            }

            return stringBuilder.ToString();
        }
    }
}
