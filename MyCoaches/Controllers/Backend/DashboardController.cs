using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyCoaches.Models;

namespace MyCoaches.Controllers.Backend
{
    public class DashboardController : Controller
    {
        private PFEEntities db = new PFEEntities();
        // GET: Dashboard
        public ActionResult Index()
        {
            int personne = db.Personnes.Count();
            int  men = db.Personnes.Where(p => p.Sexe == "men").Count();
            int women = db.Personnes.Where(p => p.Sexe == "women").Count();
            List<int> NumbersDashboardSrcl1 = new List<int>();
            NumbersDashboardSrcl1.Add((men* 100 ) / personne);
            NumbersDashboardSrcl1.Add((women * 100) / personne);
            List<int> NumbersDashboardSrcl2 = new List<int>();
            NumbersDashboardSrcl2.Add((db.Utilisateurs.Count() * 100) / (db.Utilisateurs.Count() + db.Entraineurs.Count() ));
            NumbersDashboardSrcl2.Add((db.Entraineurs.Count()  * 100) / (db.Utilisateurs.Count() + db.Entraineurs.Count() ));
            List<int> NumbersDashboardSrcl3 = percentage();
            ViewBag.NumbersDashboardSrcl1 = NumbersDashboardSrcl1;
            ViewBag.NumbersDashboardSrcl2 = NumbersDashboardSrcl2;
            ViewBag.NumbersDashboardSrcl3 = NumbersDashboardSrcl3;

            return View();
        }
        public List<int> percentage()
        {
            List<int> list = new List<int>();
            int p1 = 0, p2 = 0, p3 = 0, p4 = 0;
            foreach (var item in db.Personnes)
            {
                int age = AgeYears((DateTime)item.DateNc);
                if ( age > 12 && age <= 18)
                    p1++;
                if (age > 18 && age <= 24)
                    p2++;
                if (age > 24 && age <= 32)
                    p3++;
                if (age > 32)
                    p4++;
            }
            int somme = db.Personnes.Count();
            list.Add((p1 * 100) / somme);
            list.Add((p2 * 100) / somme);
            list.Add((p3 * 100) / somme);
            list.Add((p4 * 100) / somme);
            return list;
        }
        public int AgeYears(DateTime date)
        {
            int now = int.Parse(DateTime.Now.ToString("yyyyMMdd"));
            int dob = int.Parse(date.ToString("yyyyMMdd")); 
            return (now - dob) / 10000;
        }
    }
}