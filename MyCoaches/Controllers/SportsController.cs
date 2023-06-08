using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyCoaches.Models;

namespace MyCoaches.Controllers
{
    public class SportsController : Controller
    {
        private PFEEntities db = new PFEEntities();
        // GET: Sports
        public ActionResult Index()
        {
            if (Session["user"] == null)
            {
                Response.Redirect(Url.Action("Index", "Home"));
            }
            var sport = db.Choixes.ToList();
            ViewData["sports"] = sport;
            return View();
        }

        //Joining

        public ActionResult Join()
        {
            if (Session["user"] == null)
            {
                Response.Redirect(Url.Action("Index", "Home"));
            }
            var sport = db.Choixes.ToList();
            ViewData["sports"] = sport;
            return View("Join");
        }
        public ActionResult filter()
        {
            string search = Convert.ToString(Request.Params.Get("search"));
            string[] tag = new string[4];
            for (int i = 1; i <= 4; i++)
            {
                tag[i - 1] = Convert.ToString(Request.Params.Get("tag" + i.ToString()));
            }
            if (search != "")
            {
                if (tag.Count() == 0 || true)
                {
                    ViewData["sports"] = db.Choixes.Where(c => c.Entraineur.Personne.Nom.Contains(search) || c.Entraineur.Personne.Prenom.Contains(search) || c.textPub.Contains(search)).ToList();
                }
            }
            else
            {
                ViewData["sports"] = db.Choixes.ToList();
            }
            return View("choix");
        }

    }
}