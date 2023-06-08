using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyCoaches.Models;

namespace MyCoaches.Controllers
{
    public class CoachesController : Controller
    {
        private PFEEntities db = new PFEEntities();

        // GET: Coaches
        public ActionResult Index()
        {
            if (Session["user"] == null)
            {
                Response.Redirect(Url.Action("Index", "Home"));
            }
            if (Request.QueryString["search"] != null)
            {
                string search = Convert.ToString(Request.Params.Get("search"));

                if (search != "")
                {
                    var coaches_1 = db.Entraineurs.Where(e => e.Personne.Nom.Contains(search) || e.Personne.Prenom.Contains(search));
                    var choix_1 = db.Choixes.ToList();
                    ViewData["coaches"] = coaches_1.ToList();
                    ViewData["choix"] = choix_1;
                    ViewData["linkSc"] = db.reseauxSociauxes.ToList();
                    return View();
                }
                else
                {
                    var coaches_1 = db.Entraineurs.ToList();
                    var choix_1 = db.Choixes.ToList();
                    ViewData["coaches"] = coaches_1;
                    ViewData["choix"] = choix_1;
                    ViewData["linkSc"] = db.reseauxSociauxes.ToList();
                    return View();
                }
            }
            var coaches = db.Entraineurs.ToList();
            var choix = db.Choixes.ToList();
            ViewData["coaches"] = coaches;
            ViewData["choix"] = choix;
            ViewData["linkSc"] = db.reseauxSociauxes.ToList();
            return View();
        }
        public ActionResult filter()
        {
            string search = Convert.ToString(Request.Params.Get("search"));
            string[] tag = new string[4];
            for (int i = 1; i <= 4; i++)
            {
                tag[i-1] = Convert.ToString(Request.Params.Get("tag"+i.ToString()));
            }
            if (search != "")
            {
                if (tag.Count() == 0 || true)
                {
                    ViewData["coaches"] = db.Entraineurs.Where(e => e.Personne.Nom.Contains(search) || e.Personne.Prenom.Contains(search)).ToList();
                    ViewData["choix"] = db.Choixes.ToList();
                }
            }
            else
            {
                ViewData["coaches"] = db.Entraineurs.ToList();
                ViewData["choix"] = db.Choixes.ToList();
            }
            ViewData["linkSc"] = db.reseauxSociauxes.ToList();
            return View("coaches");
        }

    }
}