using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Drawing;
using System.Net;
using System.Net.Mail;
using System.Text;



namespace MyCoaches.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult Index()
        {
            if (Session["user"] != null)
            {
                Response.Redirect(Url.Action("Home", "Home"));
            }
            return View();
        }
        //send email
        public ActionResult sendEmail()
        {
            string emailFrom = "mqamyouness18@gmail.com";
            string emailTo = Request.Params.Get("email");
            string name = Request.Params.Get("name");
            string subject = Request.Params.Get("subject");
            string text =  Request.Params.Get("text");
            var spmt = new SmtpClient();
            {
                spmt.Host = "smpt.gmail.com";
                spmt.Port = 587;
                spmt.EnableSsl = true;
                spmt.DeliveryMethod = SmtpDeliveryMethod.Network;
                spmt.Credentials = new NetworkCredential(emailFrom, "Mqamyouness18@@");
                spmt.Timeout = 2000;
            }
            spmt.Send(emailFrom, emailFrom, subject, text);
            return View();// RedirectToAction("Index", "Contact");
        }
    }
}