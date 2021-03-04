using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

using MvcCodeFirst_Raihan.Models;

namespace MvcCodeFirst_Raihan.Controllers
{
    public class OrdersController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            List<Customer> OrderAndCustomerList = db.Customers.ToList();
            return View(OrderAndCustomerList);
        }

        public ActionResult SaveOrder(string name, String address, Order[] order)
        {
            string result = "Error! Order Is Not Complete!";
            if (name != null && address != null && order != null)
            {
                var cutomerId = Guid.NewGuid();
                Customer model = new Customer();
                model.CustomerID = cutomerId;
                model.Name = name;
                model.Address = address;
                model.OrderDate = DateTime.Now;
                db.Customers.Add(model);

                foreach (var item in order)
                {
                    var orderId = Guid.NewGuid();
                    Order O = new Order();
                    O.OrderID = orderId;
                    O.ProductName = item.ProductName;
                    O.Quantity = item.Quantity;
                    O.Price = item.Price;
                    O.Amount = item.Amount;
                    O.CustomerID = cutomerId;
                    db.Orders.Add(O);
                }
                db.SaveChanges();
                result = "Success! Order Is Complete!";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        public ActionResult EditOrder(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerID", "Name", order.CustomerID);
            return View(order);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditOrder([Bind(Include = "OrderID,ProductName,Quantity,Price,Amount,CustomerID")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerID", "Name", order.CustomerID);
            return View(order);
        }



        public ActionResult EditCustomer(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Customer cus = db.Customers.Find(id);
            if (cus == null)
            {
                return HttpNotFound();
            }

            return View(cus);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCustomer([Bind(Include = "CustomerID,Name,Address,OrderDate")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        public ActionResult DeleteCustomer(Guid? id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteOrder(Guid? id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}