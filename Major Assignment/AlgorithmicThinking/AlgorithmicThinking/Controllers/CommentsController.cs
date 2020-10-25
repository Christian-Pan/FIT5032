using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AlgorithmicThinking.Models;
using Microsoft.AspNet.Identity;

namespace AlgorithmicThinking.Controllers
{
    [Authorize]
    public class CommentsController : Controller
    {
        private Algorithmic_Thinking_ModelContainer db = new Algorithmic_Thinking_ModelContainer();

        // GET: Comments
        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            var comments = db.Comments;
            return View(comments.ToList());
        }

        // GET: Comments/Details/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // GET: Comments/Create
        public ActionResult Create(bool? IsForSection, int? number)
        {
            if (IsForSection == null && number == null)
            {
                ViewBag.SectionId = 1;
                ViewBag.CommentId = 13;
            } 
            else
            {
                if (IsForSection == true)
                {
                    ViewBag.SectionId = number;
                    ViewBag.CommentId = 13;
                }
                else
                {
                    var comment = db.Comments.First(i => i.Id == number);
                    ViewBag.SectionId = comment.SectionId;
                    ViewBag.CommentId = number;
                }
            }
            return View();
        }

        // POST: Comments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "Id,Content,SectionId,CommentId")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                comment.Uid = User.Identity.GetUserId();
                comment.Datetime = DateTime.Now;
                db.Comments.Add(comment);
                db.SaveChanges();

                return RedirectToAction("Details", "Sections", new { id = comment.SectionId });
            }

            return View(comment);
        }

        // GET: Comments/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            ViewBag.SectionId = new SelectList(db.Sections, "Id", "Title", comment.SectionId);
            ViewBag.CommentId = new SelectList(db.Comments, "Id", "Id", comment.CommentId);
            return View(comment);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit([Bind(Include = "Id,Content,SectionId,CommentId")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                if (!User.IsInRole("Administrator"))
                {
                    comment.Uid = User.Identity.GetUserId();
                    comment.Datetime = DateTime.Now;
                }
                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SectionId = new SelectList(db.Sections, "Id", "Title", comment.SectionId);
            ViewBag.CommentId = new SelectList(db.Comments, "Id", "Id", comment.CommentId);
            return View(comment);
        }

        // GET: Comments/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteConfirmed(int id)
        {
            Comment comment = db.Comments.Find(id);
            db.Comments.Remove(comment);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
