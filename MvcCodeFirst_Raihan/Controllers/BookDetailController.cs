using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MvcCodeFirst_Raihan.Models;

namespace MvcCodeFirst_Raihan.Controllers
{
    public class BookDetailController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BookDetail
        public ActionResult Index()
        {
            var bookDetails = db.BookDetails.Include(b => b.Publisher);
            return View(bookDetails.ToList());
        }

        // GET: BookDetail/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    BookDetail bookDetail = db.BookDetails.Find(id);
        //    if (bookDetail == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(bookDetail);
        //}

        // GET: BookDetail/Create
        public ActionResult Create()
        {
            ViewBag.PublisherId = new SelectList(db.Publishers, "PublisherId", "Name");
            return View();
        }

        // POST: BookDetail/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookDetailID,BookName,PublisherId,PurchaseDate,ReturnDate,BookPrice,ServiceCharge,DeleveryCharge,Total")] BookDetail bookDetail)
        {
            if (ModelState.IsValid)
            {
                db.BookDetails.Add(bookDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PublisherId = new SelectList(db.Publishers, "PublisherId", "Name", bookDetail.PublisherId);
            return View(bookDetail);
        }

        // GET: BookDetail/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookDetail bookDetail = db.BookDetails.Find(id);
            if (bookDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.PublisherId = new SelectList(db.Publishers, "PublisherId", "Name", bookDetail.PublisherId);
            return View(bookDetail);
        }

        // POST: BookDetail/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookDetailID,BookName,PublisherId,PurchaseDate,ReturnDate,BookPrice,ServiceCharge,DeleveryCharge,Total")] BookDetail bookDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bookDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PublisherId = new SelectList(db.Publishers, "PublisherId", "Name", bookDetail.PublisherId);
            return View(bookDetail);
        }

        // GET: BookDetail/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookDetail bookDetail = db.BookDetails.Find(id);
            if (bookDetail == null)
            {
                return HttpNotFound();
            }
            return View(bookDetail);
        }

        // POST: BookDetail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BookDetail bookDetail = db.BookDetails.Find(id);
            db.BookDetails.Remove(bookDetail);
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
