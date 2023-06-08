using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyCoaches.Models;
using System.Web.Security;

namespace MyCoaches.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        private PFEEntities db = new PFEEntities();
        // GET: Home
        public ActionResult Index()
        {
            if (Session["user"] != null)
            {
                Response.Redirect(Url.Action("Home", "Home"));
            }
            ViewData["Posts"] = db.Posts.Where(p => p.id < 4).ToList();
            ViewData["Coaches"] = db.Entraineurs.Where(e => e.id < 5).ToList();
            ViewData["PeopleSay"] = db.WhatPeopleSays.Where(w => w.show == true).ToList();
            List<int> Numbers = new List<int>();
            Numbers.Add(db.Personnes.Count());
            Numbers.Add(db.Entraineurs.Count());
            Numbers.Add(db.Posts.Count());
            Numbers.Add(db.Utilisateurs.Count());
            ViewBag.Numbers = Numbers;

            if (Session["user"] != null)
            {
                Redirect("Home");
            }
            return View();
        }
        // GET: Home
        public ActionResult Home()
        {
            ViewData["Posts"] = db.Posts.Where(p => p.id < 4).ToList();
            if (Session["user"] == null)
            {
                Response.Redirect(Url.Action("Index", "Home"));
            }
            else
            {
                if (Session["client"] != null)
                {
                    int idClinet = Convert.ToInt32(Session["client"].ToString());
                    var postsClient = db.Posts.Where(p => p.Choix.Abonnements.Select(a => a.C_idUtilisateur).Contains(idClinet));
                    ViewData["Posts"] = postsClient.ToList();
                }
                if (Session["coache"] != null)
                {
                    int idCoache = Convert.ToInt32(Session["coache"].ToString());
                    var posts = db.Posts.Where(p => p.Choix.C_idEntraineur == idCoache);
                    ViewData["Posts"] = posts.ToList();
                }
            }
            ViewData["Coaches"] = db.Entraineurs.Where(e => e.id < 5).ToList();
            ViewData["Choix"] = db.Choixes.Where(c=> c.Entraineur.Evaluation == 5).Take(3).ToList();
            ViewData["PeopleSay"] = db.WhatPeopleSays.Where(w => w.show == true).ToList();
            ViewData["linkSc"] = db.reseauxSociauxes.ToList();
            if (Session["user"] == null)
            {
                Redirect("index");
            }
            return View();
        }
        public ActionResult Logout()
        {
            Session["user"] = null;
            Session["client"] = null;
            Session["coache"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}