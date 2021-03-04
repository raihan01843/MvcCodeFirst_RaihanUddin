using MvcCodeFirst_Raihan.CustomValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcCodeFirst_Raihan.Models
{
    public class BookDetail
    {
        public int BookDetailID { get; set; }
        public string BookName { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/mm/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Purchase Date")]
        [DataType(DataType.Date)]
        [CustomHireDate(ErrorMessage = "Hire Date must be less than or equal to Today's Date")]
        public DateTime PurchaseDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/mm/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Return Date")]
        [DataType(DataType.Date)]
        [CustomHireDate(ErrorMessage = "Hire Date must be less than or equal to Today's Date")]
        public DateTime ReturnDate { get; set; }
        public int BookPrice { get; set; }
        public int ServiceCharge { get; set; }
        public int DeleveryCharge { get; set; }
        [NotMapped]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int Total { get { return (BookPrice+ServiceCharge + DeleveryCharge); } }
        public int PublisherId { get; set; }


        public virtual Publisher Publisher { get; set; }
    }
}