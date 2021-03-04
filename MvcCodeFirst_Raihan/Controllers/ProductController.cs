using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcCodeFirst_Raihan.Models;
using MvcCodeFirst_Raihan.ViewModels;
using PagedList;

namespace ViewModelInMvc.Controllers
{
    [RoutePrefix("Product Info")]
    public class ProductController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Product
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

            var products = from t in db.Products
                           select t;
            if (!string.IsNullOrEmpty(searchString))
            {
                products = products.Where(t => t.ProductName.ToUpper().Contains(searchString.ToUpper()));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    products = products.OrderByDescending(t => t.ProductName);
                    break;
                default:
                    products = products.OrderBy(t => t.ProductName);
                    break;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(products.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductVM productVM)
        {
            if (ModelState.IsValid)
            {
                string fileName = Path.GetFileNameWithoutExtension(productVM.ImageFile.FileName);
                string extention = Path.GetExtension(productVM.ImageFile.FileName);
                HttpPostedFileBase postedFile = productVM.ImageFile;
                int length = postedFile.ContentLength;

                if (extention.ToLower() == ".jpg" || extention.ToLower() == ".jpeg" || extention.ToLower() == ".png")
                {
                    if (length <= 1000000)
                    {
                        fileName = fileName + extention;
                        productVM.ImagePath = "~/AppFiles/Images/" + fileName;
                        fileName = Path.Combine(Server.MapPath("~/AppFiles/Images/"), fileName);
                        productVM.ImageFile.SaveAs(fileName);
                        var product = Mapper.Map<Product>(productVM);

                        db.Products.Add(product);
                        int a = db.SaveChanges();
                        if (a > 0)
                        {

                            ModelState.Clear();
                            return RedirectToAction("Index", "Product");
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

        public ActionResult Edit(int? id)
        {
            var query = db.Products.Single(t => t.ProductID == id);
            Session["Image"] = query.ImagePath;

            var product = Mapper.Map<Product, ProductVM>(query);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductVM productVM)
        {

            if (ModelState.IsValid == true)
            {
                if (productVM.ImageFile != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(productVM.ImageFile.FileName);
                    string extention = Path.GetExtension(productVM.ImageFile.FileName);
                    HttpPostedFileBase postedFile = productVM.ImageFile;
                    int length = postedFile.ContentLength;
                    if (extention.ToLower() == ".jpg" || extention.ToLower() == ".jpeg" || extention.ToLower() == ".png")
                    {
                        if (length <= 1000000)
                        {
                            fileName = fileName + extention;
                            productVM.ImagePath = "~/AppFiles/Images/" + fileName;
                            fileName = Path.Combine(Server.MapPath("~/AppFiles/Images/"), fileName);
                            productVM.ImageFile.SaveAs(fileName);

                            var product = Mapper.Map<Product>(productVM);


                            db.Entry(product).State = EntityState.Modified;
                            int a = db.SaveChanges();
                            if (a > 0)
                            {
                                ModelState.Clear();
                                return RedirectToAction("Index", "Product");
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
                    productVM.ImagePath = Session["Image"].ToString();
                    var product = Mapper.Map<Product>(productVM);

                    db.Entry(product).State = EntityState.Modified;
                    int a = db.SaveChanges();
                    if (a > 0)
                    {
                        TempData["UpdateMessage"] = "<script>alert('Data Updated Successfully')</script>";
                        ModelState.Clear();
                        return RedirectToAction("Index", "Product");
                    }
                    else
                    {
                        TempData["UpdateMessage"] = "<script>alert('Data not Updated')</script>";
                    }
                }
            }
            return View();


        }


        public ActionResult Delete(int? id)
        {
            var query = db.Products.Single(t => t.ProductID == id);
            var product = Mapper.Map<Product, ProductVM>(query);
            return View(product);
        }

        // POST: Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, ProductVM productVM)
        {
            var query = db.Products.Single(t => t.ProductID == id);
            var trainee = Mapper.Map<Product, ProductVM>(query);
            db.Products.Remove(query);  // 
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}

