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
    public class LinkPostsController : Controller
    {
        private PFEEntities db = new PFEEntities();

        // GET: LinkPosts
        public ActionResult Index()
        {
            var linkPost = db.LinkPosts.Include(l => l.Post);
            return View(linkPost.ToList());
        }

        // GET: LinkPosts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LinkPost linkPost = db.LinkPosts.Find(id);
            if (linkPost == null)
            {
                return HttpNotFound();
            }
            return View(linkPost);
        }

        // GET: LinkPosts/Create
        public ActionResult Create()
        {
            ViewBag.C_idPost = new SelectList(db.Posts, "id", "TextPost");
            return View();
        }

        // POST: LinkPosts/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Link,TextPost,C_idPost")] LinkPost linkPost)
        {
            if (ModelState.IsValid)
            {
                db.LinkPosts.Add(linkPost);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.C_idPost = new SelectList(db.Posts, "id", "TextPost", linkPost.C_idPost);
            return View(linkPost);
        }

        // GET: LinkPosts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LinkPost linkPost = db.LinkPosts.Find(id);
            if (linkPost == null)
            {
                return HttpNotFound();
            }
            ViewBag.C_idPost = new SelectList(db.Posts, "id", "TextPost", linkPost.C_idPost);
            return View(linkPost);
        }

        // POST: LinkPosts/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Link,TextPost,C_idPost")] LinkPost linkPost)
        {
            if (ModelState.IsValid)
            {
                db.Entry(linkPost).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.C_idPost = new SelectList(db.Posts, "id", "TextPost", linkPost.C_idPost);
            return View(linkPost);
        }

        // GET: LinkPosts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LinkPost linkPost = db.LinkPosts.Find(id);
            if (linkPost == null)
            {
                return HttpNotFound();
            }
            return View(linkPost);
        }

        // POST: LinkPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LinkPost linkPost = db.LinkPosts.Find(id);
            db.LinkPosts.Remove(linkPost);
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
