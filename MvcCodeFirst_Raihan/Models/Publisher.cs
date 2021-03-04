using MvcCodeFirst_Raihan.CustomAttribute;
using MvcCodeFirst_Raihan.CustomValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcCodeFirst_Raihan.Models
{
    public class Publisher
    {
        public int PublisherId { get; set; }
        [Required(ErrorMessage = "You Can't Leave it Blank")]
        public string Name { get; set; }
        [Required(ErrorMessage = "You Can't Leave it Blank")]
        [Display(Name = "Cell Phone No")]
        [MyAttribute]
        public string CellPhoneNo { get; set; }
        public string Email { get; set; }
        [DataType(DataType.Date)]
        [CustomHireDate(ErrorMessage = "Hire Date must be less than or equal to Today's Date")]
        public DateTime PublishDate { get; set; }
        public virtual ICollection<BookDetail> BookDetails { get; set; }
    }
}