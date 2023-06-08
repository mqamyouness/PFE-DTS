using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyCoaches.Models;

namespace MyCoaches.Controllers
{
    public class AboutController : Controller
    {
        private PFEEntities db = new PFEEntities();
        // GET: About
        public ActionResult Index()
        {
            if (Session["user"] != null)
            {
                Response.Redirect(Url.Action("Home", "Home"));
            }
            ViewData["PeopleSay"] = db.WhatPeopleSays.Where(w => w.show == true).ToList();
            List<int> Numbers = new List<int>();
            Numbers.Add(db.Personnes.Count());
            Numbers.Add(db.Entraineurs.Count());
            Numbers.Add(db.Posts.Count());
            Numbers.Add(db.Utilisateurs.Count());
            ViewBag.Numbers = Numbers;
            return View();
        }
    }
}