using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyCoaches.Controllers
{
    public class InController : Controller
    {
        // GET: In
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login()
        {
            Session["user"] = "true";
            Response.Redirect("../Home/Home");
            return View();
 
        }
    }
}