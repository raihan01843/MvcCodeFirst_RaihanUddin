using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcCodeFirst_Raihan.ViewModels
{
    public class ProductVM
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string ImagePath { get; set; }


        public HttpPostedFileBase ImageFile { get; set; }

    }
}