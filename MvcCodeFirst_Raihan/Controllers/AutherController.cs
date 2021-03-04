using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using MvcCodeFirst_Raihan.Models;

namespace MvcCodeFirstPrject_Moin.Controllers
{
    public class AutherController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: Customer
        public ActionResult Index()
        {
            var data = db.Authers.ToList();
            return View(data);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Auther tr)
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
                        tr.Image = "~/AppFiles/Images/" + fileName;
                        fileName = Path.Combine(Server.MapPath("~/AppFiles/Images/"), fileName);
                        tr.ImageFile.SaveAs(fileName);
                        db.Authers.Add(tr);
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
            var TeacherRow = db.Authers.Where(model => model.AutherID == id).FirstOrDefault();
            Session["Image"] = TeacherRow.Image;
            return View(TeacherRow);
        }

        [HttpPost]
        public ActionResult Edit(Auther tr)
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
                            tr.Image = "~/AppFiles/Images/" + fileName;
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
                    tr.Image = Session["Image"].ToString();
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
            Auther auther = db.Authers.Find(id);
            if (auther == null)
            {
                return HttpNotFound();

            }
            return View(auther);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmDelete(int id = 0)
        {
            Auther auther = db.Authers.Find(id);
            db.Authers.Remove(auther);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}