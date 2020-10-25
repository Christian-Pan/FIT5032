using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlgorithmicThinking.Models;

namespace AlgorithmicThinking.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        [RequireHttps]
        public ActionResult Index()
        {
            if (User.IsInRole("Administrator"))
                return View();
            return RedirectToAction("UserIndex", "Chapters");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            return RedirectToAction("Index", "Locations");
        }

        public ActionResult Feedback()
        {
            return View();
        }

        public ActionResult Send_Email()
        {
            return View(new SendEmailViewModel());
        }

        [HttpPost]
        public ActionResult Send_Email(SendEmailViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    String toEmail = model.ToEmail;
                    String subject = model.Subject;
                    String contents = model.Contents;

                    EmailSender es = new EmailSender();
                    es.Send(toEmail, subject, contents);

                    ViewBag.Result = "Email has been send.";

                    ModelState.Clear();

                    return View(new SendEmailViewModel());
                }
                catch
                {
                    return View();
                }
            }

            return View();
        }
    }
}