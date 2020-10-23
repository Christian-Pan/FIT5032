using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AlgorithmicThinking.Models;

namespace AlgorithmicThinking.Controllers
{
    [Authorize]
    public class SectionsController : Controller
    {
        private Algorithmic_Thinking_ModelContainer db = new Algorithmic_Thinking_ModelContainer();
        private ApplicationDbContext userDb = new ApplicationDbContext();
        // GET: Sections
        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            var sections = db.Sections.Include(s => s.Chapter);
            return View(sections.ToList());
        }

        // GET: Sections/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Section section = db.Sections.Find(id);
            Chapter chapter = db.Chapters.Find(section.ChapterId);

            if (section == null)
            {
                return HttpNotFound();
            }

            // ViewBag.Chapter = chapter.Title;
            // ViewBag.ChapterContent = chapter.Content;
            ViewBag.SectionId = section.Id;
            ViewBag.Section = section.Title;
            ViewBag.SectionContent = section.Content;
            ViewBag.Id = chapter.Id;

            Dictionary<Comment, List<Comment>> dict = new Dictionary<Comment, List<Comment>>();
            foreach (var comment in db.Comments)
            {
                if (comment.SectionId == section.Id && comment.CommentId == 13)
                {
                    var user = userDb.Users.First(i => i.Id == comment.Uid);
                    comment.Uid = user.Email;
                    dict.Add(comment, new List<Comment>());

                    foreach (var subComment in db.Comments)
                    {
                        if (subComment.SectionId == section.Id && subComment.CommentId == comment.Id)
                        {
                            var anotherUser = userDb.Users.First(i => i.Id == subComment.Uid);
                            subComment.Uid = anotherUser.Email;
                            dict[comment].Add(subComment);
                        }
                    }
                }
            }

            /*
            List<Comment> comments = new List<Comment>();
            foreach (var comment in db.Comments)
            {
                if (comment.SectionId == section.Id)
                {
                    var user = userDb.Users.First(i => i.Id == comment.Uid);
                    if (comment.CommentId == 13)
                    {
                        comment.Uid = user.Email;
                    }
                    else
                    {
                        var topComment = db.Comments.First(i => i.Id == comment.CommentId);
                        var anotherUser = userDb.Users.First(i => i.Id == comment.Uid);
                        comment.Uid = user.Email + " replies " + anotherUser.Email;
                    }
                    comments.Add(comment);
                }
            }
            */

            return View(dict);
        }

        // GET: Sections/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            ViewBag.ChapterId = new SelectList(db.Chapters, "Id", "Title");
            return View();
        }

        // POST: Sections/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "Id,Title,Content,ChapterId")] Section section)
        {
            if (ModelState.IsValid)
            {
                section.LastUpdatedTime = DateTime.Now;
                db.Sections.Add(section);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ChapterId = new SelectList(db.Chapters, "Id", "Title", section.ChapterId);
            return View(section);
        }

        // GET: Sections/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Section section = db.Sections.Find(id);
            if (section == null)
            {
                return HttpNotFound();
            }
            ViewBag.ChapterId = new SelectList(db.Chapters, "Id", "Title", section.ChapterId);
            return View(section);
        }

        // POST: Sections/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "Id,Title,Content,ChapterId")] Section section)
        {
            if (ModelState.IsValid)
            {
                section.LastUpdatedTime = DateTime.Now;
                db.Entry(section).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ChapterId = new SelectList(db.Chapters, "Id", "Title", section.ChapterId);
            return View(section);
        }

        // GET: Sections/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Section section = db.Sections.Find(id);
            if (section == null)
            {
                return HttpNotFound();
            }
            return View(section);
        }

        // POST: Sections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteConfirmed(int id)
        {
            Section section = db.Sections.Find(id);
            db.Sections.Remove(section);
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
