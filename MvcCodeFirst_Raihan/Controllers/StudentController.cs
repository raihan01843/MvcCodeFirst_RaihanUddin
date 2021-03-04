using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Threading.Tasks;
using AutoMapper;
using MvcCodeFirst_Raihan.ViewModels;
using MvcCodeFirst_Raihan.Models;
using System.Net;
using System.Data.Entity;
using PagedList;

namespace MvcProject_Raihan.Controllers
{
    [RoutePrefix("Student Info")]
    public class StudentController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        //// GET: Student
        //public ActionResult Index()
        //{
        //    return View();
        //}

        // GET: Trainee
        [Route("Home")]
        public ActionResult Index(string sortOrder, string searchString, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParam = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            var students = from t in db.Students
                           select t;
            if (!string.IsNullOrEmpty(searchString))
            {
                students = students.Where(t => t.StudentName.ToUpper().Contains(searchString.ToUpper()));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    students = students.OrderByDescending(t => t.StudentName);
                    break;
                default:
                    students = students.OrderBy(t => t.StudentName);
                    break;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(students.ToPagedList(pageNumber, pageSize));
        }

        // GET: Details
        [Route("Details/{id}")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var query = db.Students.Single(t => t.StudentID == id);
            var student = Mapper.Map<Student, StudentVM>(query);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StudentVM studentVM)
        {
            if (ModelState.IsValid)
            {
                var student = Mapper.Map<Student>(studentVM);
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(studentVM);
        }

        // GET: Edit
        public ActionResult Edit(int? id)
        {
            var query = db.Students.Single(t => t.StudentID == id);
            var student = Mapper.Map<Student, StudentVM>(query);
            return View(student);
        }

        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StudentVM studentVM)
        {
            if (ModelState.IsValid)
            {
                var student = Mapper.Map<Student>(studentVM);
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(studentVM);
        }

        // GET: Delete
        //[Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            var query = db.Students.Single(t => t.StudentID == id);
            var student = Mapper.Map<Student, StudentVM>(query);
            return View(student);
        }

        // POST: Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, StudentVM traineeVM)
        {
            var query = db.Students.Single(t => t.StudentID == id);
            var student = Mapper.Map<Student, StudentVM>(query);
            db.Students.Remove(query);  // 
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