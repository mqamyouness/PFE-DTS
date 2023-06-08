using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyCoaches.Models;
using System.Security.Cryptography;
using System.Text;

namespace MyCoaches.Controllers.Backend
{
    public class LoginController : Controller
    {
        private PFEEntities db = new PFEEntities();
        // GET: Login
        public ActionResult Index()
        {
            if (Session["Admin"] != null)
            {
                Response.Redirect("~/Dashboard/Index");
            }
            return View();
        }
        [HttpPost]
        public ActionResult Login()
        {
            if (Session["Admin"] != null)
            {
                Response.Redirect("~/Dashboard/Index");
            }
            string username = Request.Params.Get("username");
            string password = Request.Params.Get("password");
            if (password != "" && username != "")
            {
                var user = db.AdminBackends.Where(u => u.email == username || u.username == username);
                if (user.Count() > 0)
                {
                    if (user.First().password == HashString(password))
                    {
                        Session["Admin"] = user.First().id;
                        if (user.First().gestionDesAdmin == true)
                        {
                            Session["AdminPr"] = true;
                        }
                        return RedirectToAction("Index", "Dashboard");
                    }
                }
            }
            // else add sweet alert 
            return View("Index");
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
        public ActionResult Logout()
        {
            Session["Admin"] = null;
            Response.Redirect("~/Login/Index");
            return View();
        }
    }
}
