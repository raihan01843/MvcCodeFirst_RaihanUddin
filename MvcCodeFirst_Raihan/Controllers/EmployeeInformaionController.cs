using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcCodeFirst_Raihan.Models;

namespace MvcCodeFirst_Raihan.Controllers
{
    public class EmployeeInformaionController : Controller
    {
        // GET: EmployeeInformaion
        private ApplicationDbContext _context;
        public EmployeeInformaionController()
        {
            _context = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult List()
        {
            return Json(_context.EmployeeInfromations.ToList(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Add(EmployeeInfromation user)
        {
            _context.EmployeeInfromations.Add(user);
            _context.SaveChanges();
            return Json(JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetbyID(int ID)
        {
            return Json(_context.EmployeeInfromations.FirstOrDefault(x => x.ID == ID), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Update(EmployeeInfromation user)
        {
            var data = _context.EmployeeInfromations.FirstOrDefault(x => x.ID == user.ID);
            if (data != null)
            {
                data.Name = user.Name;
                data.District = user.District;
                data.Country = user.Country;
                data.Age = user.Age;
                _context.SaveChanges();
            }
            return Json(JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete(int ID)
        {
            var data = _context.EmployeeInfromations.FirstOrDefault(x => x.ID == ID);
            _context.EmployeeInfromations.Remove(data);
            _context.SaveChanges();
            return Json(JsonRequestBehavior.AllowGet);
        }
    }
}