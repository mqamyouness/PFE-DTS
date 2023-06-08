using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyCoaches.Controllers
{
    public class CoachesController : Controller
    {
        // GET: Coaches
        public ActionResult Index()
        {
            return View();
        }
    }
}