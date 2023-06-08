using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyCoaches.Controllers
{
    public class SportsController : Controller
    {
        // GET: Sports
        public ActionResult Index()
        {
            return View();
        }
    }
}