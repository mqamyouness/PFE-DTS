using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyCoaches.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            if (Session["user"] != null)
            {
                Response.Write("<h1>" + Session["user"] + "</h1>");
                Response.Write("<h1>/Home/Home</h1>");
                return View("Home");
            }
            Response.Write("<h1>/Home/index</h1>");
            return View();
        }

        // GET: Home
        public ActionResult Home()
        {
            return View();
        }

    }
}