using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using MvcCodeFirst_Raihan.Models;

namespace MvcCodeFirst_Raihan.Controllers
{
    public class TeacherController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

       
        public ActionResult Index()
        {
            var data = db.Teachers.ToList();
            return View(data);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Teacher tr)
        {
            if (ModelState.IsValid == true)
            {
                string fileName = Path.GetFileNameWithoutExtension(tr.ImageFile.FileName);
                string extention = Path.GetExtension(tr.ImageFile.FileName);
                HttpPostedFileBase postedFile = tr.ImageFile;
                int length = postedFile.ContentLength;
                if (extention.ToLower() == ".jpg" || extention.ToLower() == ".jpeg" || extention.ToLower() == ".png")
                {
                    if (length <= 1000000)
                    {
                        fileName = fileName + extention;
                        tr.ImagePath = "~/AppFiles/Images/" + fileName;
                        fileName = Path.Combine(Server.MapPath("~/AppFiles/Images/"), fileName);
                        tr.ImageFile.SaveAs(fileName);
                        db.Teachers.Add(tr);
                        int a = db.SaveChanges();
                        if (a > 0)
                        {

                            ModelState.Clear();
                            return RedirectToAction("Index", "Teacher");
                        }
                        else
                        {
                            TempData["CreateMessage"] = "<script>alert('Data not inserted')</script>";
                        }
                    }
                    else
                    {
                        TempData["SizeMessage"] = "<script>alert('Image Size Should Less Than 1 MB')</script>";
                    }
                }
                else
                {
                    TempData["ExtentionMessage"] = "<script>alert('Format Not Supported')</script>";
                }
            }
            return View();
        }
        public ActionResult Edit(int id)
        {
            var TeacherRow = db.Teachers.Where(model => model.TeacherID == id).FirstOrDefault();
            Session["Image"] = TeacherRow.ImagePath;
            return View(TeacherRow);
        }

        [HttpPost]
        public ActionResult Edit(Teacher tr)
        {
            if (ModelState.IsValid == true)
            {
                if (tr.ImageFile != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(tr.ImageFile.FileName);
                    string extention = Path.GetExtension(tr.ImageFile.FileName);
                    HttpPostedFileBase postedFile = tr.ImageFile;
                    int length = postedFile.ContentLength;
                    if (extention.ToLower() == ".jpg" || extention.ToLower() == ".jpeg" || extention.ToLower() == ".png")
                    {
                        if (length <= 1000000)
                        {
                            fileName = fileName + extention;
                            tr.ImagePath = "~/AppFiles/Images/" + fileName;
                            fileName = Path.Combine(Server.MapPath("~/AppFiles/Images/"), fileName);
                            tr.ImageFile.SaveAs(fileName);
                            db.Entry(tr).State = EntityState.Modified;
                            int a = db.SaveChanges();
                            if (a > 0)
                            {
                                ModelState.Clear();
                                return RedirectToAction("Index", "Teacher");
                            }
                            else
                            {
                                TempData["UpdateMessage"] = "<script>alert('Data not Updated')</script>";
                            }
                        }
                        else
                        {
                            TempData["SizeMessage"] = "<script>alert('Image Size Should Less Than 1 MB')</script>";
                        }
                    }
                    else
                    {
                        TempData["ExtentionMessage"] = "<script>alert('Format Not Supported')</script>";
                    }
                }
                else
                {
                    tr.ImagePath = Session["Image"].ToString();
                    db.Entry(tr).State = EntityState.Modified;
                    int a = db.SaveChanges();
                    if (a > 0)
                    {
                        TempData["UpdateMessage"] = "<script>alert('Data Updated Successfully')</script>";
                        ModelState.Clear();
                        return RedirectToAction("Index", "Teacher");
                    }
                    else
                    {
                        TempData["UpdateMessage"] = "<script>alert('Data not Updated')</script>";
                    }
                }
            }
            return View();
        }


        public ActionResult Delete(int id = 0)
        {
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();

            }
            return View(teacher);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmDelete(int id = 0)
        {
            Teacher teacher = db.Teachers.Find(id);
            db.Teachers.Remove(teacher);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}