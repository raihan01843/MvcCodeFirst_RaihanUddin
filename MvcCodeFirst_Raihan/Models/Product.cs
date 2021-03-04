using MvcCodeFirst_Raihan.CustomValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcCodeFirst_Raihan.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        [Required(ErrorMessage ="This Field Must be fill!!")]
        public string ProductName { get; set; }
        [Required(ErrorMessage = "This Field Must be fill!!")]
        public string Description { get; set; }
        [Required(ErrorMessage = "This Field Must be fill!!")]
        [Display(Name ="Purchase Date ")]
        [DataType(DataType.Date)]
        [CustomHireDate(ErrorMessage = "Hire Date must be less than or equal to Today's Date")]
        public DateTime PurchaseDate { get; set; }
        [Required(ErrorMessage = "This Field Must be fill!!")]
        [Display(Name = "Photo ")]
        public string ImagePath { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }

    }
}