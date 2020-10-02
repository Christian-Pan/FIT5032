using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AlgorithmicThinking.Models;

namespace AlgorithmicThinking.Controllers
{
    [Authorize]
    public class ChaptersController : Controller
    {
        private Algorithmic_Thinking_ModelContainer db = new Algorithmic_Thinking_ModelContainer();

        // GET: Chapters
        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            return View(db.Chapters.ToList());
        }

        public ActionResult UserIndex()
        {
            return View(db.Chapters.ToList());
        }

        // GET: Chapters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chapter chapter = db.Chapters.Find(id);
            if (chapter == null)
            {
                return HttpNotFound();
            }

            List<Section> sections = new List<Section>();
            foreach (var section in db.Sections)
            {
                if (section.ChapterId == id) sections.Add(section);
            }

            ViewBag.ChapterTitle = chapter.Title;
            ViewBag.Content = chapter.Content;
            ViewBag.LastUpdatedTime = chapter.LastUpdatedTime;

            return View(sections);
        }

        // GET: Chapters/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Chapters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create([Bind(Include = "Id,Title,Content")] Chapter chapter)
        {
            if (ModelState.IsValid)
            {
                chapter.LastUpdatedTime = DateTime.Now;
                db.Chapters.Add(chapter);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(chapter);
        }

        // GET: Chapters/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chapter chapter = db.Chapters.Find(id);
            if (chapter == null)
            {
                return HttpNotFound();
            }
            return View(chapter);
        }

        // POST: Chapters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit([Bind(Include = "Id,Title,Content")] Chapter chapter)
        {
            if (ModelState.IsValid)
            {
                chapter.LastUpdatedTime = DateTime.Now;
                db.Entry(chapter).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(chapter);
        }

        // GET: Chapters/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chapter chapter = db.Chapters.Find(id);
            if (chapter == null)
            {
                return HttpNotFound();
            }
            return View(chapter);
        }

        // POST: Chapters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteConfirmed(int id)
        {
            Chapter chapter = db.Chapters.Find(id);
            db.Chapters.Remove(chapter);
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
