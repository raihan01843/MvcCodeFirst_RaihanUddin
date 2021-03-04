using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcCodeFirst_Raihan.ViewModels
{
    public class StudentVM
    {
        public int StudentID { get; set; }

        [Required]
        [Display(Name = "Student Name")]
        public string StudentName { get; set; }

        [Required]
        [Display(Name = "Cell Phone No.")]
        public string CellPhone { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [Display(Name = "Contact Address")]
        public string ContactAddress { get; set; }
    }
}