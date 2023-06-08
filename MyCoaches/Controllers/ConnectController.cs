using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyCoaches.Models;
using System.Security.Cryptography;
using System.Text;

namespace MyCoaches.Controllers
{
    public class ConnectController : Controller
    {
        PFEEntities db = new PFEEntities();
        // GET: Connect
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult confirmed()
        {
            if (Request.QueryString["TeXtVaLiDatIonFoRyOuUsErFoRvAlIdCoMpt"] == null)
            {
                return View("Index");
            }
            string dateHs = Convert.ToString(Request.Params.Get("TeXtVaLiDatIonFoRyOuUsErFoRvAlIdCoMpt"));
            var user = db.Personnes.Where(p =>  p.TextValidation == dateHs).ToList();
            if (user.Count() == 0)
            {
                return View("Index");
            }
            Personne prs = user.First();
            prs.ValidationChomp = true;
            db.SaveChanges();
            ViewData["personne"] = user;
            return View();
        }
        //login
        [HttpPost]
        public ActionResult Login()
        {
            string username = Request.Params.Get("username1");
            string password1 = Request.Params.Get("password1");
            var user = db.Personnes.Where(u => (u.Email == username || u.Username == username));
            if ( user.Count()>0)
            {
                Personne p = new Personne();
                p = user.First();            
                if (p.MotsdePass == HashString(password1))
                {
                    Session["user"] = user.First().id;
                    if (p.Nom != null)
                    {
                        var e = db.Entraineurs.Where(ep => ep.Personne.id == p.id);
                        if (e.Count() > 0)
                        {
                            Session["coache"] = e.First().id;
                        }
                        else
                        {
                            var c = db.Utilisateurs.Where(ep => ep.Personne.id == p.id);
                            Session["client"] = c.First().id;
                            Response.Write(c.First().id);
                        }
                        return RedirectToAction("Home", "Home");
                    }
                    else
                    {
                        Session["newuser"] = 0;
                        return RedirectToAction("Index", "Connect");
                    }
                }
                // else add sweet alert 
            }
            // else add sweet alert 
            return RedirectToAction("Index", "Connect");
        }
        //Singup
        [HttpPost]
        public ActionResult Singup()
        {
            string email2 = Request.Params.Get("email2");
            string username2 = Request.Params.Get("username2");
            string password2 = Request.Params.Get("password2");
            string password22 = Request.Params.Get("password22");
            var user = db.Personnes.Where(u => u.Email == email2);
            if (user.Count() == 0)
            {
                user = db.Personnes.Where(u => u.Username == username2);
                if (user.Count() == 0)
                {
                    if (password2 == password22)
                    {  
                        Personne p = new Personne();
                        p.Email = email2;
                        p.Username = username2;
                        p.MotsdePass = HashString(password2);
                        p.TextValidation = HashString(DateTime.Now.ToLongDateString());
                        Session["newuser"] = p.TextValidation;
                        //send emil with value url TeXtVaLiDatIonFoRyOuUsErFoRvAlIdCoMpt = p.TextValidation 
                        db.Personnes.Add(p);
                        db.SaveChanges();
                        return RedirectToAction("Index", "Connect" );
                    }// else add sweet alert 
                }// else add sweet alert 
            }
            // else add sweet alert 
            return RedirectToAction("Index", "Connect");
        }
        public  ActionResult confirmedInfo()
        {
            
            return RedirectToAction("Home", "Home");
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