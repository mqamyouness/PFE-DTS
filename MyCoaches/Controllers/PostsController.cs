using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyCoaches.Models;
namespace MyCoaches.Controllers
{
    public class PostsController : Controller
    {
        private PFEEntities db = new PFEEntities();
        // GET: Posts
        public ActionResult Index()
        {
            if (Session["user"] == null)
            {
                Response.Redirect(Url.Action("Index", "Home"));
            }
            if (Session["user"] != null)
            {
                 if (Session["client"] != null)
                 {
                     int idClinet = Convert.ToInt32(Session["client"].ToString());
                     var postsClient = db.Posts.Where(p => p.Choix.Abonnements.Select(a => a.C_idUtilisateur).Contains( idClinet));
                     ViewData["Posts"] = postsClient.ToList();
                 }
                 if (Session["coache"] != null)
                 {
                     int idCoache = Convert.ToInt32(Session["coache"].ToString());
                     var posts = db.Posts.Where(p => p.Choix.C_idEntraineur == idCoache);
                     ViewData["Posts"] = posts.ToList();
                 }
             }
             
            return View();
        }
        public ActionResult DetailsPost(int id)
        {
            if (Session["user"] == null)
            {
                Response.Redirect(Url.Action("Index", "Home"));
            }
            if (Session["client"] != null)
            {
                int idClinet = Convert.ToInt32(Session["client"].ToString());
                var postsClient = db.Posts.Where(p => p.Choix.Abonnements.Select(a => a.C_idUtilisateur).Contains(idClinet));
                if (!postsClient.Select(pc => pc.id).Contains(id))
                {
                    Response.Redirect(Url.Action("Index", "Posts"));
                }
                var minPostU = db.Posts.Where(p => p.Choix.Abonnements.Select(a => a.C_idUtilisateur).Contains(idClinet));
                ViewData["minPost"] = minPostU.ToList();
            }
            else
            {
                int idCoache = Convert.ToInt32(Session["coache"].ToString());
                var minPostE = db.Posts.Where(p => p.Choix.C_idEntraineur == idCoache);
                ViewData["minPost"] = minPostE.ToList();
            }
            var post = db.Posts.Where(p => p.id == id).ToList();
            var linkpost = db.LinkPosts.Where(lp => lp.Post.id == id).ToList();
            var personnescmt = from pc in db.Personnes join cmt in db.Commentaires on pc.id equals cmt.Personne.id where cmt.Post.id == id select pc;
            var personneCoache = (from p in db.Personnes join e in db.Entraineurs on p equals e.Personne join c in db.Choixes on e.id equals c.Entraineur.id join pt in db.Posts on c.id equals pt.Choix.id where pt.id == id select p).ToList();
            var commnt = db.Commentaires.Where(cm => cm.Post.id == id).ToList();
            var reponse = db.Reponses.Where(r => r.Commentaire.Post.id == id).ToList();
            ViewData["post"] = post;
            ViewData["linkpost"] = linkpost;
            ViewData["personnescmt"] = personnescmt.ToList();
            ViewData["personneCoach"] = personneCoache.ToList();
            ViewData["commentaire"] = commnt;
            ViewData["reponse"] = reponse;
            ViewData["idUser"] = Convert.ToInt32(Session["client"]);
            return View();
        }
        // GET Comment
        public ActionResult getComment()
        {
            int id = Convert.ToInt32(Request.Params.Get("id"));
            var commnt = db.Commentaires.Where(cm => cm.Post.id == id).ToList();
            var reponse = db.Reponses.Where(r => r.Commentaire.Post.id == id).ToList();
            ViewData["commentaire"] = commnt;
            ViewData["reponse"] = reponse;
            return View("Comments");
        }
        // GET Evaluation

        public ActionResult getEvaluation()
        {
            int id = Convert.ToInt32(Request.Params.Get("id"));
            var post = db.Posts.Where(p => p.id == id).ToList();
            var commnt = db.Commentaires.Where(cm => cm.Post.id == id).ToList();
            var reponse = db.Reponses.Where(r => r.Commentaire.Post.id == id).ToList();
            ViewData["commentaire"] = commnt;
            ViewData["reponse"] = reponse;
            ViewData["post"] = post;
            ViewData["idUser"] = Convert.ToInt32(Session["client"]);
            return View("Evaluation");
        }
        // SET Comment

