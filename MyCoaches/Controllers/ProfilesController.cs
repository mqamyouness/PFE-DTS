using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyCoaches.Models;

namespace MyCoaches.Controllers
{
    [HandleError]
    public class ProfilesController : Controller
    {
        private PFEEntities db = new  PFEEntities();
        // GET: Profiles
        public ActionResult Index()
        {
            if (Session["coache"] != null)
            {
                int id = Convert.ToInt32(Session["coache"]);
                var user = from u in db.Entraineurs where u.id == id select u;
                int? pr = user.First().C_idPersonne;
                ViewData["coache"] = user.ToList();
                ViewData["LinkCrtf"] = db.LinkCertifications.Where(l => l.C_idEntraineur == id).ToList();
                ViewData["linkSc"] = db.reseauxSociauxes.Where(r => r.Personne.id == pr).ToList();
                ViewData["Posts"] = db.Posts.Where(p => p.Choix.C_idEntraineur == id).ToList();
                ViewData["sports"] = db.Choixes.Where(c => c.C_idEntraineur == id).ToList();
                ViewData["minPost"] = db.Posts.Where(p => p.Choix.Abonnements.Select(a => a.C_idUtilisateur).Contains(id)).ToList();
                ViewBag.button = true;
                return View("Coache");
            }
            else if (Session["client"] != null)
            {
                int id = Convert.ToInt32(Session["client"]);
                var client = db.Utilisateurs.Where(u => u.id == id);
                int? pr = client.First().C_idPersonne;
                ViewData["client"] = client.ToList();
                ViewData["linkSc"] = db.reseauxSociauxes.Where(r => r.C_idPersonne == pr).ToList();
                return View("client");
            }

            return View();
        }
        
        //GET  USER
        public ActionResult client()
        {
            int id = Convert.ToInt32(Request.Params.Get("client"));
            if (id > 0)
            {
                var client = db.Utilisateurs.Where(u => u.id == id);
                int? pr = client.First().C_idPersonne;
                ViewData["client"] = client.ToList();
                ViewData["linkSc"] = db.reseauxSociauxes.Where(r => r.Personne.Utilisateurs == db.Utilisateurs.Where(u => u.id == id).First()).ToList();
                return View("client");
            }
            return View("Index");
        }
        // GET COACHE
        public ActionResult Coache()
        {
            int id = Convert.ToInt32(Request.Params.Get("coache"));
            if(id >0)
            {
                var user = from u in db.Entraineurs where u.id == id select u;
                int? pr = user.First().C_idPersonne;
                ViewData["coache"] = user.ToList();
                ViewData["LinkCrtf"] = db.LinkCertifications.Where(l => l.C_idEntraineur == id).ToList();
                ViewData["linkSc"] = db.reseauxSociauxes.Where(r => r.Personne.id == pr).ToList();
                ViewData["Posts"] = db.Posts.Where(p => p.Choix.C_idEntraineur == id).ToList();
                ViewData["minPost"] = db.Posts.Where(p => p.Choix.Abonnements.Select(a => a.C_idUtilisateur).Contains(id)).ToList();
                ViewData["sports"] = db.Choixes.Where(c => c.C_idEntraineur == id).ToList();
                return View("Coache");
            }
            return RedirectToAction("PageNotFound", "Error");
        }    
        
        public ActionResult updateProfileUser()
        {
            if (Session["client"] == null)
            {
                return RedirectToAction("Index", "Profiles");
            }
            int id = Convert.ToInt32(Session["user"]);
            ViewData["personne"] = db.Personnes.Where(p => p.id == id).ToList();
            return View();
        }

        public ActionResult updateProfileCoache()
        {
            if (Session["coache"] == null)
            {
                return RedirectToAction("Index", "Profiles");
            }
            int id = Convert.ToInt32(Session["user"]);
            ViewData["coache"] = db.Personnes.Where(p => p.id == id).ToList();
            return View("updateProfileCoache");
        }
    }
}