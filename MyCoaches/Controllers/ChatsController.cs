using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyCoaches.Models;

namespace MyCoaches.Controllers
{
    public class ChatsController : Controller
    {
        private PFEEntities db = new PFEEntities();
        // GET: Chats
        public ActionResult Index()
        {
            if (Session["user"] == null)
            {
                Response.Redirect(Url.Action("Index", "Home"));
                ViewData["msg"] = db.MessageEnvs.ToList();
            }
            int id = 0;
            if (Session["client"] != null)
            {
                id = Convert.ToInt32(Session["client"]);
                var contactcoache = db.Entraineurs.Where(e => e.EnvoyePars.Select(ev => ev.C_idUtilisateur).Contains(id)).ToList();
                int i = contactcoache.First().id;
                ViewData["msg"] = db.MessageEnvs.Where(m => m.EnvoyePars.Select(e => e.Utilisateur.id).Contains(id) && m.EnvoyePars.Select(e => e.C_idEntraineur).Contains(i)).OrderBy(o => o.DateEnv).ToList();
                ViewData["contact"] = contactcoache;
            }
            if (Session["coache"] != null)
            {
                id = Convert.ToInt32(Session["coache"]);
                var contactUser = db.Utilisateurs.Where(e => e.EnvoyePars.Select(ev => ev.C_idEntraineur).Contains(id)).ToList();
                int i = contactUser.First().id;
                ViewData["msg"] = db.MessageEnvs.Where(m => m.EnvoyePars.Select(e => e.Entraineur.id).Contains(id) && m.EnvoyePars.Select(e => e.C_idUtilisateur).Contains(i)).OrderBy(o => o.DateEnv).ToList();
                ViewData["contact"] = contactUser;
            }
            return View();
        }
        public ActionResult getMessages()
        {
            int id = 0;
            int i = Convert.ToInt32(Request.Params.Get("id"));
            if (Session["client"] != null)
            {
                id = Convert.ToInt32(Session["client"]);
                var msgUser = db.MessageEnvs.Where(m => m.EnvoyePars.Select(e => e.Utilisateur.id).Contains(id) && m.EnvoyePars.Select(e => e.C_idEntraineur).Contains(i)).OrderBy(o => o.DateEnv).ToList();
                ViewData["msg"] = msgUser;
            }
            else
            {
                id = Convert.ToInt32(Session["coache"]);
                ViewData["msg"] = db.MessageEnvs.Where(m => m.EnvoyePars.Select(e => e.Entraineur.id).Contains(id) && m.EnvoyePars.Select(e => e.C_idUtilisateur).Contains(i)).OrderBy(o => o.DateEnv).ToList();
            }
            return View("Messages");
        }
        public ActionResult getContact()
        {
            int id = 0;
            if (Session["client"] != null)
            {
                id = Convert.ToInt32(Session["client"]);

                ViewData["contact"] = db.Entraineurs.Where(e => e.EnvoyePars.Select(ev => ev.C_idUtilisateur).Contains(id)).ToList();
            }
            else
            {
                id = Convert.ToInt32(Session["coache"]);
                ViewData["contact"] = db.Utilisateurs.Where(e => e.EnvoyePars.Select(ev => ev.C_idEntraineur).Contains(id)).ToList();
            }
            return View("Contact");
        }
        public  ActionResult searchChat()
        {
            string search = Convert.ToString(Request.Params.Get("search"));
            int id = 0;
            if (Session["client"] != null)
            {
                id = Convert.ToInt32(Session["client"]);
                ViewData["contact"] = db.Entraineurs.Where(e => e.EnvoyePars.Select(ev => ev.C_idUtilisateur).Contains(id) && (e.Personne.Nom.Contains(search) || e.Personne.Prenom.Contains(search) )).ToList(); ;
            }
            else
            {
                id = Convert.ToInt32(Session["coache"]);
                ViewData["contact"] = db.Utilisateurs.Where(e => e.EnvoyePars.Select(ev => ev.C_idEntraineur).Contains(id) && (e.Personne.Nom.Contains(search) || e.Personne.Prenom.Contains(search))).ToList();
            }
            return View("Contact");
        }
        public  JavaScriptResult checkMessage()
        {
            int id = 0;
            int i = int.Parse(Request.Params.Get("id"));
            int nbr_msg = int.Parse(Request.Params.Get("nbr"));
            if (Session["client"] != null)
            {
                id = Convert.ToInt32(Session["client"]);
                var msgUser = db.MessageEnvs.Where(m => m.EnvoyePars.Select(e => e.Utilisateur.id).Contains(id) && m.EnvoyePars.Select(e => e.C_idEntraineur).Contains(i)).OrderBy(o => o.DateEnv).ToList();
                if (msgUser.Select(m => m.id).Count() > nbr_msg)
                {
                    return JavaScript("1");
                }
            }
            else
            {
                id = Convert.ToInt32(Session["coache"]);
                var coache = db.MessageEnvs.Where(m => m.EnvoyePars.Select(e => e.Entraineur.id).Contains(id) && m.EnvoyePars.Select(e => e.C_idUtilisateur).Contains(i)).OrderBy(o => o.DateEnv).ToList();
                if (coache.Select(m => m.id).Count() > nbr_msg)
                {
                    return JavaScript("1");
                }
            }
            return JavaScript("0");
        }
        public JavaScriptResult checkContact()
        {
            int id = 0;
            int i = int.Parse(Request.Params.Get("nbr"));
            if (Session["client"] != null)
            {
                id = Convert.ToInt32(Session["client"]);
                var contactClient = db.Entraineurs.Where(e => e.EnvoyePars.Select(ev => ev.C_idUtilisateur).Contains(id)).ToList();
                if (contactClient.Count() >i)
                {
                    return JavaScript("1");
                }
            }
            else
            {
                id = Convert.ToInt32(Session["coache"]);
                var contactCoache = db.Utilisateurs.Where(e => e.EnvoyePars.Select(ev => ev.C_idEntraineur).Contains(id)).ToList();
                if (contactCoache.Count() > i)
                {
                    return JavaScript("1");
                }
            }
            return JavaScript("0");
        }

        public  void sendMessages()
        {
            int id;
            string message = Convert.ToString(Request.Params.Get("message"));
            int idUser= int.Parse(Request.Params.Get("id"));
            MessageEnv m = new MessageEnv();
            m.DateEnv = DateTime.Now;
            m.TextMessage = message;
            EnvoyePar e = new EnvoyePar();
            if (Session["client"] != null)
            {
                id = Convert.ToInt32(Session["client"]);
                e.MessageEnv= m;
                e.C_idEntraineur = idUser;
                e.C_idUtilisateur = id;
                e.Deriction = false;
            }
            else
            {
                id = Convert.ToInt32(Session["coache"]);
                e.MessageEnv = m;
                e.C_idEntraineur = id;
                e.C_idUtilisateur = idUser;
                e.Deriction = true;
            }
            db.MessageEnvs.Add(m);
            db.EnvoyePars.Add(e);
            db.SaveChanges();
        }
    }
}