        public void addComment()
        {
            string comment = Convert.ToString(Request.Params.Get("message"));
            int id = Convert.ToInt32(Request.Params.Get("idPost"));
            Commentaire c = new Commentaire();
            c.C_idPersonne = Convert.ToInt32(Session["user"]);
            c.TextComnt = comment;
            c.DateComnt = DateTime.Now;
            c.C_idPost = id;
            db.Commentaires.Add(c);
            db.SaveChanges();
        }
        // SET repondes

        public void addRepondes()
        {
            int id = Convert.ToInt32(Request.Params.Get("id"));
            int personne = Convert.ToInt32(Session["user"]);
            string repond = Convert.ToString(Request.Params.Get("message"));
            string forUser = Convert.ToString(Request.Params.Get("foruser"));
            Reponse r = new Reponse();
            r.TextRpnd = repond;
            r.dateRpnd = DateTime.Now;
            r.Personne = db.Personnes.Where(p => p.id == personne).First();
            r.Commentaire =db.Commentaires.Where(c => c.id == id).First();
            db.Reponses.Add(r);
            db.SaveChanges();

        }
        // SET Evaluation

        public void Like()
        {
            int id = Convert.ToInt32(Request.Params.Get("id"));
            int personne = Convert.ToInt32(Session["user"]);
            Post postObject = db.Posts.Where(p => p.id == id).First();
            Evaluation evaluation;
            if (postObject.Evaluations.Where(p => p.C_idPersonne == personne).Count()>0)
            {
                evaluation = postObject.Evaluations.Where(p => p.C_idPersonne == personne).First();
                if (evaluation.Evaluation1 != true)
                    evaluation.Evaluation1 = true;
                else
                    evaluation.Evaluation1 = null;
                db.SaveChanges();
            }
            else
            {
                evaluation = new Evaluation();
                evaluation.C_idPost = id;
                evaluation.C_idPersonne = personne;
                evaluation.Evaluation1 = true;
                db.Evaluations.Add(evaluation);
                db.SaveChanges();
            }
        }
        public void Dislike()
        {
            int id = Convert.ToInt32(Request.Params.Get("id"));
            int personne = Convert.ToInt32(Session["user"]);
            Post postObject = db.Posts.Where(p => p.id == id).First();
            Evaluation evaluation;
            if (postObject.Evaluations.Where(p => p.C_idPersonne == personne).Count() > 0)
            {
                evaluation = postObject.Evaluations.Where(p => p.C_idPersonne == personne).First();
                if (evaluation.Evaluation1 != false)
                    evaluation.Evaluation1 = false;
                else
                    evaluation.Evaluation1 = null;
                db.SaveChanges();
            }
            else
            {
                evaluation = new Evaluation();
                evaluation.C_idPost = id;
                evaluation.C_idPersonne = personne;
                evaluation.Evaluation1 = false;
                db.Evaluations.Add(evaluation);
                db.SaveChanges();
            }
        }
        public ActionResult getminPost()
        {
            string search = Convert.ToString(Request.Params.Get("search"));
            if (search != "")
            {
                if (Session["client"] != null)
                {
                    int idClinet = Convert.ToInt32(Session["client"].ToString());
                    var minPostU = db.Posts.Where(p => p.Titer.Contains(search) && p.Choix.Abonnements.Select(a => a.C_idUtilisateur).Contains(idClinet));
                    ViewData["minPost"] = minPostU.ToList();

                }
                else
                {
                    int idCoache = Convert.ToInt32(Session["coache"].ToString());
                    var minPostE = db.Posts.Where(p => p.Titer.Contains(search) && p.Choix.C_idEntraineur == idCoache);
                    ViewData["minPost"] = minPostE.ToList();
                }
            }
            else
            {
                if (Session["client"] != null)
                {
                    int idClinet = Convert.ToInt32(Session["client"].ToString());
                    var minPostU = db.Posts.Where(p => p.Choix.Abonnements.Select(a => a.C_idUtilisateur).Contains(idClinet));
                    ViewData["minPost"] = minPostU.ToList();

                }
                else
                {
                    int idCoache = Convert.ToInt32(Session["coache"].ToString());
                    var minPostE = db.Posts.Where(p => p.Choix.C_idEntraineur == idCoache);
                    ViewData["minPost"] = minPostE.ToList();
                }
            }

            return View("minPost");
        }

    }
}